using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MainMenuManager : MonoBehaviour 
{
	public GameObject SoundOn;
	public GameObject Soundff;
	public GameObject LoadingBar;
	// Use this for initialization
	void Start () 
	{
		CheckSoundStatus();

        #if PLATFORM_ANDROID
	        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
	        {
		        Permission.RequestUserPermission(Permission.Camera);
	        }
        #endif

	}

	void CheckSoundStatus()
	{
		if(SharedScript.IsSoundOn)
		{
			AudioListener.volume=1;
			Soundff.SetActive(true);
			SoundOn.SetActive(false);
		}	
		else
		{
			AudioListener.volume=0;
			Soundff.SetActive(false);
			SoundOn.SetActive(true);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnPlayButtonClicked()
	{
		LoadingBar.SetActive(true);
		Invoke("LoadMainGameScene",0.2f);

	}
	void LoadMainGameScene()
	{
		SceneManager.LoadScene("MainGameScene");	
	}

	public void OnRateButtonClicked()
	{
		
	}

	public void OnMoreButtonClicked()
	{
		
	}

	public void OnSoundButtonClicked()
	{
		SharedScript.IsSoundOn =!SharedScript.IsSoundOn;
		CheckSoundStatus();
	}
}
