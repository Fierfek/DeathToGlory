using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ControlScheme))]

public class MainCharacter : MonoBehaviour {

	Health health;
	public GameObject axeCollider;
	public Attack attack;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();

		health.setHealth(100);
	}
	
	// Update is called once per frame
	void Update () {
		//Check for death
		if(health.getHealth() <= 0) {
			//Die
			//ResetGame
		}

		if (Input.GetButton("Fire1") ) {
			basicAttack();
		}
	}

	public void basicAttack() {
		attack.DoAttack(.5f);
	}
}