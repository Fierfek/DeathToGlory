using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PillarShake : MonoBehaviour {

    public float shakeSpeed = 5;
    
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
            Debug.Log("Pillar hit with Shockwave");
        }
    }

    void ShakePillar()
    {
        float xChange = transform.rotation.x + Random.Range(-10.0f, 10.0f);
        float yChange = transform.rotation.y + Random.Range(-10.0f, 10.0f);
        float zChange = transform.rotation.z + Random.Range(-10.0f, 10.0f);
        Vector3 change = new Vector3(xChange, yChange, zChange);
        transform.Rotate(change);
        
    }
}
