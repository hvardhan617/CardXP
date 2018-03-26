using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;


//only handles authentication methods from Firebase API and returns a task.
//Task handling is not done here,Managed in Form Manager
public class AuthManager : MonoBehaviour
{

    // Firebase API variables
    Firebase.Auth.FirebaseAuth auth;

    // Delegates
    public delegate IEnumerator AuthCallback_SignUp(Task<Firebase.Auth.FirebaseUser> task, string operation, CustomerInfo custInfoRs);
    public event AuthCallback_SignUp authCallback_SignUp;

    public delegate IEnumerator AuthCallback_Login(Task<Firebase.Auth.FirebaseUser> task, string operation);
    public event AuthCallback_Login authCallback_Login;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    //returns a task of type FirebaseUser
    public void SignUpNewUser(string email, string password, CustomerInfo customerInfo)
    {
        Debug.Log("Signing Up New User" + email);
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            StartCoroutine(authCallback_SignUp(task, "sign_up", customerInfo));
        });
    }

    //returns a task of type FirebaseUser
    public void LoginExistingUser(string email, string password)
    {
        Debug.Log("Logging in Existing User"+email);
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            StartCoroutine(authCallback_Login(task, "login"));
        });
    }
}