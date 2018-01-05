using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Shotgun : MonoBehaviour {

	public GameObject player;

	[Header("Stats")]
	public float range;
	public float spread;
	public float damage; //Range in meters

	SphereCollider collider;
	Rigidbody rigidbody;

	private Vector3 direction;
	private float angle;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
		collider = GetComponent<SphereCollider>();
		rigidbody = GetComponent<Rigidbody>();

		collider.isTrigger = true;
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;

		collider.radius = range;
	}

	public void Fire() {
		gameObject.SetActive(true);
	}

	private void Damage(GameObject enemy) {

		//Find angle between the two objects
		direction = enemy.transform.position - player.transform.position;
		angle = Vector3.Angle(direction, player.transform.forward);

		if(angle <= spread) {
			enemy.gameObject.GetComponent<Health>().TakeDamage(damage);
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.tag.Equals("Enemy") && gameObject.activeSelf) {
			Damage(other.gameObject);
		}
	}
}
