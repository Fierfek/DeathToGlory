using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]

public class Enemy : MonoBehaviour {

	
	Health health;      //All enemies should have health or sometype of damage taking system
    NavMeshAgent agent; //nav mesh agent on object     
    Transform target;   //the transform of the target to move towards
    int speed;          //speed of character

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();                   //set agent to this objects agent
        target = GameObject.Find("MainCharacter").transform;    //set target to main character
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);      //update agent destination to target location
	}
}
