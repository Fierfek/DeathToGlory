using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MouseRotationX))]
[RequireComponent(typeof(Animator))]

public class CharacterMovement : MonoBehaviour {

	[Header("Movement")]
	public float moveSpeed = 5;
	public float sprintSpeed = 10;
	public Vector3 moveDirection;
	public float jumpSpeed = 5;
	private float velocity, acceleration;
	public float gravity = 10;

	private bool jump, sprint, roll, hook, grav, rolling;

	CharacterController cc;
	MouseRotationX mrx;
	Animator animator;

	[Header("Camera")]
	public GameObject cameraAnchor;
	private float radToDeg = 180 / Mathf.PI;
	private Quaternion cameraRotation;
	private Vector3 forward, right;
	private float currentRotation;

	[Header("Roll")]
	public float rollTime = .5f;
	public float rollSpeed = 5;
	private float rollPause, rollStart;
	private Vector3 rollDirection;
	private Vector3 temp;

	private Vector3 bottom;
	bool jumping = false;


	private void Start() {
		cc = GetComponent<CharacterController>();
		mrx = GetComponent<MouseRotationX>();
		animator = GetComponent<Animator>();

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

		if (isGrounded()) {
			//Find the foreward relative to the camera
			forward = cameraAnchor.transform.forward.normalized;
			forward.y = 0;
			right = new Vector3(forward.z, 0, -forward.x);

			//set the movements direction
			moveDirection = Input.GetAxis("Move Horizontal") * right + Input.GetAxis("Move Vertical") * forward;

			velocity = moveDirection.magnitude;
			animator.SetFloat("speed", velocity);

			if (roll) {
				rolling = true;
				rollDirection = moveDirection;
			}

			if (jump) {
				moveDirection.y += jumpSpeed;
			}

			if (sprint) {
				temp.Set(moveDirection.x * sprintSpeed, moveDirection.y, moveDirection.z * sprintSpeed);
			} else {
				temp.Set(moveDirection.x * moveSpeed, moveDirection.y, moveDirection.z * moveSpeed);
			}

			moveDirection = temp;

			if (rolling) {
				if (Time.time <= rollStart + rollTime) {
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

		animator.SetBool("jump", jump);
		animator.SetBool("grounded", isGrounded());

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
		transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(currentRotation, angle, .75f), 0);
	}
	
	public bool isGrounded() {
		if(cc.isGrounded) {

			if (jumping)
				jumping = false;

			return true;
		}

		if(jump) {
			jumping = true;
		}

		if(!jumping) {
			bottom = cc.transform.position + cc.center + Vector3.down * (cc.height / 2);

			RaycastHit h;
			if (Physics.Raycast(bottom, Vector3.down, out h, .3f)) {

				Debug.DrawRay(bottom, Vector3.down * h.distance, Color.black); //Debug only
				cc.Move(Vector3.down * h.distance);
				return true;
			}
		}

		return false;
	}
}
