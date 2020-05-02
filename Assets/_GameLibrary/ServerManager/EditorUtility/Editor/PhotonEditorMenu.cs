using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(InitManager))]
public class PhotonEditorMenu : Editor
{
    InitManager initManagerInstance;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        initManagerInstance = (InitManager)target;
        if(GUILayout.Button("Set Game Data"))
        {
            GetDataFromServer();
        }
    }

    static void GetDataFromServer()
    {
        GameObject RevenueManager = GameObject.Find("RevenueManager");
        if (RevenueManager)
        {
            RevenueManager.GetComponent<InitManager>().BundleID=PlayerSettings.applicationIdentifier;

            #if UNITY_ANDROID
            RevenueManager.GetComponent<InitManager>().Platform = "a";
            #endif

            #if UNITY_IOS
             RevenueManager.GetComponent<InitManager>().Platform = "i";
            #endif
        }
        WWW www = new WWW(InitManager.ServerURL_Actual);
        ContinuationManager.Add(() => www.isDone, () =>
       {
           if (!string.IsNullOrEmpty(www.error))
           {
               Debug.Log("WWW failed: " + www.error);
               EditorUtility.DisplayDialog("", "Failed!!" + www.error, "", "");
           }

           try
           {
               if (RevenueManager)
               {
                   //RevenueManager.GetComponent<InitManager>().OnDataRecieved(www.text);
                   EditorUtility.DisplayDialog("", "Sucess !! ", "", "");
               }
            
            }
           catch
           {
               Debug.LogError("Data error: could not parse retrieved data as json.");
               EditorUtility.DisplayDialog("", "Parseing Error!! ", "", "");

           }
       }
        );
    }



} 