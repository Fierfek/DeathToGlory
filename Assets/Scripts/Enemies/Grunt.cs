
using UnityEngine;
using UnityEngine.AI;

public class Grunt : Enemy {

    Rigidbody rb;
	// Use this for initialization
	void Start () {
		movementSpeed = 3.5f;
		name = "Grunt";
        aggroRange = 10;
        rb = GetComponent<Rigidbody>();

		agent.speed = movementSpeed;
		health.SetHealth(10f);
	}
	
	// Update is called once per frame
	void Update () {

        //if enemy is in attack animation is won't move
        if (isAttacking)
        {
            attack(); //temp "anim" for testing
        }
        else if (checkAgro())
            agent.SetDestination(target.position);  //update agent destination to target location
 
		//Check for death
		if(health.GetHealth() <= 0) {
			//Die
		}
	}

    //true is in aggro range
    private bool checkAgro()
    {
        return aggroRange > getDistToPlayer(); 
    }

    //finds the distance between character and grunt
    private float getDistToPlayer()
    {
        return (-transform.position + target.position).magnitude;
    }

    //temporary attack "animation" for testing
    private void attack()
    {

    }

    


}
