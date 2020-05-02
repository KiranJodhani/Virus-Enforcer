using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAds : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        RevenueManager.Instance.ShowInterstitialAd();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
