using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Make Connection with DB===DBConfig in Spring,and helper methods/wrappers for repository queries.
//Has wrappers for queries written in Router,to define if you wanna save or retrieve data from DB.
public class DatabaseManager : MonoBehaviour
{

    //Only a single instance(singleton) must exist in the VM,since multiple instances can make concurrent changes hence corrupting the data.

    public static DatabaseManager sharedInstance = null;

    //used for Testing purpose
    //public Button LoginButton;
    // public ARSceneController arScene_Replica;


    /// <summary>
    /// Awake this instance and initialize sharedInstance for Singleton pattern
    /// </summary>
    /// //check if dual instances are found,script auto destroys.
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://cardxp-217cc.firebaseio.com/");
        Debug.Log("Connected to Database");

    }
    string key = "7MMLJ9DHQq";
    ////used for Testing purpose
    private void Start()
    {
        // key = Router_Manager.baseRef.Push().Key;
        //CustomerInfo customer = new CustomerInfo();
        //CreateNewCustomerNodes(customer, key);
    }



    //}
    ////used for Testing purpose
    public void onLoginButtonClick()
    {
        // arScene_Replica.loginId = key;
        SceneManager.LoadScene("ARScene");
    }


    //retrieve customer data based on UserID
    public void FetchCustomerInfo(Action<RootObject> completionBlock, string uid)
    {
        Debug.Log("In DB Manager");
        RootObject rootObject = new RootObject();
        RouterManager.baseRef.Child("customerDetails/" + uid).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot customers = task.Result;
                //Debug.Log("customers::::::" + customers.Key + "::::::" + customers.ChildrenCount + ":::::::" + customers.GetRawJsonValue());

                string result = customers.GetRawJsonValue(); //your firebase json response
                Customer customer = new Customer();
                customer = JsonUtility.FromJson<Customer>(result);
                rootObject.customer = customer;
                Debug.Log("customer::::" + customer.email);
                

                RouterManager.baseRef.Child("balanceInfo/" + uid).GetValueAsync().ContinueWith(task1 =>
                {
                    if (task1.IsCompleted)
                    {
                        DataSnapshot balance = task1.Result;
                        // Debug.Log("customers::::::" + customers.Key + "::::::" + customers.ChildrenCount + ":::::::" + customers.GetRawJsonValue());

                        string result1 = balance.GetRawJsonValue(); //your firebase json response
                        Balance balanceInfo = new Balance();
                        balanceInfo = JsonUtility.FromJson<Balance>(result1);
                        rootObject.balance = balanceInfo;
                        Debug.Log("balanceInfo::::" + balanceInfo.b1);

                        RouterManager.baseRef.Child("monthlyExpd/" + uid).GetValueAsync().ContinueWith(task2 =>
                        {
                            if (task2.IsCompleted)
                            {
                                DataSnapshot monthlyExpd = task2.Result;
                                // Debug.Log("customers::::::" + customers.Key + "::::::" + customers.ChildrenCount + ":::::::" + customers.GetRawJsonValue());

                                string result2 = monthlyExpd.GetRawJsonValue(); //your firebase json response
                                MonthlyExpenditure xpenses = new MonthlyExpenditure();
                                xpenses = JsonUtility.FromJson<MonthlyExpenditure>(result2);
                                rootObject.xpenses = xpenses;
                                Debug.Log("MonthlyExpenditure::::" + xpenses.ToString());

                                RouterManager.baseRef.Child("rewardPoints/" + uid).GetValueAsync().ContinueWith(task3 =>
                                {
                                    if (task3.IsCompleted)
                                    {
                                        DataSnapshot rewardPoints = task3.Result;
                                        // Debug.Log("customers::::::" + customers.Key + "::::::" + customers.ChildrenCount + ":::::::" + customers.GetRawJsonValue());

                                        string result3 = monthlyExpd.GetRawJsonValue(); //your firebase json response
                                        RewardPoints points = new RewardPoints();
                                        points = JsonUtility.FromJson<RewardPoints>(result3);
                                        rootObject.points = points;
                                        Debug.Log("RewardPoints::::" + points.ToString());

                                        RouterManager.baseRef.Child("dues/" + uid).GetValueAsync().ContinueWith(task4 =>
                                        {
                                            if (task4.IsCompleted)
                                            {
                                                DataSnapshot dues = task4.Result;
                                                // Debug.Log("customers::::::" + customers.Key + "::::::" + customers.ChildrenCount + ":::::::" + customers.GetRawJsonValue());

                                                string result4 = monthlyExpd.GetRawJsonValue(); //your firebase json response
                                                Dues dueInfo = new Dues();
                                                dueInfo = JsonUtility.FromJson<Dues>(result4);
                                                rootObject.dues = dueInfo;
                                                Debug.Log("Dues::::" + dueInfo.ToString());

                                                completionBlock(rootObject);
                                            }
                                        });


                                    }
                                });




                            }
                        });

                    }



                });
            }


        });

    }


    //insert new Customer
    public void CreateNewCustomerNodes(CustomerInfo customerInfo, string uid)
    {
        // string key = Router.baseRef.Push().Key;
        string key = uid;
        Debug.Log("key:::" + key);
        //Debug.Log("UID:::::" + uid);
        Customer custDetails = new Customer("Harsha Vardhan",customerInfo.email, customerInfo.mobile);
        Balance balanceInfo = new Balance(customerInfo.currentBalance, customerInfo.b1, customerInfo.b2, customerInfo.b3, customerInfo.b4);//for currentAccount Balance
        RewardPoints rewardPoints = new RewardPoints(customerInfo.points);
        Dues duesInfo = new Dues(customerInfo.dues_Electricity, customerInfo.dues_CreditCard, customerInfo.dues_Telephone);
        MonthlyExpenditure monthlyExpenses = new MonthlyExpenditure(customerInfo.week1, customerInfo.week2, customerInfo.week3, customerInfo.week4);

        //string custJSON = JsonUtility.ToJson(custDetails);
        //string balanceJSON = JsonUtility.ToJson(balanceInfo);
        //string rewardPointsJSON = JsonUtility.ToJson(rewardPoints);
        //string duesJSON = JsonUtility.ToJson(duesInfo);
        //string monthlyExpensesJSON= JsonUtility.ToJson(monthlyExpenses);
        //Router_Manager.baseRef.Child("customerInfo").Child(uid).SetRawJsonValueAsync(custJSON);
        //Router_Manager.baseRef.Child("customerInfo"+uid+"/balanceInfo").SetRawJsonValueAsync(balanceJSON);
        //Router_Manager.baseRef.Child("customerInfo" + uid + "/rewardPoints").SetRawJsonValueAsync(rewardPointsJSON);
        //Router_Manager.baseRef.Child("customerInfo" + uid + "/duesInfo").SetRawJsonValueAsync(duesJSON);
        //Router_Manager.baseRef.Child("customerInfo" + uid + "/expenditureInfo").SetRawJsonValueAsync(monthlyExpensesJSON);

        // Router_Manager.CustomerWithUID(uid).SetRawJsonValueAsync(custJSON);

        //u need to map ur object to a dictionary
        Dictionary<string, System.Object> customerDict = custDetails.ToDictionary();
        Dictionary<string, System.Object> balanceDict = balanceInfo.ToDictionary();
        Dictionary<string, System.Object> rewardPointDict = rewardPoints.ToDictionary();
        Dictionary<string, System.Object> duesDict = duesInfo.ToDictionary();
        Dictionary<string, System.Object> monthlyExpdDict = monthlyExpenses.ToDictionary();
        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
        childUpdates["/balanceInfo/" + key] = balanceDict;
        childUpdates["/customerDetails/" + key] = customerDict;
        childUpdates["/rewardPoints/" + key] = rewardPointDict;
        childUpdates["/dues/" + key] = duesDict;
        childUpdates["/monthlyExpd/" + key] = monthlyExpdDict;

        RouterManager.baseRef.UpdateChildrenAsync(childUpdates);
    }
}