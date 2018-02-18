using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination2 : Enemy {

    public float timer = 10f; //Starting 
    float cooldown; //Attack cooldown timer.
    public float fireRate;
    public float gunRange = 5f;
    
    public int period = 4;
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
        attackRange = 4f;
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
            anim.SetTrigger("abomGunDeath");

        }
        //if enemy is in attack animation is won't move
        else if (CheckAgro())
        {
            //transform.LookAt(new Vector3(target.position.x, 1, target.position.z));
            if(cooldown <= 0)//Controls attack cooldown
            {
                hasAttacked = false;
            }
            cooldown -= Time.deltaTime;
            if (getDistToPlayer() <= attackRange && !hasAttacked)
            {
                //While in melee, abomination should be standing still.
                anim.SetTrigger("melee");
                Melee();

                isAttacking = true;
                //Attacks GattlingGun/Melee
                //Animation with attack
                if (direction)
                {
                    Fire();
                    direction = false;
                    anim.SetTrigger("fireGun");
                }
                else
                {
                    Fire2();
                    direction = true;
                    anim.SetTrigger("fireGun");
                }
                hasAttacked = true;
                cooldown = timer;
            }
           else if(getDistToPlayer() <= gunRange && !hasAttacked)
            {
 
                //This part switches the shooting direction.
                //While shooting, abomination should not move.
                if (direction)
                {
                    anim.SetTrigger("fireGun");
                    Fire();
                    direction = false;
                }
                else
                {
                    anim.SetTrigger("fireGun");
                    Fire2();
                    direction = true;
                }
                cooldown = timer;
                hasAttacked = true;
            }

            if(getDistToPlayer() > gunRange)
            {

            
                isAttacking = false;
                anim.SetTrigger("abomGunWalk");
             
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            {
                   agent.SetDestination(target.position);  //update agent destination to target location
            }

 

        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    

    void Melee()
    {
        Vector3 lookPosition = transform.localPosition;
        GameObject gunSwing = Instantiate(melee, lookPosition + (new Vector3(0, 1, 0)), transform.rotation);
        
        Destroy(gunSwing,1f);
        //Creates meleebox infront of Abomination
    }

    //Fires bullet in one spin direction.
    void Fire()
    {
     

       // Create the Bullet from the Bullet Prefab
       GameObject bullet = (GameObject) Instantiate(bulletPrefab, transform.position + transform.forward  *4.25f + new Vector3(0,1,0), bulletSpawn.rotation);
        bullet.transform.Rotate(90,0,0);

        // Destroy the bullet after frequency number of seconds
        //Adjust the period, adjust the number of revolutions.
        Destroy(bullet, period);

    }

    //Fires bullet in the other spin direction
    void Fire2()
    {
        

        // Create the Bullet from the Bullet Prefab
        GameObject bullet = (GameObject) Instantiate(bulletPrefab2, bulletSpawn.position + transform.forward *4.25f + (new Vector3 (0, 1, 0)), bulletSpawn.rotation);
        bullet.transform.Rotate(90, 0, 0);

        // Destroy the bullet after frequency number of seconds
        //Adjust the period, adjust the number of revolutions.
        Destroy(bullet, period);

    }
}
