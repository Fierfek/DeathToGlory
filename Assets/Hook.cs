using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class Hook : MonoBehaviour {

	private float speed = 20f, distance;
	private bool throwing, done;
	private Vector3 start, end;
	public GameObject player;

	CharacterController cc;
	Rigidbody rigidbody;
	public GameObject chainSection;
	GameObject[] chains;
	int i = 0;
	Vector3 offset;
	

	// Use this for initialization
	void Start () {
		chains = new GameObject[50];

		cc = player.GetComponent<CharacterController>();
		rigidbody = GetComponent<Rigidbody>();
		gameObject.GetComponent<SphereCollider>().isTrigger = true;

		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;

		offset = new Vector3(0,  -0.05f, 0);

		for (i = 0; i < chains.Length; i++) {
			chains[i] = Instantiate(chainSection);
			if (i == 0) {
				chains[i].GetComponent<CharacterJoint>().connectedBody = rigidbody;
				chains[i].transform.position = transform.position;
				chains[i].GetComponent<CharacterJoint>().autoConfigureConnectedAnchor = false;
				chains[0].GetComponent<CharacterJoint>().connectedAnchor = new Vector3(0, 0, 0);
			} else {
				chains[i].transform.SetParent(chains[i - 1].transform);
				chains[i].transform.localPosition = offset;
				chains[i].GetComponent<CharacterJoint>().connectedBody = chains[i - 1].GetComponent<Rigidbody>();
			}

			chains[i].SetActive(false);
		}

		chains[chains.Length - 1].AddComponent<Follow>();
		chains[chains.Length - 1].GetComponent<Follow>().Setup(player.transform);

		//gameObject.SetActive(false);
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
			if (done) {
				ResetChain();
				//gameObject.SetActive(false);
			} else {
				cc.Move(Vector3.MoveTowards(player.transform.position, end, speed * Time.deltaTime) - player.transform.position);

				//For the specific case that the main character class doesn't catch the collision. .3f is so it detects it after main character would.
				if (Vector3.Distance(cc.ClosestPointOnBounds(end), end) <= .3f) {
					done = true;
				}
			}
		}
	}

	private void AddChain() {
		for (int i = 0; i < chains.Length; i++) {
			chains[i].SetActive(true);
		}

		chains[chains.Length - 1].GetComponent<Follow>().SetFollow(true);
	}

	private void ResetChain() {
		for (int i = 0; i < chains.Length; i++) {
			chains[i].SetActive(false);
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

		AddChain();
		
	}

	public bool Done() {
		return done;
	}

	public bool Throwing() {
		return throwing;
	}

	public void Stop() {
		ResetChain();
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
