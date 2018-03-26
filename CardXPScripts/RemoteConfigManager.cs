using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RemoteConfigManager : MonoBehaviour
{
    float lowerMenuB1_R;
    float lowerMenuB1_G;
    float lowerMenuB1_B;

    float lowerMenuB2_R;
    float lowerMenuB2_G;
    float lowerMenuB2_B;

    float lowerMenuB3_R;
    float lowerMenuB3_G;
    float lowerMenuB3_B;

    float lowerMenuB4_R;
    float lowerMenuB4_G;
    float lowerMenuB4_B;

    void Start()
    {
        initializeRemoteConfigDefaults();
      
    }

    // Initialize remote config, and set the default values.
    void initializeRemoteConfigDefaults()
    {
        System.Collections.Generic.Dictionary<string, object> defaults =
          new System.Collections.Generic.Dictionary<string, object>();

        // These are the values that are used if we haven't fetched data from the
        // server yet, or if we ask for values that the server doesn't have:
        defaults.Add("lowerMenuB1_R", "107");
        defaults.Add("lowerMenuB1_G", "51");
        defaults.Add("lowerMenuB1_B", "15");

        defaults.Add("lowerMenuB2_R", "139");
        defaults.Add("lowerMenuB2_G", "69");
        defaults.Add("lowerMenuB2_B", "23");

        defaults.Add("lowerMenuB3_R", "185");
        defaults.Add("lowerMenuB3_G", "105");
        defaults.Add("lowerMenuB3_B", "52");

        defaults.Add("lowerMenuB4_R", "248");
        defaults.Add("lowerMenuB4_G", "167");
        defaults.Add("lowerMenuB4_B", "87");



        Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
        Debug.Log("RemoteConfig configured and ready!");
    }


    // Start a fetch request.
    public void FetchData()
    {
        Debug.Log("Fetching data...");
        // FetchAsync only fetches new data if the current data is older than the provided
        // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
        // By default the timespan is 12 hours, and for production apps, this is a good
        // number.  For this example though, it's set to a timespan of zero, so that
        // changes in the console will always show up immediately.
        System.Threading.Tasks.Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync(
            TimeSpan.Zero);
        fetchTask.ContinueWith(FetchComplete);
    }

    void FetchComplete(Task fetchTask)
    {
        if (fetchTask.IsCanceled)
        {
            Debug.Log("Fetch cancelled.");
        }
        else if (fetchTask.IsFaulted)
        {
            Debug.Log("Fetch encountered an error.");
        }
        else if (fetchTask.IsCompleted)
        {
            Debug.Log("Fetch completed successfully!");
        }

        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.Info;
        switch (info.LastFetchStatus)
        {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
                Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                       info.FetchTime));
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case Firebase.RemoteConfig.FetchFailureReason.Error:
                        Debug.Log("Fetch failed for unknown reason");
                        break;
                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                        break;
                }
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
                Debug.Log("Latest Fetch call still pending.");
                break;
        }
    }


    // Display the currently loaded data.  If fetch has been called, this will be
    // the data fetched from the server.  Otherwise, it will be the defaults.
    // Note:  Firebase will cache this between sessions, so even if you haven't
    // called fetch yet, if it was called on a previous run of the program, you
    //  will still have data from the last time it was run.
    public RemoteConfigParameters DisplayData(RemoteConfigParameters remoteConfigParameters)
    {
        Debug.Log("Current Data:");

        lowerMenuB1_R = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB1_R").LongValue;
        lowerMenuB1_G = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB1_G").LongValue;
        lowerMenuB1_B = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB1_B").LongValue;

        lowerMenuB2_R = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB2_R").LongValue;
        lowerMenuB2_G = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB2_G").LongValue;
        lowerMenuB2_B = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB2_B").LongValue;

        lowerMenuB3_R = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB3_R").LongValue;
        lowerMenuB3_G = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB3_G").LongValue;
        lowerMenuB3_B = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB3_B").LongValue;

        lowerMenuB4_R = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB4_R").LongValue;
        lowerMenuB4_G = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB4_G").LongValue;
        lowerMenuB4_B = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("lowerMenuB4_B").LongValue;

        // Debug.Log("lowerMenu_b1: " + lowerMenub1);Debug.Log("lowerMenu_b2: " + lowerMenub2); Debug.Log("lowerMenu_b3: " + lowerMenub3); Debug.Log("lowerMenu_b4: " + lowerMenub4);

        remoteConfigParameters.lowerMenuB1_R = lowerMenuB1_R;
        remoteConfigParameters.lowerMenuB1_G = lowerMenuB1_G;
        remoteConfigParameters.lowerMenuB1_B = lowerMenuB1_B;

        remoteConfigParameters.lowerMenuB2_R = lowerMenuB2_R;
        remoteConfigParameters.lowerMenuB2_G = lowerMenuB2_G;
        remoteConfigParameters.lowerMenuB2_B = lowerMenuB2_B;

        remoteConfigParameters.lowerMenuB3_R = lowerMenuB3_R;
        remoteConfigParameters.lowerMenuB3_G = lowerMenuB3_G;
        remoteConfigParameters.lowerMenuB3_B = lowerMenuB3_B;

        remoteConfigParameters.lowerMenuB4_R = lowerMenuB4_R;
        remoteConfigParameters.lowerMenuB4_G = lowerMenuB4_G;
        remoteConfigParameters.lowerMenuB4_B = lowerMenuB4_B;

        return remoteConfigParameters;
    }

    public void DisplayAllKeys()
    {
        Debug.Log("Current Keys:");
        System.Collections.Generic.IEnumerable<string> keys =
            Firebase.RemoteConfig.FirebaseRemoteConfig.Keys;
        foreach (string key in keys)
        {
            Debug.Log("    " + key);
        }
        Debug.Log("GetKeysByPrefix(\"lowerMenu_b\"):");
        keys = Firebase.RemoteConfig.FirebaseRemoteConfig.GetKeysByPrefix("lowerMenu_b");
        foreach (string key in keys)
        {
            Debug.Log("    " + key);
        }
    }

}
