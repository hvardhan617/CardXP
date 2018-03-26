using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootObject 
{

    public Customer customer;
    public Balance balance;
    public Dues dues;
    public RewardPoints points;
    public MonthlyExpenditure xpenses;

    public RootObject(Customer customer, Balance balance, Dues dues, RewardPoints points, MonthlyExpenditure xpenses)
    {
        this.customer = customer;
        this.balance = balance;
        this.dues = dues;
        this.points = points;
        this.xpenses = xpenses;

    }
    public RootObject()
    {

    }


}
