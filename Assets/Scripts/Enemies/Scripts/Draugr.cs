﻿
using UnityEngine;
using UnityEngine.AI;

public class Draugr : Enemy {


    float timer;
    Animator anim;

    // Use this for initialization
    void Start () {
		movementSpeed = 3.5f;
        damageAmount = 1;
        aggroRange = 10;
        attackRange = agent.stoppingDistance;
        name = "Draugr";

        anim = GetComponent<Animator>();
		agent.speed = movementSpeed;
		health.SetHealth(10f);

        
	}
	
	// Update is called once per frame
	void Update () {
        //Check for death
        if (health.GetHealth() <= 0)
        {
            isAttacking = false;
            //die

        }
        //if enemy is in attack animation is won't move
        else if (checkAgro())
        {
            if (getDistToPlayer() <= attackRange)
            {
                anim.SetTrigger("attack");
                isAttacking = true;              
            }
            else if(getDistToPlayer() > attackRange)
            {
                isAttacking = false;
                anim.SetTrigger("running");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
                agent.SetDestination(target.position);  //update agent destination to target location

        }
        else
            anim.SetTrigger("idle");
		
	}    

    //temporary attack "animation" for testing
    private void attack()
    {
        timer += Time.deltaTime;
        target.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
        spin();
        if(timer >= 2)
        {
            isAttacking = false;
        }
    }

    private void spin()
    {
        transform.Rotate(transform.eulerAngles + new Vector3(0, .1f, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(target.gameObject))
            playerHealth.TakeDamage(damageAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == target)
            playerHealth.TakeDamage(damageAmount);
    }
}
