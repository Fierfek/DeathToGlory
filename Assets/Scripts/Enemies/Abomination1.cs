using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination1 : Enemy {

    public float throwForce = 10f;
    public float throwForce2 = 5f;
    public float throwRange = 10f;


    Animator anim;
    public GameObject grenadePrefab;
    //public GameObject shockwave;

    bool hasThrown = false;
	// Use this for initialization
	void Start () {
        movementSpeed = 2.0f;
        damageAmount = 3;
        aggroRange = 8f;
        attackRange = 4f;
        name = "Abomination 1";

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(20f);
        InvokeRepeating("ThrowBomb", 2.0f, 3.0f);

    }
	
	// Update is called once per frame
	void Update () {
        //Check for death
        if (health.GetHealth() <= 0)
        {
            isAttacking = false;
            //anim.SetTrigger("gruntDeath");

        }
        //if enemy is in attack animation is won't move
        else if (CheckAgro())
        {
            if (getDistToPlayer() <= attackRange)
            {

                //anim.SetTrigger("MaulSwing");
                isAttacking = true;

            }
            else if(getDistToPlayer() <= throwRange)
            {
                if (!hasThrown)
                {
                    //anim.SetTrigger("ThrowBomb");
                    //ThrowBomb();
                    //hasThrown = true;
                }
            }

            else
            {
                isAttacking = false;
                //anim.SetTrigger("AbomRun");

            }
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                agent.SetDestination(target.position);  //update agent destination to target location

        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    void FireMaul()
    {

        //GameObject firemaul = (GameObject) Instantiate(shockwave);

        //If in shockwave, do damage and trigger nearby bombs
        
    }

    void ThrowBomb(float forceThrow)
    {
        GameObject bomb = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forceThrow, ForceMode.VelocityChange);
    }



}
