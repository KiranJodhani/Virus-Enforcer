using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus_Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 0;
    public Transform Player;
    void Start()
    {
        speed = Random.Range(50, 100);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        if(Player)
        {
            transform.LookAt(Player);
        }
       
    }
}
