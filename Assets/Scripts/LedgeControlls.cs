using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeControlls : MonoBehaviour {

	private CharacterController cc;

	public float moveSpeed, jumpSpeed;

	private Ledge currentLedge;
	private Transform[] nodes;

	public Transform hangPoint;
	private bool hanging;
	private Vector3 hangOffset;
	private float length;

	private float x;
	private Vector3 direction;

	private RaycastHit hit;

	LayerMask mask = 1 << 2;

	// Use this for initialization
	void Awake () {

	}

	void Start() {
		cc = GetComponent<CharacterController>();

		hangOffset = transform.position - hangPoint.position;
		mask = ~mask;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentLedge != null) {

			if (Input.GetButton("Jump")) {

				//Raycast to determine if climb over edge or go up.
				if(Physics.Raycast(hangPoint.position + Vector3.up, transform.forward, out hit, 2f, mask)) {
					//Climb up
					//anim.Trigger("ClimbUp");
				} else {
					//Jump up
					direction = Vector3.up * jumpSpeed;
				}

				cc.Move(direction * Time.deltaTime);

			} else {
				x = Input.GetAxis("Move Horizontal");

				if (x != 0) {
					direction = hangPoint.position - nodes[0].position;

					direction = direction.normalized;
					direction *= moveSpeed * x;

				} else {
					direction.Set(0, 0, 0);
				}

				cc.Move(direction * Time.deltaTime);
			}
		}
	}

	public void setLedge(Ledge l, Collider c) {
		currentLedge = l;

		transform.position = c.ClosestPoint(hangPoint.position) + hangOffset;
		nodes = l.getNodes();

		length = Vector3.Distance(nodes[0].position, nodes[1].position);
	}
}
