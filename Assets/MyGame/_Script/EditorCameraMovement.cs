using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraMovement : MonoBehaviour 
{

	float RotationSpeed=50;
public	Transform CamParent;

	// Use this for initialization
	void Start () 
	{
		Invoke("findCam", 2);
	}

    void findCam()
    {
		CamParent = GameObject.Find("camParent").transform;
	}
	// Update is called once per frame
	void Update () 
	{
        if(CamParent)
        {
			if (Input.GetKey(KeyCode.W))
			{
				CamParent.Rotate(Vector3.right * Time.deltaTime * RotationSpeed);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				CamParent.Rotate(Vector3.left * Time.deltaTime * RotationSpeed);
			}
			else if (Input.GetKey(KeyCode.A))
			{
				CamParent.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				CamParent.Rotate(Vector3.down * Time.deltaTime * RotationSpeed);
			}
		}
      
	}
}
