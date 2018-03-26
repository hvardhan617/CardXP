using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

//This class is synonymous to Repository in Spring,wherein the routes to nodes to save and fetch data from are specified.
public class RouterManager : MonoBehaviour
{

    public static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

    public static DatabaseReference Players()
    {
        return baseRef.Child("players");//v need all the children of players node here
    }

    public static DatabaseReference PlayerWithUID(string uid)
    {
        return baseRef.Child("players").Child(uid);//v need all the children of players-->uid here
    }
}