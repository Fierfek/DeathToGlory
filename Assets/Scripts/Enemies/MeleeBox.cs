using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBox : MonoBehaviour {

    public float meleeDamage = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Do Damage
            Debug.Log("Player Damage: " + meleeDamage);
        }
    }
}
