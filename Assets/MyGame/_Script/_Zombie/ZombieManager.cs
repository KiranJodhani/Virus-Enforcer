using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour 
{
	[Header("************** ANIMATION ***************")]
	public Animator ZombieAnimator;


	[Header("************** LIFE ***************")]
	public float ZombieLife;
	public float  DamageFactor;
	public Collider ZombieCollider;
	public Vector3 StartPositon;


	[Header("************** AUDIO ***************")]
	public AudioSource ZombieAudioSource;

	public AudioClip IdleClip;
	public AudioClip AttackClip;
	public AudioClip InjuredClip;
	public AudioClip DeathClip;

	public bool CanAttack;
	public bool IsDead;
	// Use this for initialization
	void Start () 
	{
		ZombieAudioSource=GetComponent<AudioSource>();
		ZombieAudioSource.playOnAwake=true;
		ZombieAnimator=GetComponent<Animator>();
		ZombieCollider=GetComponent<BoxCollider>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameController.Instance)
		{
			GameController.Instance.AlertBar.SetActive(!IsZombieInView());	
		}

		if(IsZombieInView() && ZombieCollider.enabled && !IsDead)
		{
			PlayAttackAnimation();
			PlayAttackSound();
		}
		else if(!IsDead && ZombieCollider.enabled )
		{
			PlayIdleAnimation();
			PlayIdleSound();
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
		ZombieAnimator.SetBool("Idle",false);
		ZombieAnimator.SetBool("Attack",false);
		ZombieAnimator.SetBool("Death",false);
		ZombieAnimator.SetBool("Injured",false);
	}

	void ZombiDamageRecieved()
	{
		ZombieCollider.enabled=false;

		ZombieLife--;
		if(ZombieLife<=0)
		{
			IsDead=true;
			PlayDeathAnimation();
			PlayDeathSound();

		}
		else
		{	
			PlayInjuredAnimation();
			PlayInjuredSound();
			Invoke("CheckZombieHealth",1);
		}
	}

	void CheckZombieHealth()
	{
		ZombieCollider.enabled=true;
	}


	public void PlayIdleAnimation()
	{
		DisableAllAnimation();
		ZombieAnimator.SetBool("Idle",true);
	}

	public void PlayAttackAnimation()
	{
		DisableAllAnimation();
		ZombieAnimator.SetBool("Attack",true);
	}

	public void PlayInjuredAnimation()
	{
		DisableAllAnimation();
		ZombieAnimator.SetBool("Injured",true);

	}

	public void PlayDeathAnimation()
	{
		DisableAllAnimation();
		ZombieAnimator.SetBool("Death",true);
		ZombieCollider.enabled=false;
		Invoke("DestroyZombie",3f);
	}


	/***************  SOUNDS **************/

	public void PlayIdleSound()
	{
		if(ZombieAudioSource.clip!=IdleClip)
		{
			ZombieAudioSource.clip=IdleClip;	
			ZombieAudioSource.Play();
		}

	}

	public void PlayAttackSound()
	{
		if(ZombieAudioSource.clip!=AttackClip)
		{
			ZombieAudioSource.clip=AttackClip;	
			ZombieAudioSource.Play();
		}
	}

	public void PlayInjuredSound()
	{
		if(ZombieAudioSource.clip!=InjuredClip)
		{
			ZombieAudioSource.clip=InjuredClip;	
			ZombieAudioSource.Play();
		}
	}

	public void PlayDeathSound()
	{
		if(ZombieAudioSource.clip!=DeathClip)
		{
			ZombieAudioSource.clip=DeathClip;	
			ZombieAudioSource.Play();
		}
	}

	public	void PlayerDamaged()
	{
		GameController.Instance.PlayerDamagerRecived();
	}

	void DestroyZombie()
	{
		//PhotonAdManager.Instance.ShowInsterstitial ();
		GameController.Instance.SpawnTheZombie(Random.Range(5,8));
		GameController.Instance.IncreasePlayerHealth();
		GameController.Instance.ZombieCounter++;
		//GameController.Instance.ZombieParentRotationSpeed=Random.Range(10,50);
		Destroy(gameObject);
	}



}
