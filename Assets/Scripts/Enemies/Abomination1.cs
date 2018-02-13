using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination1 : Enemy {

    public float throwForce = 10f;
    public float throwForce2 = 5f;
    public float throwRange = 10f;
    public float throwsPerPeriod = 2f;
    public float cooldown = 5f;//Attack Cooldown period
    float timer;//Attack timer


    public int bombCount = 0;


    Animator anim;
    public GameObject grenadePrefab;
    public GameObject shockwave;

    bool hasAttacked = false;
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
        timer = cooldown;

        //This is just for testing purposes with throwing bombs.
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
            timer -= Time.deltaTime;
           if(timer <= 0)//This part controls attack cooldown
            {
                hasAttacked = false;
            }
            if (getDistToPlayer() <= attackRange && !hasAttacked)
            {

                FireMaul();

                //animation
                isAttacking = true;
                hasAttacked = true;
                timer = 4f;
            }

            else if(getDistToPlayer() <= throwRange && !hasAttacked && bombCount <20)
            {

                ThrowBomb();
                //Animation goes here
                isAttacking = true;
                hasAttacked = true;
                timer = 1f;
            }

            else
            {
                isAttacking = false;
                //anim.SetTrigger("AbomRun");

            }
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) 
            // Was testing how it worked with movement
            if(getDistToPlayer() >= 4f)
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

        GameObject firemaul = (GameObject) Instantiate(shockwave, transform.position + new Vector3(0, -1 , 0), transform.rotation);
        firemaul.transform.eulerAngles = new Vector3(90, 0, 0);
        //If in shockwave, do damage and trigger nearby bombs
        
    }

    //This is self explanatory. You throw a bomb with a certain amount of force. The bomb detonates after 
    void ThrowBomb()
    {
        //transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        GameObject bomb = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }


}
