using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveGolem : Enemy {

    public GameObject gruntPrefab;
    public GameObject shockwave;
    public GameObject head;
    public float cooldown = 20f;
    public float hurlRange = 10f;
    public float hurlForce = 20f;
    public float pillarDetectRnge = 200f; //This is the max pillar detect range for golem;
    public float spawnRange = 8f;
    float timer;
    int spwnedMobs = 0;
    bool hasAttacked = false;

    Animator anim;
    // Use this for initialization
    void Start() {
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
    void Update() {
        if (health.GetHealth() <= 0)
        {
            isAttacking = false;
            anim.SetTrigger("Death");
        }

        else if (CheckAgro())
        {
            if (cooldown > 0 && hasAttacked)
            {
                cooldown -= Time.deltaTime;
                if(cooldown<= 0)
                {
                    hasAttacked = false;
                }
            }
            if (getDistToPlayer() <= 2 && cooldown <= 0 && !hasAttacked )
            {
                ThrowPlayer();
                hasAttacked = true;
            }
            else if (getDistToPlayer() <= attackRange && !hasAttacked)
            {
                Shockwave();
                hasAttacked = true;
            }

        
            else if (getDistToPlayer() <= spawnRange && spwnedMobs <5)
            {
                SpawnEnemy();
                spwnedMobs++;
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
        Vector3 swPos = new Vector3(1, 0, 1);
        GameObject poundAttack = Instantiate(shockwave, transform.position + swPos , transform.rotation);
        poundAttack.transform.eulerAngles = new Vector3(90, 0, 0);

    }

    void SpawnEnemy()
    {

        GameObject newEnemy = Instantiate(gruntPrefab, transform.position + (transform.right * 2), transform.rotation);

    }

    bool CheckAgro()
    {
        return aggroRange > getDistToPlayer();
    }

    //This script will throw player at a pillar.
    void ThrowPlayer()
    {
        //Need to figure out how to hold the player for a time.

        //Check if there is a pillar in range, then throw player to the nearest pillar.
        Collider[] colliders = Physics.OverlapSphere(transform.position, hurlRange);
        Collider closestPillar = null;

        float closeDist = pillarDetectRnge; //This is the max pillar detect range for golem;
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Pillar")
            {
                float pillarDist = Vector3.Distance(nearbyObject.transform.position,transform.position);
                if (closeDist > pillarDist)
                {
                    closeDist = pillarDist;
                    closestPillar = nearbyObject;
                }
            }

        }

        //Golem turns towards closest pillar

        //Then Throws player at pillar with some force. 
        Vector3 pillarDirection = closestPillar.transform.position - transform.position;

        GameObject.Find("Main Character").GetComponent<Rigidbody>().AddForce(pillarDirection * hurlForce, ForceMode.VelocityChange);
        //Damage player due to impact or grab.
    }



}
