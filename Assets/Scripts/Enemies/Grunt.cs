
using UnityEngine;
using UnityEngine.AI;

public class Grunt : Enemy {

	// Use this for initialization
	void Start () {
		movementSpeed = 3.5f;
		name = "Grunt";

		agent.speed = movementSpeed;
		health.SetHealth(10f);
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(target.position);  //update agent destination to target location

		//Check for death
		if(health.GetHealth() <= 0) {
			//Die
		}
	}

	//Derivitives of enemy need this and a collider with trigger checked to work properly.
	private void OnTriggerEnter(Collider other) {
		print(name + " has been hit");
		//take damage
	}
}
