using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

	// Use this for initialization
//	public int LifeTime;
	void Start () 
	{
		Destroy(gameObject,2);
	}
	
}
