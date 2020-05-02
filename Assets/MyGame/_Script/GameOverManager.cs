using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour 
{
	public Text OldScoreLable;
	public Text NewScoreLable;
	// Use this for initialization
	void Start () 
	{
		RevenueManager.Instance.ShowInterstitialAd();
		if(PlayerPrefs.HasKey(SharedScript.SCOREKEY))
		{
			int SavedScore = PlayerPrefs.GetInt(SharedScript.SCOREKEY);
			int newScore = GameController.Instance.Score;

			if(newScore>SavedScore)
			{
				OldScoreLable.text= "Score : "+SavedScore.ToString();
				NewScoreLable.text="High Score : "+GameController.Instance.Score.ToString();

				PlayerPrefs.SetInt(SharedScript.SCOREKEY,newScore);
				PlayerPrefs.Save();

			}
			else
			{
				OldScoreLable.text= "Score : "+SavedScore.ToString();
				NewScoreLable.text="New Score : "+GameController.Instance.Score.ToString();

				PlayerPrefs.SetInt(SharedScript.SCOREKEY,SavedScore);
				PlayerPrefs.Save();
			}

		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ReloadTheGame()
	{
		SceneManager.LoadScene("MainGameScene");	
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}
}
