using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination2 : Enemy {

    public float timer = 5f; //Starting 
    float cooldown; //Attack cooldown timer.
    public float fireRate;
    public float gunRange = 15f;
    
    public int frequency = 4;
    //With frequency = 4, then it creates one revolution.
    //90 degrees per second.
    bool direction = false;
    bool hasAttacked = false;
    
    Animator anim;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject melee;
    public Transform bulletSpawn;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 2.0f;
        damageAmount = 3;
        aggroRange = 8;
        attackRange = 3f;
        name = "Abomination 2";

        anim = GetComponent<Animator>();
        agent.speed = movementSpeed;
        health.SetHealth(20f);
        cooldown = timer;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for death
        if (health.GetHealth() <= 0)
        {
            isAttacking = false;
            anim.SetTrigger("gruntDeath");

        }
        //if enemy is in attack animation is won't move
        else if (CheckAgro())
        {
           
            if(timer <= 0)
            {
                hasAttacked = false;
            }
 
           else if (getDistToPlayer() <= attackRange && !hasAttacked)
            {
                //While in melee, abomination should be standing still.
                Melee();
                hasAttacked = true;
                /*anim.SetTrigger("FireGun");
                isAttacking = true;
                //Attacks GattlingGun/Melee
                //Animation with attack
                */
                timer = cooldown;
            }
           else if(getDistToPlayer() <= gunRange && !hasAttacked)
            {

                //This part switches the shooting direction.
                //While shooting, abomination should not move.
                if (direction)
                {
                    Fire();
                    direction = false;
                    //animation
                }
                else
                {
                    Fire2();
                    direction = true;
                    //animation
                }
                timer = cooldown;
                hasAttacked = true;
            }

            else
            {

                /*
                isAttacking = false;
                anim.SetTrigger("AbomRun");
                */
            }
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            //Movement needs work
            if(getDistToPlayer() > 4f)
            {
                //agent.SetDestination(target.position);  //update agent destination to target location
            }
 

        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    

    void Melee()
    {
        GameObject gunSwing = Instantiate(melee, transform.position + (new Vector3(0, 0, 2f)), transform.rotation);
        Destroy(gunSwing,1f);
        //Creates meleebox infront of Abomination
    }

    //Fires bullet in one spin direction.
    void Fire()
    {
        float spinAngle = 360 / frequency;

       // Create the Bullet from the Bullet Prefab
       GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position + (new Vector3 (0, 0, 4.25f)), bulletSpawn.rotation);


        // Destroy the bullet after frequency number of seconds
        //Adjust the frequency, adjust the number of revolutions.
        Destroy(bullet, frequency);

    }

    //Fires bullet in the other spin direction
    void Fire2()
    {
           
        float spinAngle = 360 / frequency;

       // Create the Bullet from the Bullet Prefab
       GameObject bullet = (GameObject) Instantiate(bulletPrefab2, bulletSpawn.position + (new Vector3 (0, 0, 4.25f)), bulletSpawn.rotation);


        // Destroy the bullet after frequency number of seconds
        //Adjust the frequency, adjust the number of revolutions.
        Destroy(bullet, frequency);

    }
}
