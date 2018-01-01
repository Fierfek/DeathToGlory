using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour {

	
	protected Health health;			//All enemies should have health or sometype of damage taking system
    protected NavMeshAgent agent;		//nav mesh agent on object 
    protected Transform target;			//the transform of the target to move towards

	//Enemy attributes
    protected float movementSpeed;		//speed of character
	protected float aggroRange;			//Distance at which the player is noticed and we exit the idle state.

	// Use this for initialization
	void Awake() {
		health = GetComponent<Health>();
		agent = GetComponent<NavMeshAgent>();                   //set agent to this objects agent
		target = GameObject.Find("Main Character").transform;    //set target to main character
		tag = "Enemy";
	}
}
