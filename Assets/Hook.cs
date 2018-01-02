using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Hook : MonoBehaviour {

	private float speed = .6f, distance;
	private bool throwing, done;
	private Vector3 start, end;
	public GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame - Sometimes get's character stuck;
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
				player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, speed);
				if(Vector3.Distance(player.transform.position, end) <= 1.2f) {
					done = true;
				}
			}
		}
	}

	public void throwHook(Vector3 start, Vector3 end, float distance) {
		this.distance = distance;
		throwing = true;
		this.start = start;
		this.end = end;
		transform.position = start;
		gameObject.SetActive(true);
		done = false;
	}

	public bool Done() {
		return done;
	}

	public void Stop() {
		done = true;
	}

	private void OnCollisionEnter(Collision collision) {
		if (!collision.gameObject.tag.Equals("Player")) {
			throwing = false;
			print(collision.gameObject.name);
		}
	}
}
