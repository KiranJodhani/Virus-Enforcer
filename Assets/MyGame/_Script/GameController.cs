using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{

	public static GameController Instance;

	[Header("************ VIEW PORT *********")]
	public GameObject AlertBar;

	public GameObject HelpScreen;
    public Camera ARCam;


    [Header("************** ZOMBIE ***************")]
	public GameObject[] ZombieList;
	public GameObject[] ZombieSpawnPoint;
	//public GameObject ZombieParent;
	public GameObject ZombieHitParticle;
	//public float ZombieParentRotationSpeed;
	public int ZombieCounter=1;

	[Header("************** WEAPON MANAGER ***************")]
	public GameObject FireParticles;
	public bool IsFiring=false;
	private RaycastHit hit;
	public Camera FireCam;
	public AudioSource WeaponAudioSource;



	[Header("************** SCORE ***************")]
	public int Score;
	public Text ScoreLable;
	public float Health;
	public Image PlayerHealthBar;
	public GameObject PlayerBloodScreen;
	public Image ZombieHealthBar;
	public float ZombieDamageFactor;
	void Awake()
	{
		Instance=this;
	}
	// Use this for initialization

	void Start () 
	{
		SpawnTheZombie(3);
		HelpScreen.SetActive (false);
		if (PlayerPrefs.HasKey ("HelpScreen"))
		{
			
		}
		else
		{
			HelpScreen.SetActive (true);
			PlayerPrefs.SetString ("HelpScreen", "HelpScreenfff");
			PlayerPrefs.Save ();
			Invoke ("HideHelpScreen", 3);
			
		}
	}


	void HideHelpScreen()
	{
		HelpScreen.SetActive (false);
	}
	// Update is called once per frame
	void Update () 
	{
		//ZombieParent.transform.Rotate(Vector3.up*Time.deltaTime*ZombieParentRotationSpeed);
		if(IsFiring)
		{
			ActualFire();
		}

		WeaponAudioSource.enabled=IsFiring;
	}


	public void ActualFire ()
	{
		Vector3 dir = transform.TransformDirection (Vector3.forward) * 100;
		Ray ray = FireCam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
		if (Physics.Raycast (ray, out hit))
		{
			Debug.DrawRay (transform.position, dir, Color.green);
			string HittedObjTag = hit.transform.tag;
			Transform HittedObjTrans = hit.transform;
			Vector3 HitPosition = hit.point;

			if(HittedObjTag=="Zombie")
			{
				Vector3 BloodExloPosition = hit.point;
				GameObject TempHit = Instantiate(ZombieHitParticle);
				TempHit.transform.localPosition=BloodExloPosition;
				//TempHit.transform.parent=ZombieParent.transform;
				TempHit.SetActive(true);
				Score++;
				ScoreLable.text=Score.ToString();
				ZombieHealthBar.fillAmount =ZombieHealthBar.fillAmount-ZombieDamageFactor;
				HittedObjTrans.SendMessage("ZombiDamageRecieved");
			}
		}
	}

	public void SpawnTheZomieDelayed()
	{
        int RandomZombie = Random.Range(0, ZombieList.Length);
        //int RandomZombie = 0;
        GameObject SpawnedZombie = Instantiate(ZombieList[RandomZombie]) as GameObject;
		//ZombieParentRotationSpeed=0;
		SpawnedZombie.GetComponent<VirusManager>().ZombieLife=ZombieCounter;
		ZombieDamageFactor=1/SpawnedZombie.GetComponent<VirusManager>().ZombieLife;
		SpawnedZombie.transform.localPosition = ZombieSpawnPoint[Random.Range(0,ZombieSpawnPoint.Length)].transform.localPosition;
		SpawnedZombie.transform.localEulerAngles = Vector3.zero;
		SpawnedZombie.SetActive(true);
		ZombieHealthBar.fillAmount=1;
	}

	public	void SpawnTheZombie(int Delayed)
	{
		Invoke("ShowAlert",2);
		Invoke("SpawnTheZomieDelayed",Delayed);
	}

	void ShowAlert()
	{
		GameController.Instance.AlertBar.SetActive(true);
	}


	public void StartFiring()
	{
		CancelInvoke("StopFiringDelayed");
		FireParticles.SetActive(true);
		IsFiring=true;
		
	}

	public void StopFiring()
	{
		Invoke("StopFiringDelayed",0.2f);
	}

	void StopFiringDelayed()
	{
		FireParticles.SetActive(false);
		IsFiring=false;
	}

	public	void IncreasePlayerHealth()
	{
		//if(Health<1)
		//{
		//	Health = Health+0.1f;
		//	PlayerHealthBar.fillAmount=Health;
		//}
	}


	public	void PlayerDamagerRecived()
	{		
		Health = Health-0.1f;
		Health = 0;
		PlayerHealthBar.fillAmount=Health;
		ShowPlayerBloodScreen();
		if(Health<=0)
		{
			//Game over
			PlayerPrefs.SetInt(SharedScript.SCOREKEY,Score);
			PlayerPrefs.Save();
			SceneManager.LoadScene("GameOverScene");
		}
	}



	void ShowPlayerBloodScreen()
	{
		PlayerBloodScreen.SetActive(true);
		Invoke("HidePlayerBloodScreen",0.3f);
	}

	void HidePlayerBloodScreen()
	{
		PlayerBloodScreen.SetActive(false);
	}


}
