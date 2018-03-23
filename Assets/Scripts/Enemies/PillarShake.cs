using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarShake : MonoBehaviour {

    int range = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shockwave")
        {
            ShakePillar();
        }
    }

    void ShakePillar()
    {
        float xChange = transform.rotation.x + Random.Range(-5.0f, 5.0f);
        float zChange = transform.rotation.z + Random.Range(-5.0f, 5.0f);
        transform.Rotate(xChange, 0, zChange);
        
    }
}
