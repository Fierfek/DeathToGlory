using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 4f;

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
    
    void Explode()
    {
        Instantiate(explosionEffects, transform.position, transform.rotation);

        Collider [] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
        }

        hasExploded = true;
        Destroy(gameObject);
    }
}
