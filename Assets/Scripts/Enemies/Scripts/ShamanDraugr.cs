using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanDraugr : Enemy {

    float timer;
    Animator anim;
    Transform healTarget;
    float healRange;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 1.5f;
        damageAmount = 5;
        aggroRange = 100;
        attackRange = agent.stoppingDistance;
        healRange = attackRange;
        name = "ShamanDraugr";

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(20f);
        healTarget = findNearbyEnemy();


    }

    // Update is called once per frame
    void Update()
    {
        //Check for death
        if (health.GetHealth() <= 0)
        {
            anim.SetTrigger("death");

        }
        //if enemy is in attack animation is won't move
        else if (checkAgro())
        {
            
                if(distToTarget() < healRange)
                {
                    agent.SetDestination(transform.position);
                if (healTarget.GetComponent<Enemy>().getStatus())
                    anim.SetTrigger("heal");
                else
                    anim.SetTrigger("raiseDead");
                }
            
        }
        else
            anim.SetTrigger("idle");

    }

    private Transform findNearbyEnemy()
    {
        
        Collider[] nearby = Physics.OverlapSphere(transform.position, 20);
        Transform newTarget = nearby[0].gameObject.transform;
        for (int i = 0; i< nearby.Length; i++)
        {
            if (nearby[i].gameObject.GetComponent<Health>().currentHealth < newTarget.GetComponent<Health>().currentHealth)
                newTarget = nearby[i].gameObject.transform;
        }

        return newTarget;
    }

    private float distToTarget()
    {
        return (-transform.position + healTarget.position).magnitude;
    }


}
