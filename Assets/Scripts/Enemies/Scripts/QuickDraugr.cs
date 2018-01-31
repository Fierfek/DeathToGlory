using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickDraugr : Enemy {

    float timer;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 4.5f;
        damageAmount = 1;
        aggroRange = 20;
        attackRange = 1.5f;
        name = "QuickDraugr";

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        agent.stoppingDistance = .5f;
        health.SetHealth(10f);


    }

    // Update is called once per frame
    void Update()
    {
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
            else if (getDistToPlayer() > attackRange)
            {
                isAttacking = false;
                anim.SetTrigger("running");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
                agent.SetDestination(target.position);  //update agent destination to target location
            else
                agent.SetDestination(transform.position);

        }
        else
            anim.SetTrigger("idle");

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(target.gameObject))
            playerHealth.TakeDamage(damageAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
			if (other.gameObject.transform == target)
				playerHealth.TakeDamage(damageAmount);
    }
}
