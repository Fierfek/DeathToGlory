using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterMovement))]
//hello
public class MainCharacter : MonoBehaviour {

	Health health;
	CharacterMovement cm;
	public static bool updateStats;
	public Attack attack;

	// Use this for initialization
	void Start() {
		health = GetComponent<Health>();
		health.setHealth(100);
		cm = GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Basic Attack")) {
			basicAttack();
		}

		if (Input.GetButton("Sprint")) {
			cm.Sprint();
		}

		if (Input.GetButtonDown("Jump")) {
			cm.Jump();
		}

		if (Input.GetButton("Roll")) {
			cm.Roll();
			//roll
		}
	}

	public void basicAttack() {
		attack.DoAttack(.5f);
	}
}