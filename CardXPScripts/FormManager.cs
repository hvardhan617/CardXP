using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using System;

public class FormManager : MonoBehaviour
{

    // UI objects linked from the inspector
    public InputField emailInput;
    public InputField passwordInput;

    public Button signUpButton;
    public Button loginButton;

    public Text statusText;

    //We need instances of Auth_Manager when the form data is filled for authentication.
    //We need Database_Manager for creating nodes and storing data after authentication.(Since DbManager is a static class we dont create any object for it)
    public AuthManager authManager;

    const int kMaxLogSize = 16382;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

    //  public ARSceneController aRSceneController;

    //its recommended to keep event handlers for delegates in awake method as it is called only once in the lifecycle of the script(only on initialization)
    void Awake()
    {
        ToggleButtonStates(false);
        // Auth delegate subscriptions
        authManager.authCallback_SignUp += HandleAuthCallback_SignUp;
        authManager.authCallback_Login += HandleAuthCallback_Login;
    }

    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("All Firebase Dependencies available....can initialize Application");
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
                //can be used to show an Error POPUp message
            }
        });
    }

    /// <summary>
    /// Validates the email input
    /// </summary>
    public void ValidateEmail()
    {
        string email = emailInput.text;
        var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        if (email != "" && Regex.IsMatch(email, regexPattern))
        {
            ToggleButtonStates(true);
        }
        else
        {
            ToggleButtonStates(false);
        }
    }

    //attached to SignUp Button
    public void OnSignUp()
    {

        string url = "http://127.0.0.1:8080/ping";

        WWWForm form = new WWWForm();
        form.AddField("var1", emailInput.text);
        form.AddField("var2", passwordInput.text);
        string body = JsonUtility.ToJson(form);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(body);

        Dictionary<string, string> headers = form.headers;
        headers["Content-Type"] = "application/json";

        WWW www = new WWW(url, data, headers);
        // WWW www = new WWW(url, form);


        StartCoroutine(WaitForRequest(www));

        //authManager.SignUpNewUser(emailInput.text, passwordInput.text);

        Debug.Log("Sign Up");
    }

    //attached to Login Button
    public void OnLogin()
    {
        authManager.LoginExistingUser(emailInput.text, passwordInput.text);

        Debug.Log("Login");
    }

    //for Testing Process
    public void onLoginButtonClickTest()
    {
        // arScene_Replica.loginId = key;
        CustomerInfo customer = new CustomerInfo();
        DatabaseManager.sharedInstance.CreateNewCustomerNodes(customer, "7MMLJ9DHQq");
        UnityEngine.SceneManagement.SceneManager.LoadScene("ARView");
    }

    //task returned by Auth_Manager is handled here.
    IEnumerator HandleAuthCallback_SignUp(Task<Firebase.Auth.FirebaseUser> task, string operation, CustomerInfo customerInfo)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            UpdateStatus("Sorry , there was an error creating your new account. ERROR: " + task.Exception);
        }
        else if (task.IsCompleted)
        {

            if (operation == "sign_up")
            {
                Firebase.Auth.FirebaseUser newCustomer = task.Result;
                Debug.LogFormat("Welcome to Bank AR {0}!", newCustomer.Email);

                //on Successful signUp,creating nodes in DB by populating data from REST Services hit earlier.
                DatabaseManager.sharedInstance.CreateNewCustomerNodes(customerInfo, newCustomer.UserId);
                // Database_Manager.sharedInstance.CreateNewCustomer(customer, newCustomer.UserId, balance, rewardPoints, dues,monthlyExpenses);
            }

            UpdateStatus("Please wait while we are setting up your account...");

            yield return new WaitForSeconds(1.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("ARView");
        }
    }

    //task returned by Auth_Manager is handled here.
    IEnumerator HandleAuthCallback_Login(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            UpdateStatus("Sorry , there was an error logging into your account. ERROR: " + task.Exception);
        }
        else if (task.IsCompleted)
        {
            //On login there is no Node creation of Db Data insertion,Hence Only data retrieval is there from appropriate paths/Db references.
            if (operation == "login")
            {
                Firebase.Auth.FirebaseUser loggedInUser = task.Result;
                Debug.LogFormat("You are logged in as", loggedInUser.Email);

                //on Successful login,fetch data from nodes in DB .
               // CustomerInfo customerInfo = new CustomerInfo();
               
            }

            UpdateStatus("Loading the AR scene");

            yield return new WaitForSeconds(1.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("ARView");
        }
    }

    void OnDestroy()
    {
        authManager.authCallback_SignUp -= HandleAuthCallback_SignUp;
        authManager.authCallback_Login -= HandleAuthCallback_Login;
    }

    // Utilities
    //make the buttons interactable only on entering appropriate data in the input fields.
    private void ToggleButtonStates(bool toState)
    {
        signUpButton.interactable = toState;
        loginButton.interactable = toState;
    }

    private void UpdateStatus(string message)
    {
        statusText.text = message;
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors(for Testing purpose)
        //Replace wil (www.error == null)when bank API is Up.
        if (www.error != null)
        {
            Debug.Log("Response Received from Bank API::::" + www.text);
            string json = www.text;
            Debug.Log("Json String::: " + json);
            CustomerInfo customerInfo = new CustomerInfo();
            //customerInfo = JsonUtility.FromJson<CustomerInfo>(json);
            Debug.Log("Response ::::: " + customerInfo.currentBalance);

            PassResponseToAuthManager(customerInfo);
        }
        else
        {
            Debug.Log("WWW Error::::Error in Service hit to Bank API " + www.error);
        }
    }

    public void PassResponseToAuthManager(CustomerInfo customerInfo)
    {
        authManager.SignUpNewUser(emailInput.text, passwordInput.text, customerInfo);
    }

    // Exit if escape (or back, on mobile) is pressed.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public InputField cardNumber;
    public InputField expiryDate;
    public InputField nameOnCard;
    public InputField nameOfBank;
    public Text response;
    //Attached to the Register Card Button when user tries to fill in Card Details
    public void OnRegisterButtonClick()
    {
        string url = "http://127.0.0.1:8080/ping";

        WWWForm form = new WWWForm();
        form.AddField("cardNumber", cardNumber.text);
        form.AddField("ExpiryDate", expiryDate.text);
        form.AddField("nameOnCard", nameOnCard.text);
        form.AddField("bankName", nameOfBank.text);
       

        string body = JsonUtility.ToJson(form);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(body);

        Dictionary<string, string> headers = form.headers;
        headers["Content-Type"] = "application/json";

        WWW www = new WWW(url, data, headers);
        // WWW www = new WWW(url, form);


        StartCoroutine(CreateLoginID(www));

        //authManager.SignUpNewUser(emailInput.text, passwordInput.text);

        Debug.Log("Register");

    }

    private IEnumerator CreateLoginID(WWW www)
    {
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Response Received from Auth API::::" + www.text);
            string json = www.text;
            Debug.Log("Json String::: " + json);
            RegisterInfo registerInfo = new RegisterInfo();
            registerInfo = JsonUtility.FromJson<RegisterInfo>(json);
            Debug.Log("Response ::::: " + registerInfo.loginId);

            response.text = "We have sent you a confirmation mail with your login credentials";

        }
        else
        {
            Debug.Log("WWW Error::::Error in Service hit to Bank API " + www.error);
            response.text = "There was an Error when trying to Register card..Please try later";
        }
    }
}
