using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MouseRotationX))]

public class CharacterMovement : MonoBehaviour {

	public Vector3 moveDirection;
	public float moveSpeed = 1;
	public float sprintSpeed = 2;
	public float jumpSpeed = 1;
	public float gravity = 1;

	private bool jump, sprint, roll, hook, grav, rolling;

	CharacterController cc;
	MouseRotationX mrx;

	public GameObject cameraAnchor;
	private float radToDeg = 180 / Mathf.PI;
	private Quaternion cameraRotation;
	private Vector3 forward, right;

	private float currentRotation;
	public float turnRate = .75f;

	public float rollTime, rollStart, rollSpeed;
	private float rollPause;
	private Vector3 rollDirection;


	private void Start() {
		cc = GetComponent<CharacterController>();
		mrx = GetComponent<MouseRotationX>();
		jump = sprint = roll = hook = grav = rolling = false;
	}

	// Update is called once per frame
	void Update() {
		if (hook) {
			mrx.enabled = true;
		} else {
			mrx.enabled = false;
		}

		if (cc.isGrounded) {

			//Find the foreward relative to the camera
			forward = cameraAnchor.transform.forward.normalized;
			forward.y = 0;
			right = new Vector3(forward.z, 0, -forward.x);

			//set the movements direction
			moveDirection = Input.GetAxis("Move Horizontal") * right + Input.GetAxis("Move Vertical") * forward;

			if (roll) {
				rolling = true;
				rollDirection = moveDirection;
			}

			if (Input.GetAxisRaw("Move Horizontal") != 0 || Input.GetAxisRaw("Move Vertical") != 0) {
				if(!hook) {
					RotateTo(Mathf.Atan2(moveDirection.x, moveDirection.z) * radToDeg);
				}
			}

			if (jump) {
				moveDirection.y += jumpSpeed;
			}

			if(sprint) {
				moveDirection *= sprintSpeed;
			} else {
				moveDirection *= moveSpeed;
			}

			if(rolling) {
				if(Time.time <= rollStart + rollTime) {
					moveDirection = Vector3.zero;
					moveDirection = rollDirection * rollSpeed;
				} else {
					rolling = false;
				}
			}
		}

		resetFlags();
		if (!grav) {
			moveDirection.y -= gravity * Time.deltaTime;
		}

		cc.Move(moveDirection * Time.deltaTime);
	}

	public void gravityOff() {
		grav = true;
	}

	public void Jump() {
		jump = true;
	}

	public void Sprint() {
		sprint = true;
	}

	public void Roll() {
		if(rollStart + rollTime + rollPause <= Time.time) {
			rollStart = Time.time;
			roll = true;
			rollPause = .5f;
		}
	}

	public void HookShot() {
		hook = true;
	}

	private void resetFlags() {
		jump = sprint = roll = hook = grav = false;
	}

	private void RotateTo(float angle) {
		currentRotation = transform.eulerAngles.y;
		transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(currentRotation, angle, turnRate), 0);
	}
}
