using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickDraugr : Enemy {

    Animator anim;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 1.5f;
        damageAmount = 1;
        aggroRange = 4;
        attackRange = agent.stoppingDistance;
        name = "Quick Draugr";
        state = "idle";
        dead = false;
        Random.InitState((int)(Time.time * 10)); //set random seed

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(10f);


    }

    // Update is called once per frame
    void Update()
    {
        //Check for death
        if (Input.GetKeyUp(KeyCode.Alpha0) || health.GetHealth() <= 0)
        {
            agent.SetDestination(transform.position);
            if (!dead)
            {
                dead = true;
                anim.SetTrigger("die");
                health.TakeDamage(10);
            }
        }
        else if (dead)
        {
            dead = false;
            state = "idle";
            anim.SetTrigger("idle");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            agent.SetDestination(transform.position);
            anim.SetTrigger("hit");

        }
        //if enemy is in attack animation is won't move
        else if (checkAgro())
        {
            if (!tooClose())
            {
                transform.LookAt(target.position);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("idle"))
            {
                anim.SetTrigger("run");
                state = "run";
                if (getDistToPlayer() <= attackRange)
                {
                    state = "attack";
                    //anim.SetTrigger("attack");

                }

            }
            else if (state.Equals("run"))
            {
                agent.SetDestination(target.position);
                if (getDistToPlayer() <= attackRange)
                {
                    state = "attack";
                    //anim.SetTrigger("attack");

                }
            }
            else if (state.Equals("attack"))
            {
               selectAttack();
                
                state = "idle";
            }

        }
        else
        {
            state = "idle";
            anim.SetTrigger("idle");
        }


    }

    private bool tooClose()
    {
        return ((transform.position.x - target.position.x) < .25) && ((transform.position.z - target.position.z) < .25);
    }

    private void selectAttack()
    {
        float temp = Random.value;
        if (temp < .5)
        {
            anim.SetTrigger("attack");
        }
        else
        {
            anim.SetTrigger("kick");
        }
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
