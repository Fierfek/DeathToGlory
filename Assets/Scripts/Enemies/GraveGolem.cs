using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveGolem : Enemy {

    public GameObject gruntPrefab;
    public GameObject shockwave;
    public GameObject head;
    public float cooldown = 15f;
    float timer;
    public float spawnRange = 8f;


    Animator anim;
	// Use this for initialization
	void Start () {
        movementSpeed = 2.0f;
        damageAmount = 3;
        aggroRange = 8f;
        attackRange = 4f;
        name = "Grave Golem";

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(20f);
        timer = cooldown;
    }
	
	// Update is called once per frame
	void Update () {
		if(health.GetHealth() <= 0)
        {
            isAttacking = false;
            anim.SetTrigger("Death");
        }

        else if (CheckAgro())
        {
            if(cooldown> 0)
            {
                cooldown -= Time.deltaTime;
            }
            if(getDistToPlayer() <= 5 && cooldown<=0)
            {

            }
            else if(getDistToPlayer() <= attackRange)
            {
                Shockwave();
            }
            else if (getDistToPlayer() <= spawnRange)
            {
                SpawnEnemy();
            }
            else
            {
                isAttacking = false;
                //animation
            }
            /*
             * if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")){
             
            }

             */
        }
	}

    void Shockwave()
    {
        GameObject poundAttack = Instantiate(shockwave);

        
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(gruntPrefab);

    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    //This script will throw player at a pillar.
    void ThrowPlayer()
    {

    }

}
