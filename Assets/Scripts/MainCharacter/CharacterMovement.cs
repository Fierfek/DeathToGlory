using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MouseRotationX))]

public class CharacterMovement : MonoBehaviour {

	[Header("Movement")]
	public float moveSpeed = 5;
	public float sprintSpeed = 10;
	public float jumpSpeed = 1;
	public float gravity = 15;
	public float turnRate = .75f;

	private bool jump, sprint, roll, hook, grav, rolling;

	CharacterController cc;
	MouseRotationX mrx;
	private Vector3 moveDirection;

	[Header("Camera")]
	public GameObject cameraAnchor;
	private float radToDeg = 180 / Mathf.PI;
	private Quaternion cameraRotation;
	private Vector3 forward, right;

	private float currentRotation;
	private Vector3 temp;

	[Header("Roll")]
	public float rollTime = .5f;
	public float rollSpeed = 5;
	private float rollPause, rollStart;
	private Vector3 rollDirection;


	private void Start() {
		cc = GetComponent<CharacterController>();
		mrx = GetComponent<MouseRotationX>();
		jump = sprint = roll = hook = grav = rolling = false;

		temp = new Vector3();
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

			if (jump) {
				moveDirection.y += jumpSpeed;
			}

			if (sprint) {
				temp.Set(moveDirection.x * sprintSpeed, moveDirection.y * moveSpeed, moveDirection.z * sprintSpeed);
			} else {
				temp.Set(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed, moveDirection.z * moveSpeed);
			}

			moveDirection = temp;

			if (rolling) {
				if(Time.time <= rollStart + rollTime) {
					moveDirection = Vector3.zero;
					moveDirection = rollDirection * rollSpeed;
				} else {
					rolling = false;
				}
			}
		}

		if (Input.GetAxisRaw("Move Horizontal") != 0 || Input.GetAxisRaw("Move Vertical") != 0) {
			if (!hook) {
				cameraRotation = cameraAnchor.transform.rotation;
				RotateTo(Mathf.Atan2(moveDirection.x, moveDirection.z) * radToDeg);
				cameraAnchor.transform.rotation = cameraRotation;
			}
		}


		if (!grav) {
			moveDirection.y -= gravity * Time.deltaTime;
		}

		resetFlags();

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
