using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Hook : MonoBehaviour {

	private float speed = 10f, distance;
	private bool throwing, done;
	private Vector3 start, end;
	public GameObject player;

	CharacterController cc;
	public GameObject chainSection;
	GameObject[] chains;
	int i = 0, numChains = 50;
	Vector3 offset;

	private Vector3 storagePoint;
	

	// Use this for initialization
	void Start () {
		chains = new GameObject[numChains];

		cc = player.GetComponent<CharacterController>();
		gameObject.GetComponent<SphereCollider>().isTrigger = true;

		storagePoint = new Vector3(100, 100, 100);
		transform.position = storagePoint;

		offset = new Vector3(0,  -0.05f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (throwing) {
			if (Vector3.Distance(end, transform.position) <= 1f || transform.position == end) {
				throwing = false;
				i = 0;
			} else {
				transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
			}
		} else {
			if (!done) {
				cc.Move(Vector3.MoveTowards(player.transform.position, end, speed * Time.deltaTime) - player.transform.position);

				//For the specific case that the main character class doesn't catch the collision. .3f is so it detects it after main character would.
				if (Vector3.Distance(cc.ClosestPointOnBounds(end), end) <= .3f) {
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

		gameObject.SetActive(true);
		throwing = true;
		done = false;
		
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
