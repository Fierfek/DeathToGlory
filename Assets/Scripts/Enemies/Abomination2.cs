using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination2 : Enemy {

    public float timer;
    public float fireRate;

    Animator anim;
    public GameObject bulletPrefab;
    public GameObject shockwave;
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
            if (getDistToPlayer() <= attackRange)
            {

                anim.SetTrigger("FireGun");
                isAttacking = true;
                //Attacks GattlingGun/Melee
                //Animation with attack
            }
            else
            {
                isAttacking = false;
                anim.SetTrigger("AbomRun");

            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                agent.SetDestination(target.position);  //update agent destination to target location

        }
    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    
    void GattlingGun()
    {
        //Rotation and firing. Should have a rate of fire somewhere.
    }
    void Melee()
    {
        //Creates meleebox infront of Abomination
    }

    //Using this to work out how to do bullets.
    //https://unity3d.com/learn/tutorials/temas/multiplayer-networking/shooting-single-player
    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
