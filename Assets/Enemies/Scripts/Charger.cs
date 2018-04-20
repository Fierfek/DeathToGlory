using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charger : Enemy {

    Animator anim;
    public int ROTATION_SPEED = 45;
    public int STUN_TIME = 4;
    int rotationMod;
    float timer;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 10f;
        damageAmount = 5;
        aggroRange = 100;
        attackRange = agent.stoppingDistance;
        name = "Charger";
        state = "idle";
        dead = false;
        Random.InitState((int)(Time.time * 10)); //set random seed

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(10f);

        rotationMod = 1;
        timer = 0;

       

    }

    // Update is called once per frame
    void Update()
    {
        //Check for death
        if (Input.GetKeyUp(KeyCode.Alpha0) || health.GetHealth() <= 0)
        {
            
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
            
            anim.SetTrigger("hit");
            state = "idle";

        }
        //if enemy is in attack animation is won't move
        else if (checkAgro())
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("idle"))
            {

                state = "targetPlayer";
                //targetPosition = target;
                selectRotation();
               // rotationMod = 1;



            }
            else if (state.Equals("targetPlayer"))                
            {
                Vector3 targetVector = (-transform.position + target.position).normalized;
                float angle = Vector3.Angle(transform.forward, targetVector);
                if (angle > 5)
                {
                    transform.Rotate(transform.up * Time.deltaTime * ROTATION_SPEED * rotationMod);
                }
                else
                {
                    anim.SetTrigger("run");
                    state = "run";
                    //agent.SetDestination(transform.forward * 300);

                }
            }
            else if (state.Equals("run"))
            {
                transform.Translate(0, 0, .1f);

                if (Physics.Raycast(transform.position, transform.forward, 2))
                {
                    state = "stunned";
                    anim.SetTrigger("stunned");
                    
                }
            }
            else if (state.Equals("stunned"))
            {
                timer += Time.fixedDeltaTime;
                if(timer >= STUN_TIME)
                {
                    anim.SetTrigger("idle");
                    state = "idle";
                }
            }


        }
        else
        {
            state = "idle";
            anim.SetTrigger("idle");
        }


    }

    private void selectRotation()
    {
        if(vectorToTarget().z < 0)
        {
            anim.SetTrigger("leftTurn");
            rotationMod = -1;

        }else
        {
            anim.SetTrigger("rightTurn");
            rotationMod = 1;
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
