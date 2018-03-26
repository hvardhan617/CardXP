using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthlyExpenditure  {

    public string week1;
    public string week2;
    public string week3;
    public string week4;

    public MonthlyExpenditure(string week1, string week2, string week3, string week4)
    {
        this.week1 = week1;
        this.week2 = week2;
        this.week3 = week3;
        this.week4 = week4;
    }
        public MonthlyExpenditure()
        {
           
        }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["week1"] = week1;
        result["week2"] = week2;
        result["week3"] = week3;
        result["week4"] = week4;
        return result;
    }

    public MonthlyExpenditure(IDictionary<string, object> dict)
    {
        this.week1 = dict["week1"].ToString();
        this.week2 = dict["week2"].ToString();
        this.week3 = dict["week3"].ToString();
        this.week4 = dict["week4"].ToString();

    }

}
