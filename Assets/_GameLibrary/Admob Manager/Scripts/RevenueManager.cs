using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class RevenueManager : MonoBehaviour 
{
	[Header("###### ADMOB IDS ################")]
	public string AndroidAppID;
	public string AndroidBannerID;
	public string AndroidInterstitialID;

	public string iOSAppID;
	public string iOSBannerID;
	public string iOSInterstitialID;

	public bool IsBannerLoaded=false;
    public Text Bannerlog;
    public Text fulladlog;

	private BannerView bannerView;
	private InterstitialAd interstitial;
	private RewardedAd rewardedAd;
	private float deltaTime = 0.0f;
	private static string outputMessage = string.Empty;
	public static RevenueManager Instance;
	

	public void Awake()
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);
	}

    public void InitAdmob()
    {
        Invoke("LetTheGameBegin", 1);
        #if UNITY_ANDROID
                string appId = AndroidAppID;
        #elif UNITY_IPHONE
            string appId = iOSAppID;
        #else
                string appId = "unexpected_platform";
        #endif

        MobileAds.SetiOSAppPauseOnBackground(true);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.CreateAndLoadRewardedAd();

        ShowBanner();
        LoadInterstitial();
    }


    void LetTheGameBegin()
	{
		Application.LoadLevel (1);
	}
	public void Start()
	{
        InitAdmob();

    }

		public void ShowBanner()
		{
			RequestBanner ();
		}

		public void HideBanner()
		{
			if (IsBannerLoaded) 
			{
				bannerView.Destroy();
				IsBannerLoaded = false;
			}
		}

		public void LoadInterstitial()
		{
			RequestInterstitial();
		}

		public void ShowInterstitialAd()
		{
        print("#### SHOW");
        #if !UNITY_EDITOR
            ShowInterstitial();
        #endif

    }



		// Returns an ad request with custom ad targeting.
		private AdRequest CreateAdRequest()
		{
		return new AdRequest.Builder()
		.AddTestDevice(AdRequest.TestDeviceSimulator)
		.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
		.AddKeyword("game")
		.SetGender(Gender.Male)
		.SetBirthday(new DateTime(1985, 1, 1))
		.TagForChildDirectedTreatment(false)
		.AddExtra("color_bg", "9B30FF")
		.Build();
		}

		private void RequestBanner()
		{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR
		string adUnitId = "unused";
		//

		#elif UNITY_ANDROID
		string adUnitId = AndroidBannerID;
		#elif UNITY_IPHONE
		string adUnitId = iOSBannerID;
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Clean up banner ad before creating a new one.
		if (this.bannerView != null)
		{
		this.bannerView.Destroy();
		}

		// Create a 320x50 banner at the top of the screen.
		this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

		// Register for ad events.
		this.bannerView.OnAdLoaded += this.HandleAdLoaded;
		this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		this.bannerView.OnAdOpening += this.HandleAdOpened;
		this.bannerView.OnAdClosed += this.HandleAdClosed;
		this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

		// Load a banner ad.
		this.bannerView.LoadAd(this.CreateAdRequest());
		}

		private void RequestInterstitial()
		{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR


		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = AndroidInterstitialID;
		#elif UNITY_IPHONE
		string adUnitId =iOSInterstitialID;
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Clean up interstitial ad before creating a new one.
		if (this.interstitial != null)
		{
		this.interstitial.Destroy();
		}

		// Create an interstitial.
		this.interstitial = new InterstitialAd(adUnitId);

		// Register for ad events.
		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
		this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

		// Load an interstitial ad.
		this.interstitial.LoadAd(this.CreateAdRequest());
		}

		public void CreateAndLoadRewardedAd()
		{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/5224354917";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/1712485313";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		// Create new rewarded ad instance.
		this.rewardedAd = new RewardedAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = this.CreateAdRequest();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);
		}

		private void ShowInterstitial()
		{
		if (this.interstitial.IsLoaded())
		{
		this.interstitial.Show();
		}
		else
		{
		MonoBehaviour.print("Interstitial is not ready yet");
		}
		}

		private void ShowRewardedAd()
		{
		if (this.rewardedAd.IsLoaded())
		{
		this.rewardedAd.Show();
		}
		else
		{
		MonoBehaviour.print("Rewarded ad is not ready yet");
		}
		}

		#region Banner callback handlers

		public void HandleAdLoaded(object sender, EventArgs args)
		{
		IsBannerLoaded = true;
        if(Bannerlog)
        {
            Bannerlog.text = "Success";
        }
		MonoBehaviour.print("HandleAdLoaded event received");

		}

		public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{

        if (Bannerlog)
        {
            Bannerlog.text = "failed "+args.Message;
        }
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
		}

		public void HandleAdOpened(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleAdOpened event received");
		}

		public void HandleAdClosed(object sender, EventArgs args)
		{
        if (Bannerlog)
        {
            Bannerlog.text = "closed " ;
        }
        MonoBehaviour.print("HandleAdClosed event received");
		}

		public void HandleAdLeftApplication(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleAdLeftApplication event received");
		}

		#endregion

		#region Interstitial callback handlers

		public void HandleInterstitialLoaded(object sender, EventArgs args)
		{

        if(fulladlog)
        {
            fulladlog.text = "Success";
        }
		MonoBehaviour.print("HandleInterstitialLoaded event received");

		}

		public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
        if (fulladlog)
        {
            fulladlog.text = "Failed"+args.Message;
        }

        MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
		}

		public void HandleInterstitialOpened(object sender, EventArgs args)
		{

		MonoBehaviour.print("HandleInterstitialOpened event received");
		}

		public void HandleInterstitialClosed(object sender, EventArgs args)
		{
        if (fulladlog)
        {
            fulladlog.text = "closed";
        }

        MonoBehaviour.print("HandleInterstitialClosed event received");
		LoadInterstitial ();
		}

		public void HandleInterstitialLeftApplication(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
		}

		#endregion

		#region RewardedAd callback handlers

		public void HandleRewardedAdLoaded(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
		}

		public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
		{
		MonoBehaviour.print(
		"HandleRewardedAdFailedToLoad event received with message: " + args.Message);
		}

		public void HandleRewardedAdOpening(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
		}

		public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
		{
		MonoBehaviour.print(
		"HandleRewardedAdFailedToShow event received with message: " + args.Message);
		}

		public void HandleRewardedAdClosed(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
		}

		public void HandleUserEarnedReward(object sender, Reward args)
		{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print(
		"HandleRewardedAdRewarded event received for "
		+ amount.ToString() + " " + type);
		}

		#endregion
}
