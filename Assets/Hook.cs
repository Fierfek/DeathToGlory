using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Hook : MonoBehaviour {

	private float speed = .6f, distance;
	private bool throwing, done;
	private Vector3 start, end;
	public GameObject player;
	CharacterController cc;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(throwing) {
			if (Vector3.Distance(end, transform.position) <= 1f || transform.position == end) {
				throwing = false;
			} else {
				transform.position = Vector3.MoveTowards(transform.position, end, speed);
			}
		} else {
			if(done) {
				gameObject.SetActive(false);
			} else {
				cc.Move(Vector3.MoveTowards(player.transform.position, end, speed) - player.transform.position);
				
				//For the specific case that the main character class doesn't catch the collision. .3f is so it detects it after main character would.
				if(Vector3.Distance(cc.ClosestPointOnBounds(end), end) <= .3f) {
					done = true;
				}
			}
		}
	}

	public void throwHook(Vector3 start, Vector3 end, float distance) {
		this.distance = distance;
		this.start = start;
		this.end = end;

		transform.position = start;

		if(cc == null) {
			cc = player.GetComponent<CharacterController>();
			gameObject.GetComponent<SphereCollider>().isTrigger = true;
		}

		gameObject.SetActive(true);
		done = false;
		throwing = true;
	}

	public bool Done() {
		return done;
	}

	public bool Throwing() {
		return throwing;
	}

	public void Stop() {
		done = true;
	}

	private void OnCollisionEnter(Collision collision) {
		if (!collision.gameObject.tag.Equals("Player")) {
			throwing = false;
			if(collision.gameObject.tag.Equals("Enemy")) {
				//Check weight module
			}
		}
	}
}
