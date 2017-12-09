using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ControlScheme))]

public class MainCharacter : MonoBehaviour {

	Health health;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();

		health.setHealth(100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}