using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPoints  {

    public string rewardPoints;

    public RewardPoints(string rewardPoints)
    {
        this.rewardPoints = rewardPoints;

    }

    public RewardPoints()
    {
       

    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["rewardPoints"] = rewardPoints;

        return result;
    }
    public RewardPoints(IDictionary<string, object> dict)
    {
        this.rewardPoints = dict["rewardPoints"].ToString();
        
    }
}
