using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Hook : MonoBehaviour {

	public float speed = 10f, distance = 15f;
	private bool throwing, retract, move, done;
	private Vector3 start, direction, end;

	public GameObject player;
	public LineRenderer line;

	CharacterController cc;
	Vector3 offset;

	private Vector3 storagePoint;
	private Vector3[] positions;
	private int i;

	// Use this for initialization
	void Start () {

		cc = player.GetComponent<CharacterController>();
		gameObject.GetComponent<SphereCollider>().isTrigger = true;

		storagePoint = new Vector3(100, 100, 100);
		transform.position = storagePoint;
		positions = new Vector3[100];
		i = 0;

		offset = new Vector3(0,  -0.05f, 0);
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (throwing) {
			if (Vector3.Distance(player.transform.position, transform.position) >= distance) {
				throwing = false;
				retract = true;
			} else {
				transform.position += direction * speed * Time.deltaTime;
				if (i < positions.Length) {
					positions[i] = transform.position;
				}
			}
		}

		if(move) {
			if(Vector3.Distance(cc.ClosestPointOnBounds(end), end) <= .3f) {
				move = false;
				done = true;
			} else {
				direction = transform.position - player.transform.position;
				direction = direction.normalized;
				cc.Move(direction * speed * Time.deltaTime);
			}
		}

		if(retract) {
			if (Vector3.Distance(transform.position, player.transform.position) < .3) {
				retract = false;
				done = true;
			} else {
				direction = transform.position - player.transform.position;
				direction = direction.normalized;
				transform.position -= direction * speed * 2 * Time.deltaTime;
			}
		}

		if(done) {
			reset();
			gameObject.SetActive(false);
		}

		line.SetPosition(0, player.transform.position);
		line.SetPosition(1, transform.position);
	}

	public void throwHook(Vector3 start, Vector3 direction) {
		this.start = start;
		this.direction = direction;

		transform.position = start;	

		gameObject.SetActive(true);
		throwing = true;
		done = false;
		end = storagePoint;
		line.gameObject.SetActive(true);
		i = 0;
	}

	private void reset() {
		transform.position = storagePoint;
		line.gameObject.SetActive(false);
	}

	public bool Done() {
		return done;
	}

	public bool Moving() {
		return move;
	}

	public bool Throwing() {
		return throwing;
	}

	public void Stop() {
		done = true;
	}

	private void OnTriggerEnter(Collider collider) {
		if (!collider.gameObject.tag.Equals("Player")) {

			if(collider.gameObject.tag.Equals("Hookable") || collider.gameObject.tag.Equals("Enemy")) {
				end = transform.position;
				throwing = false;
				move = true;
			} else {
				throwing = false;
				retract = true;
			}
		}
	}
}
