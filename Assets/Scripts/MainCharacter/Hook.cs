using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class Hook : MonoBehaviour {

	private float speed = 10f, distance;
	private bool throwing, done;
	private Vector3 start, end;
	public GameObject player;

	CharacterController cc;
	Rigidbody rigidbody;
	public GameObject chainSection;
	GameObject[] chains;
	int i = 0, numChains = 50;
	Vector3 offset;

	private Vector3 storagePoint;
	

	// Use this for initialization
	void Start () {
		chains = new GameObject[numChains];

		cc = player.GetComponent<CharacterController>();
		rigidbody = GetComponent<Rigidbody>();
		gameObject.GetComponent<SphereCollider>().isTrigger = true;

		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;

		storagePoint = new Vector3(100, 100, 100);
		transform.position = storagePoint;

		offset = new Vector3(0,  -0.05f, 0);

		GenerateChain();
	}

	private void GenerateChain() {
		for (i = 0; i < chains.Length; i++) {
			chains[i] = Instantiate(chainSection);
			if (i == 0) {
				chains[i].GetComponent<CharacterJoint>().connectedBody = rigidbody;
				chains[i].GetComponent<CharacterJoint>().connectedAnchor = new Vector3(0, 0, 0);

			} else {
				chains[i].transform.SetParent(chains[i - 1].transform);
				chains[i].GetComponent<CharacterJoint>().connectedBody = chains[i - 1].GetComponent<Rigidbody>();
				chains[i].GetComponent<CharacterJoint>().connectedAnchor = new Vector3(0, 0, 0);
			}
		}

		chains[0].transform.position = storagePoint;

		chains[chains.Length - 1].AddComponent<HingeJoint>();
		HingeJoint joint = chains[chains.Length - 1].GetComponent<HingeJoint>();
		joint.autoConfigureConnectedAnchor = false;
		joint.anchor = new Vector3(0, -0.023f, 0);
		joint.axis = new Vector3(0, 1, 0);
		joint.connectedAnchor = new Vector3(0, 0, 0);
		joint.connectedBody = player.GetComponent<Rigidbody>();

		ResetChain();
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
			chains[i].GetComponent<Rigidbody>().useGravity = true;
			chains[i].GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	private void ResetChain() {

		

		for (int i = 0; i < chains.Length; i++) {
			//chains[i].SetActive(false);

			chains[i].GetComponent<Rigidbody>().useGravity = false;
			chains[i].GetComponent<Rigidbody>().isKinematic = true;

			chains[i].transform.localPosition = offset;
			chains[i].transform.eulerAngles = new Vector3(90, 0, 0);

			
		}

		transform.position = storagePoint;
		chains[0].transform.position = storagePoint;

		for (int i = 0; i < chains.Length; i++) {
			//chains[i].SetActive(true);

			if (i > 0)
				chains[i].GetComponent<CharacterJoint>().connectedBody = chains[i - 1].GetComponent<Rigidbody>();
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
