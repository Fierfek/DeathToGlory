using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination2 : Enemy {

    public float timer;
    public float fireRate;
    public float gunRange = 15f;
    
    public int frequency = 6;

    
    Animator anim;
    public GameObject bulletPrefab;
    public GameObject melee;
    public Transform bulletSpawn;
    bool hasFired = false;
    // Use this for initialization
    void Start()
    {
        movementSpeed = 2.0f;
        damageAmount = 3;
        aggroRange = 8;
        attackRange = 2f;
        name = "Abomination 2";

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
            anim.SetTrigger("gruntDeath");

        }
        //if enemy is in attack animation is won't move
        else if (CheckAgro())
        {
            if(getDistToPlayer() <= gunRange && !hasFired)
            {

                //Fire();
                hasFired = true;
            }
            if (getDistToPlayer() <= attackRange)
            {

                /*anim.SetTrigger("FireGun");
                isAttacking = true;
                //Attacks GattlingGun/Melee
                //Animation with attack
                */
            }
            else
            {

                /*
                isAttacking = false;
                anim.SetTrigger("AbomRun");
                */
            }
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                //agent.SetDestination(target.position);  //update agent destination to target location

        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    

    void Melee()
    {
        GameObject gunSwing = Instantiate(melee, transform.position, transform.rotation);
        Destroy(gunSwing,1f);
        //Creates meleebox infront of Abomination
    }

    //Using this to work out how to do bullets.
    //https://unity3d.com/learn/tutorials/temas/multiplayer-networking/shooting-single-player
    void Fire()
    {
        float spinAngle = 360 / frequency;
       GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        for (int i = 0; i< frequency; i++)
        {
             bullet.transform.RotateAround(transform.position, Vector3.down, spinAngle * Time.deltaTime);
            transform.Rotate(new Vector3(0, spinAngle * Time.deltaTime, 0));
        }

        // Create the Bullet from the Bullet Prefab


        // Spin
        //bullet.RotateAround
        //transform.Rotate(transform.eulerAngles + new Vector3(0, .1f, 0));
        

        // Destroy the bullet after 2 seconds
        Destroy(bullet, frequency);
    }
}
