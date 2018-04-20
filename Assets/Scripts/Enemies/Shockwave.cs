using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

    public float timeToDestroy = 8f;

    public float expansionFactor = 2f;
	// Use this for initialization
	void Start () {
        DestroyShock();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale += new Vector3(expansionFactor * Time.deltaTime, expansionFactor * Time.deltaTime, (Time.deltaTime * 1.5f));
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player in Shockwave");
            //Knockback and damage
        }

        if(other.tag == "Bomb")
        {
            Debug.Log("Bomb in Shockwave");
        }


    }

    void DestroyShock()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
