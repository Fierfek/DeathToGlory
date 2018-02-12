using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Bomb : MonoBehaviour {

    public float delay = 10f;
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
        }

        //If bombs are hit with shockwave, explode.
        if(other.tag == "Shockwave")
        {
            Explode();
        }

        //If bombs touch environment and boolean explodeOnImpact is true, bombs blow up.
        if(other.tag == "Environment" && explodeOnImpact)
        {
            Explode();
        }


    }

    void Explode()
    {
        //Creates explosion particle system prefab. The prefab should destroy itself after effects are done.
        Instantiate(explosionEffects, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            //This commented out portion is for dealing out damage with explosion force.
            /*
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Health victim = nearbyObject.GetComponent<Health>();
            if (rb != null && victim != null)
            {
                victim.TakeDamage(explosionDamage);
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
            */
        }

        Debug.Log("Explode");
        hasExploded = true;
        Destroy(gameObject);
    }


}
