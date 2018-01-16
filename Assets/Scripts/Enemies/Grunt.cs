
using UnityEngine;
using UnityEngine.AI;

public class Grunt : Enemy {


    float timer;
    // Use this for initialization
    void Start () {
		movementSpeed = 3.5f;
        damageAmount = 1;
        aggroRange = 10;
        attackRange = 3;
        name = "Grunt";


		agent.speed = movementSpeed;
		health.SetHealth(10f);

        
	}
	
	// Update is called once per frame
	void Update () {
        //Check for death
        if (health.GetHealth() <= 0)
        {
            //Die
        }
        //if enemy is in attack animation is won't move
        else if (isAttacking)
        {
            attack(); //temp "anim" for testing
        }
        else if (checkAgro())
        {
            if(getDistToPlayer() <= attackRange)
            {
                isAttacking = true;
            }
            agent.SetDestination(target.position);  //update agent destination to target location
        }
		
	}

    //true is in aggro range
    private bool checkAgro()
    {
        return aggroRange > getDistToPlayer(); 
    }

    

    //temporary attack "animation" for testing
    private void attack()
    {
        timer += Time.deltaTime;
        target.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
        spin();
        if(timer >= 2)
        {
            isAttacking = false;
        }
    }

    private void spin()
    {
        transform.Rotate(transform.eulerAngles + new Vector3(0, .1f, 0));
    }



}
