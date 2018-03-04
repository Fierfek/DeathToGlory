using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeControlls : MonoBehaviour {

	private CharacterController cc;
	private MainCharacter mc;

	public float moveSpeed, jumpSpeed, gravity;

	public Transform cameraAnchor;

	private Ledge currentLedge;
	private Transform[] nodes;

	public Transform hangPoint;
	private bool hanging;
	private Vector3 hangOffset;
	private float length;

	private float drop;
	private bool dropping;

	private float x;
	private Vector3 direction, lhs;

	private RaycastHit hit;
	LayerMask mask = 1 << 2;

	private bool jump, jumping, climbUp;

	void Awake() {
		cc = GetComponent<CharacterController>();
		mc = GetComponent<MainCharacter>();		

		mask = ~mask;
		jump = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentLedge != null) {

			float drop = Input.GetAxisRaw("Roll");

			if(drop != 0 || dropping) {
				mc.stopHanging(direction);
			}

			if (jump || jumping) {
				if(jump && !jumping) {

					x = Input.GetAxis("Move Horizontal");

					if (x != 0) {
						direction = nodes[1].position - nodes[0].position;
						direction = direction.normalized;
						direction *= moveSpeed * x;
						direction.Set(direction.x, 0, direction.z);
					}

					//Raycast to determine if climb over edge or go up.
					if (!Physics.Raycast(hangPoint.position + Vector3.up, transform.forward, out hit, 1f, mask)) {
						//Climb up
						//anim.Trigger("ClimbUp");
						climbUp = true;
					} else {
						//Jump up
						direction.y = jumpSpeed;
						jumping = true;
					}
				}
				
				if(jumping) {
					direction.y -= gravity * Time.deltaTime;
					//mc.stopHanging(direction);
				}

				if(climbUp) {

				}

				cc.Move(direction * Time.deltaTime);

			} else {
				x = Input.GetAxis("Move Horizontal");

				if (x != 0) {
					direction = nodes[1].position - nodes[0].position;

					direction = direction.normalized;
					direction *= moveSpeed * x;

					if (Vector3.Distance(hangPoint.position + (direction * Time.deltaTime), nodes[1].position) > length) {
						if(currentLedge.getLeft() != null) {
							setLedge(currentLedge.getLeft(), currentLedge.getLeft().getCollider());
						} else {
							direction.Set(0, 0, 0);
						}
					}

					if (Vector3.Distance(hangPoint.position + (direction * Time.deltaTime), nodes[0].position) > length) {
						if (currentLedge.getRight() != null) {
							setLedge(currentLedge.getRight(), currentLedge.getRight().getCollider());
						} else {
							direction.Set(0, 0, 0);
						}
					}

				} else {
					direction.Set(0, 0, 0);
				}

				cc.Move(direction * Time.deltaTime);
			}
		}

		resetFlags();
	}

	private void resetFlags() {
		jump = false;
	}

	public void Jump() {
		jump = true;
	}

	public bool getJumping() {
		return jumping;
	}

	public void setLedge(Ledge l, Collider c) {

		currentLedge = l;
		jumping = false;

		transform.rotation = Quaternion.Euler(new Vector3(0, l.transform.rotation.eulerAngles.y, 0));
		cameraAnchor.localRotation = Quaternion.Euler(Vector3.zero);

		hangOffset = transform.position - hangPoint.position;

		transform.position = c.ClosestPoint(hangPoint.position) + hangOffset;
		nodes = l.getNodes();

		length = Vector3.Distance(nodes[0].position, nodes[1].position);
	}
}
