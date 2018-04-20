using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour {

	[Header("Movement")]
	public float moveSpeed = 5;
	public float sprintSpeed = 10;
	public Vector3 moveDirection;
	public float jumpSpeed = 5;
	private float velocity, acceleration;
	public float gravity = 10;
	private float x, y;

	private bool jump, sprint, roll, grav, rolling, hook;

	CharacterController cc;
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
	bool jumping = false, canJump = true;
	bool slidding;
	Vector3 normal;


	void Awake() {

	}

	private void Start() {
		cc = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();

		jump = sprint = roll = grav = rolling = hook = false;
		temp = new Vector3();
	}

	// Update is called once per frame
	void Update() {
		if (!slidding) {
			if (isGrounded()) {
				//Find the foreward relative to the camera
				forward = cameraAnchor.transform.forward;
				forward.y = 0;
				forward = forward.normalized;
				right = new Vector3(forward.z, 0, -forward.x);
				right = right.normalized;

				x = Input.GetAxis("Move Horizontal");
				y = Input.GetAxis("Move Vertical");
				moveDirection = x * right + y * forward;

				velocity = moveDirection.magnitude;

				moveDirection = moveDirection.normalized;

				animator.SetFloat("speed", velocity);

				if (roll) {
					rolling = true;
					rollDirection = moveDirection;
				}

				if (jump) {
					moveDirection.y += jumpSpeed;
				}

				if (sprint) {
					moveDirection.Set(moveDirection.x * sprintSpeed, moveDirection.y, moveDirection.z * sprintSpeed);
				} else {
					moveDirection.Set(moveDirection.x * moveSpeed, moveDirection.y, moveDirection.z * moveSpeed);
				}

				if (rolling) {
					if (Time.time <= rollStart + rollTime) {
						moveDirection = Vector3.zero;
						moveDirection = rollDirection * rollSpeed;
					} else {
						rolling = false;
					}
				}
			}

			if(!hook) {
				if (Input.GetAxisRaw("Move Horizontal") != 0 || Input.GetAxisRaw("Move Vertical") != 0) {
					cameraRotation = cameraAnchor.transform.rotation;
					RotateTo(Mathf.Atan2(moveDirection.x, moveDirection.z) * radToDeg);
					cameraAnchor.transform.rotation = cameraRotation;
				}
			} else {
				cameraRotation = cameraAnchor.transform.rotation;
				RotateTo(Mathf.Atan2(forward.x, forward.z) * radToDeg);
				cameraAnchor.transform.rotation = cameraRotation;
			}

			if (!grav) {
				moveDirection.y -= gravity * Time.deltaTime;
			}

		} else {
			moveDirection = new Vector3(normal.x, -normal.y, normal.z);
			velocity = moveDirection.magnitude;
			Vector3.OrthoNormalize(ref normal, ref moveDirection);
			moveDirection *= moveSpeed;

			cameraRotation = cameraAnchor.transform.rotation;
			RotateTo(Mathf.Atan2(moveDirection.x, moveDirection.z) * radToDeg);
			cameraAnchor.transform.rotation = cameraRotation;

			if (jump) {
				moveDirection = normal * (jumpSpeed/2);
			}
		}

		animator.SetBool("jump", jump);
		animator.SetBool("grounded", isGrounded());

		resetFlags();

		cc.Move(moveDirection * Time.deltaTime);
	}

	public void gravityOff() {
		grav = true;
	}

	public void slide(Vector3 normal) {
		slidding = true;
		this.normal = normal;
	}

	public void Jump() {
		jump = true;
	}

	public void Hook() {
		hook = true;
	}

	public void Sprint() {
		sprint = true;
	}

	public void stopHanging(Vector3 movement) {
		moveDirection = movement;
	}

	public void Roll() {
		if(rollStart + rollTime + rollPause <= Time.time) {
			rollStart = Time.time;
			roll = true;
			rollPause = .5f;
		}
	}

	private void resetFlags() {
		jump = sprint = roll = grav = hook = false;
		slidding = false;
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

		if(!jumping && !grav) {
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
