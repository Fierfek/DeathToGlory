using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MouseRotationX))]
[RequireComponent(typeof(Animator))]

public class CharacterMovement : MonoBehaviour {

	public Vector3 moveDirection;
	public float jumpSpeed;
	private float velocity, acceleration;
	public float gravity = 8f;

	private bool jump, sprint, roll, hook, grav, rolling;

	CharacterController cc;
	MouseRotationX mrx;
	Animator animator;

	public GameObject cameraAnchor;
	private float radToDeg = 180 / Mathf.PI;
	private Quaternion cameraRotation;
	private Vector3 forward, right;

	private float currentRotation;
	public float turnRate = .75f;

	public float rollTime = .5f, rollStart = 0, rollSpeed = 10f;
	private float rollPause;
	private Vector3 rollDirection;


	private void Start() {
		cc = GetComponent<CharacterController>();
		mrx = GetComponent<MouseRotationX>();
		animator = GetComponent<Animator>();

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

			if (Input.GetAxisRaw("Move Horizontal") != 0 || Input.GetAxisRaw("Move Vertical") != 0) {
				if (!hook) {
					cameraRotation = cameraAnchor.transform.rotation;
					RotateTo(Mathf.Atan2(moveDirection.x, moveDirection.z) * radToDeg);
					cameraAnchor.transform.rotation = cameraRotation;
				}
			}

			velocity = moveDirection.magnitude;

			moveDirection.Set(0, 0, 0);

			if(jump) {
				//moveDirection.y += jumpSpeed;
			}

			//moveDirection.y -= gravity;

			animator.SetFloat("speed", velocity);
		}

		animator.SetBool("jump", jump);
		animator.SetBool("grounded", cc.isGrounded);

		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		if(info.IsName("Falling")) {
			cc.Move(Vector3.down * gravity * Time.deltaTime);
		}

		resetFlags();
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
