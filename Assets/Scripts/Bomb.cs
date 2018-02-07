using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Bomb : MonoBehaviour {

    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 4f;
    public float explosionDamage = 5f;
    public bool explodeOnImpact = false;
    protected Health playerHealth;
    

    public GameObject explosionEffects;
    float countdown;
    bool hasExploded = false;
	// Use this for initialization
	void Start () {
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode(); 
        }
	}

 private void OnTriggerEnter(Collider other)
    {
        //if Bombs hit player, boom.
        if(other.tag == "Player")
        {
            Explode();
            playerHealth.TakeDamage(explosionDamage);
        }

        //If bombs are hit with shockwave, explode.
        if(other.tag == "Shockwave")
        {
            Explode();
        }


    }

    void Explode()
    {
        Instantiate(explosionEffects, transform.position, transform.rotation);


        hasExploded = true;
        Destroy(gameObject);
    }


}
