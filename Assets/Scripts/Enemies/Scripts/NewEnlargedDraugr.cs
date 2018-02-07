using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnlargedDraugr : Enemy {

    Animator anim;

    // Use this for initialization
    void Start()
    {
        movementSpeed = .75f;
        damageAmount = 5;
        aggroRange = 10;
        attackRange = agent.stoppingDistance;
        name = "Enlarged Draugr";
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
        if (state.Equals("die") || Input.GetKeyUp(KeyCode.Alpha0))//health.GetHealth() <= 0)
        {
            state = "die";
            if (!dead)
            {
                dead = true;
                anim.SetTrigger("die");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            anim.SetTrigger("hit");

        }
        //if enemy is in attack animation is won't move
        else if (checkAgro())
        {
            transform.LookAt(target.position);

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("idle"))
            {
                anim.SetTrigger("walkForward");
                state = "walkForward";
                if (getDistToPlayer() <= attackRange)
                {
                    state = "attack";
                    anim.SetTrigger("attack");

                }

            }
            else if (state.Equals("walkForward"))
            {
                agent.SetDestination(target.position);
                if (getDistToPlayer() <= attackRange)
                {
                    state = "attack";
                    anim.SetTrigger("attack");

                }
            }
            else if (state.Equals("attack"))
            {
                selectAttack();
                state = "attack123";
            }
            else if (state.Equals("attack123"))
            {
                state = "idle";
            }

        }
        else
            anim.SetTrigger("idle");

    }

    private void selectAttack()
    {
        float temp = Random.value;
        if (temp < .3)
        {
            anim.SetTrigger("attack1");
        }
        else if (temp < .6)
        {
            anim.SetTrigger("attack2");
        }
        else
        {
            anim.SetTrigger("attack2");
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
