using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer{

    public string displayName;
    public string email;
    public string mobile;

    public Customer(string displayName, string email, string mobile)
    {
        this.displayName = displayName;
        this.email = email;
        this.mobile = mobile;
    }
    public Customer()
    {
       
    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["displayName"] = displayName;
        result["email"] = email;
        result["mobile"] = mobile;
       
        return result;
    }


    public Customer(IDictionary<string, object> dict)
    {
        this.displayName = dict["displayName"].ToString();
        this.email = dict["email"].ToString();
        this.mobile = dict["mobile"].ToString();

    }
}
