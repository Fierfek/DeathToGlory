using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : Enemy {

    Animator anim;
    Health healTargetHealth;
    public Transform healTarget;
    private float timer;
    private const float HEAL_COOLDOWN = 10;

    // Use this for initialization
    void Start()
    {
        healTargetHealth = healTarget.gameObject.GetComponent<Health>();
        movementSpeed = 1.5f;
        damageAmount = 1;
        aggroRange = 20;
        attackRange = agent.stoppingDistance;
        name = "Shaman";
        state = "idle";
        dead = false;
        Random.InitState((int)(Time.time * 10)); //set random seed
        target = healTarget;

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(10f);


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.fixedDeltaTime;
        //Check for death
        if (state.Equals("die") || Input.GetKeyUp(KeyCode.Alpha8))//health.GetHealth() <= 0)
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
            if (!tooClose())
            {
                transform.LookAt(target.position);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("idle"))
            {
                agent.SetDestination(transform.position);
                if (getDistToPlayer() < agent.stoppingDistance)
                {
                    anim.SetTrigger("walkBack");
                    agent.SetDestination(-transform.forward.normalized * 2);
                }
                else
                {
                    anim.SetTrigger("walkForward");
                    state = "walkForward";
                    if (getDistToPlayer() <= attackRange)
                    {
                        state = "heal";
                        anim.SetTrigger("heal");

                    }
                }

            }
            else if (state.Equals("walkForward"))
            {
                agent.SetDestination(target.position);
                if (getDistToPlayer() <= attackRange)
                {
                    state = "healChoice";

                }
            }
            else if (state.Equals("healChoice"))
            {
                selectHeal();
                agent.SetDestination(transform.position);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("pause")) 
            {
                healTargetHealth.SetHealth(healTargetHealth.GetMaxHealth());
                //anim.SetTrigger("idle");
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

    private void selectHeal()
    {
        
        if(healTargetHealth.GetHealth() <= 0)
        {
            anim.SetTrigger("revive");
            state = "revive";
        }
        else
        {
            anim.SetTrigger("heal");
            state = "heal";
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
