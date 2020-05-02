using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class InitManager : MonoBehaviour
{
    public MainResponse MainResponseInstance;
   
    public static string ServerURL =
        "https://sheets.googleapis.com/v4/spreadsheets/1Rcaj5TKA-VoLo4olCCNEBXvE_YYm5KCgNkuEaF4r2VU/values/Sheet1!A2:Z999?key=AIzaSyAP5QlsW0G-tnYAZ5Cx8aZvl_wYzOKZWy4";

	public static string ServerURL_Actual =
       "https://sheets.googleapis.com/v4/spreadsheets/1Rcaj5TKA-VoLo4olCCNEBXvE_YYm5KCgNkuEaF4r2VU/values/Sheet2!A2:Z999?key=AIzaSyAP5QlsW0G-tnYAZ5Cx8aZvl_wYzOKZWy4";
    // Use this for initialization
    // Use this for initialization

    [Header("#### GAME DETAILS ######")]
    public string BundleID;
    public string Platform;
    public string AdmobAppID;
    public string BannerID;
    public string InterstitialID;
    public static InitManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    
   

    void Start ()
    {

    }

   
}

[System.Serializable]
public class MainResponse
{
    public string range;
    public string majorDimension;
    public MainResponseValues[] values;
}

[System.Serializable]
public class MainResponseValues
{
    public string number;
    public string platform;
    public string game_name;  
    public string bundleID;
    public string admob_appID;
    public string banner;
    public string interstitial;
}



/*
{
editor check
get data
if data recieved and found then set data
else set default data
}
 */
