
using UnityEngine;
using UnityEngine.AI;

public class Grunt : Enemy {

	// Use this for initialization
	void Start () {
		movementSpeed = 3.5f;
		name = "Grunt";

		agent.speed = movementSpeed;
		health.setHealth(10f);
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(target.position);  //update agent destination to target location

		//Check for death
		if(health.getHealth() <= 0) {
			//Die
		}
	}
}
