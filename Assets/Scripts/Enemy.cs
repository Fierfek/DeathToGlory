using UnityEngine;

[RequireComponent(typeof(Health))]

public class Enemy : MonoBehaviour {

	//All enemies should have health or sometype of damage taking system
	Health health;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
