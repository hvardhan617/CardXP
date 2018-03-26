using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dues  {

    public string dues_Electricity;
    public string dues_CreditCard;
    public string dues_Telephone;

    public Dues(string dues_Electricity, string dues_CreditCard, string dues_Telephone)
    {
        this.dues_Electricity = dues_Electricity;
        this.dues_CreditCard = dues_CreditCard;
        this.dues_Telephone = dues_Telephone;

    }
         public Dues()
    {
       
    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["dues_Electricity"] = dues_Electricity;
        result["dues_CreditCard"] = dues_CreditCard;
        result["dues_Telephone"] = dues_Telephone;

        return result;
    }

    public Dues(IDictionary<string, object> dict)
    {
        this.dues_Electricity = dict["dues_Electricity"].ToString();
        this.dues_CreditCard = dict["dues_CreditCard"].ToString();
        this.dues_Telephone = dict["dues_Telephone"].ToString();

    }
}
