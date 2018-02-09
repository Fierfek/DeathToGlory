using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination1 : Enemy {

    public float throwForce = 10f;
    public float throwForce2 = 5f;
    public float throwRange = 10f;
    public float throwsPerPeriod = 2f;
    public float period = 5f;


    Animator anim;
    public GameObject grenadePrefab;
    public GameObject shockwave;

    bool hasBashed = false;
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
        //InvokeRepeating("ThrowBomb", 2.0f, 1f);

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
            if (getDistToPlayer() <= attackRange && !hasBashed)
            {
                FireMaul();
                hasBashed = true;
                //anim.SetTrigger("MaulSwing");
                isAttacking = true;

            }
            else if(getDistToPlayer() <= throwRange)
            {
                if (!hasThrown)
                {
                    //anim.SetTrigger("ThrowBomb");
                    //BombBarrage();
                    //hasThrown = true;
                }
                else
                {

                }
            }

            else
            {
                isAttacking = false;
                //anim.SetTrigger("AbomRun");

            }
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            if(getDistToPlayer() >= 2f)
            {
            //agent.SetDestination(target.position);  //update agent destination to target location
            }


        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    void FireMaul()
    {

        GameObject firemaul = (GameObject) Instantiate(shockwave, transform.position, transform.rotation);

        //If in shockwave, do damage and trigger nearby bombs
        
    }

    void ThrowBomb()
    {
        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        GameObject bomb = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    void ThrowBomb2()
    {

    }

    void BombBarrage(int frequency, float throw1, float throw2)
    {
        float spinAngle = 360f / frequency;
        transform.Rotate(new Vector3(0, spinAngle * Time.deltaTime, 0));

        ThrowBomb();

    }

}
