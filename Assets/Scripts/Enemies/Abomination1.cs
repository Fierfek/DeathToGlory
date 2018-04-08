using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination1 : Enemy {

    public float throwForce = 12f;

    public float throwRange = 10f;

    public float cooldown = 5f;//Attack Cooldown period
    float timer;//Attack timer
    public Vector3 shockWaveTransform = new Vector3(0, .2f, 3f);

    public int bombCount = 0;


    Animator anim;
    public GameObject grenadePrefab;
    public GameObject shockwave;

    bool hasAttacked = false;
	// Use this for initialization
	void Start () {
        movementSpeed = 2.0f;
        damageAmount = 3;
        aggroRange = 15f;
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
            anim.SetTrigger("abomBombDeath");

        }
        //if enemy is in attack animation is won't move
        else if (CheckAgro())
        {

            if (timer <= 0)//This part controls attack cooldown
            {
                hasAttacked = false;
            }
            timer -= Time.deltaTime;


             if (getDistToPlayer() < attackRange && !hasAttacked)
            {
                //transform.LookAt(target);
                anim.SetTrigger("AbomBombMaul");
                FireMaul();

                //animation
                isAttacking = true;
                hasAttacked = true;
                timer = 4f;
            }

            else if(getDistToPlayer() < throwRange && !hasAttacked)
            {
                transform.LookAt(target);
                anim.SetTrigger("AbomBombThrow");
                ThrowBomb();
                //Animation goes here
                isAttacking = true;
                hasAttacked = true;
                timer = .5f;
            }

            if(getDistToPlayer() > throwRange)
            {
                isAttacking = false;
                anim.SetTrigger("AbomBombWalking");

            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            {
                agent.SetDestination(target.position);  //update agent destination to target location
            }
           

        }
        else
        {
            anim.SetTrigger("AbomBombIdle");
        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    void FireMaul()
    {

        GameObject firemaul = (GameObject) Instantiate(shockwave, transform.position + transform.forward * 2 + transform.up *.5f , transform.rotation);
        firemaul.transform.eulerAngles = new Vector3(90, 0, 0);
        //If in shockwave, do damage and trigger nearby bombs
        
    }

    //This is self explanatory. You throw a bomb with a certain amount of force. The bomb detonates after 
    void ThrowBomb()
    {
        
        GameObject bomb = Instantiate(grenadePrefab, transform.position + transform.forward *1 + transform.up, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }


}
