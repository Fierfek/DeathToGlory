using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour {

	
	protected Health health, playerHealth;			//All enemies should have health or sometype of damage taking system
    protected float damageAmount;       //amount of damage done to player on each attack hit
    protected NavMeshAgent agent;		//nav mesh agent on object 
    protected Transform target;			//the transform of the target to move towards
    protected bool isAttacking;         //true if this enemy is in their attack animation
    protected string state;
    protected bool dead;
    

	//Enemy attributes
    public float movementSpeed;		//speed of character
	public float aggroRange;			//Distance at which the player is noticed and we exit the idle state.
    public float attackRange;        //distance at which this enemy attacks

	// Use this for initialization
	void Awake() {
		health = GetComponent<Health>();
		agent = GetComponent<NavMeshAgent>();                   //set agent to this objects agent
		target = GameObject.Find("Main Character").transform;    //set target to main character
        playerHealth = target.gameObject.GetComponent<Health>();
		tag = "Enemy";
        isAttacking = false;
    }

    //finds the distance between character and grunt
    protected float getDistToPlayer()
    {
        return (-transform.position + target.position).magnitude;
    }

    protected Vector3 vectorToTarget()
    {
        return (-transform.position + target.position);
    }


	//true is in aggro range
	protected bool checkAgro() {
		return aggroRange > getDistToPlayer();
	}
}
