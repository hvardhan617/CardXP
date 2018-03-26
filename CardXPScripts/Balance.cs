using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance
{
    public string currentBalance;
    public string b1;
    public string b2;
    public string b3;
    public string b4;

    public Balance(string currentBalance,string b1, string b2, string b3, string b4)
    {
        this.currentBalance = currentBalance;
        this.b1 = b1;
        this.b2 = b2;
        this.b3 = b3;
        this.b4 = b4;

    }

    public Balance()
    {


    }

    public Balance(IDictionary<string, object> dict)
    {
        this.b1 = dict["currentBalance"].ToString();
        this.b1 = dict["b1"].ToString();
        this.b1 = dict["b2"].ToString();
        this.b1 = dict["b3"].ToString();
        this.b1 = dict["b4"].ToString();

    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["currentBalance"] = currentBalance;
        result["b1"] = b1;
        result["b2"] = b2;
        result["b3"] = b3;
        result["b4"] = b4;

        return result;
    }
}
    
