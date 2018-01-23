using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination : Enemy {

    public float throwForce = 20f;
    public float timer;
    Animator anim;
    public GameObject grenadePrefab;
	// Use this for initialization
	void Start () {
        movementSpeed = 2.0f;
        damageAmount = 3;
        aggroRange = 8;
        attackRange = 4f;
        name = "Abomination";

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(20f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ThrowBomb()
    {
        GameObject bomb = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
