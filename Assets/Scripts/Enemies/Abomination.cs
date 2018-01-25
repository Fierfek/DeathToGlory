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
        //Check for death
        if (health.GetHealth() <= 0)
        {
            isAttacking = false;
            anim.SetTrigger("gruntDeath");

        }
        //if enemy is in attack animation is won't move
        else if (CheckAgro())
        {
            if (getDistToPlayer() <= attackRange)
            {

                anim.SetTrigger("AbomThrow");
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
                anim.SetTrigger("AbomRun");

            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                agent.SetDestination(target.position);  //update agent destination to target location

        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    void FireMaul()
    {
        timer += Time.deltaTime;
        
    }

    void ThrowBomb()
    {
        timer += Time.deltaTime;
        GameObject bomb = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
