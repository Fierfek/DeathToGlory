using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargedDraugr : Enemy {


    float timer;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 1.5f;
        damageAmount = 5;
        aggroRange = 100;
        attackRange = agent.stoppingDistance;
        name = "EnlargedDraugr";

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(20f);


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
                anim.SetTrigger("walk");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                agent.SetDestination(target.position);  //update agent destination to target location

        }
        else
            anim.SetTrigger("idle");

    }

    //true is in aggro range
    private bool checkAgro()
    {
        return aggroRange > getDistToPlayer();
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
