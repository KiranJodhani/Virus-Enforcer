using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusManager : MonoBehaviour
{


	[Header("************** LIFE ***************")]
	public float ZombieLife;
	public float DamageFactor;
	public Collider ZombieCollider;
	public Color DamageColor;
	public Color RegularColor;
	public MeshRenderer meshreder;
	public float DistanceFromPlayer=100;
	//public Vector3 StartPositon;


	[Header("************** AUDIO ***************")]
	public AudioSource ZombieAudioSource;

	public AudioClip IdleClip;
	public AudioClip AttackClip;
	public AudioClip InjuredClip;
	public AudioClip DeathClip;

	public bool CanAttack;
	public bool IsDead;
	// Use this for initialization
	void Start()
	{
		ZombieAudioSource = GetComponent<AudioSource>();
        ZombieAudioSource.playOnAwake = true;
        ZombieCollider = GetComponent<BoxCollider>();
		meshreder.sharedMaterial.color = RegularColor;
	}

	// Update is called once per frame
	void Update()
	{
		if (GameController.Instance)
		{
			GameController.Instance.AlertBar.SetActive(!IsZombieInView());
		}

		if (IsZombieInView() && ZombieCollider.enabled && !IsDead)
		{
			Vector3 VirusPos = transform.localPosition;
			DistanceFromPlayer = Vector3.Distance(transform.localPosition, Vector3.zero);

			if (DistanceFromPlayer>1.5f)
            {
				transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.003f);
			}
            else
            {
				//RevenueManager.Instance.ShowInterstitialAd();
				PlayerDamaged();
				DestroyZombie();
			}
			
			
			//PlayAttackAnimation();
			//PlayAttackSound();
		}
		else if (!IsDead && ZombieCollider.enabled)
		{
			PlayIdleAnimation();
			//PlayIdleSound();
		}
	}


	private bool IsZombieInView()
	{
		Vector3 viewPos = GameController.Instance.ARCam.WorldToViewportPoint(ZombieCollider.bounds.center);
		return (viewPos.x > 0.0f && viewPos.x < 1.0f && viewPos.y > 0.0f && viewPos.y < 1.0f && viewPos.z > 1.0f);
	}


	/***************  ANIMATIONS **************/
	void DisableAllAnimation()
	{
		//ZombieAnimator.SetBool("Idle", false);
		//ZombieAnimator.SetBool("Attack", false);
		//ZombieAnimator.SetBool("Death", false);
		//ZombieAnimator.SetBool("Injured", false);
	}

	void ZombiDamageRecieved()
	{
		ZombieCollider.enabled = false;

		ZombieLife--;
		SetDamageColor();
		if (ZombieLife <= 0)
		{
			IsDead = true;
			PlayDeathAnimation();
			PlayDeathSound();

		}
		else
		{
			PlayInjuredAnimation();
			PlayInjuredSound();
			Invoke("CheckZombieHealth", 1);
		}
	}

	void CheckZombieHealth()
	{
		ZombieCollider.enabled = true;
	}


	public void PlayIdleAnimation()
	{
		DisableAllAnimation();
		//ZombieAnimator.SetBool("Idle", true);
	}

	public void PlayAttackAnimation()
	{
		DisableAllAnimation();
		//ZombieAnimator.SetBool("Attack", true);
	}

	public void PlayInjuredAnimation()
	{
		DisableAllAnimation();
		//ZombieAnimator.SetBool("Injured", true);

	}

	public void PlayDeathAnimation()
	{
		DisableAllAnimation();
		//ZombieAnimator.SetBool("Death", true);
		ZombieCollider.enabled = false;
		Invoke("DestroyZombie", 0f);
	}


	/***************  SOUNDS **************/

	public void PlayIdleSound()
	{
		if (ZombieAudioSource.clip != IdleClip)
		{
			ZombieAudioSource.clip = IdleClip;
			ZombieAudioSource.Play();
		}
	}

	public void PlayAttackSound()
	{
		if (ZombieAudioSource.clip != AttackClip)
		{
			ZombieAudioSource.clip = AttackClip;
			ZombieAudioSource.Play();
		}
	}

	public void PlayInjuredSound()
	{
		if (ZombieAudioSource.clip != InjuredClip)
		{
			ZombieAudioSource.clip = InjuredClip;
			ZombieAudioSource.Play();
		}
	}

	public void PlayDeathSound()
	{
		if (ZombieAudioSource.clip != DeathClip)
		{
			ZombieAudioSource.clip = DeathClip;
			ZombieAudioSource.Play();
		}
	}

	public void PlayerDamaged()
	{
		GameController.Instance.PlayerDamagerRecived();
	}

	void DestroyZombie()
	{
		//PhotonAdManager.Instance.ShowInsterstitial();
		GameController.Instance.SpawnTheZombie(Random.Range(5, 8));
		GameController.Instance.IncreasePlayerHealth();
		GameController.Instance.ZombieCounter++;
		Destroy(gameObject);
	}

    void SetDamageColor()
    {
        if(meshreder.sharedMaterial.color!=DamageColor)
        {
			meshreder.sharedMaterial.color = DamageColor;
			Invoke("SetNormalColor", 0.2f);
		}
	}

    void SetNormalColor()
    {
		meshreder.sharedMaterial.color = RegularColor;
	}
}
