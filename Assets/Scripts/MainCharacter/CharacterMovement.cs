using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour {

	public Vector3 moveDirection;
	public float moveSpeed = 1;
	public float sprintSpeed = 2;
	public float jumpSpeed = 1;
	public float gravity = 1;

	private bool jump, sprint, roll;

	CharacterController cc;

	public GameObject cameraAnchor;
	private float radToDeg = 180 / Mathf.PI;
	private Quaternion cameraRotation;
	private Vector3 forward, right;

	//inputs
	private float currentRotation;
	public float turnRate = .75f;

	private void Start() {
		cc = GetComponent<CharacterController>();
		jump = sprint = roll = false;
	}

	// Update is called once per frame
	void Update() {
		if (cc.isGrounded) {

			//Find the foreward relative to the camera
			forward = cameraAnchor.transform.forward.normalized;
			forward.y = 0;
			right = new Vector3(forward.z, 0, -forward.x);

			//set the movements direction
			moveDirection = Input.GetAxis("Move Horizontal") * right + Input.GetAxis("Move Vertical") * forward;

			if (Input.GetAxisRaw("Move Horizontal") != 0 || Input.GetAxisRaw("Move Vertical") != 0) {
				//save current camera rotation, rotate the character, then unrotate the camera.
				cameraRotation = cameraAnchor.transform.rotation;
				RotateTo(Mathf.Atan2(moveDirection.x, moveDirection.z) * radToDeg);
				cameraAnchor.transform.rotation = cameraRotation;
			}

			if (jump) {
				moveDirection.y += jumpSpeed;
			}

			if(sprint) {
				moveDirection *= sprintSpeed;
			} else {
				moveDirection *= moveSpeed;
			}

			if(roll) {
				//roll
			}

			resetFlags();

		}

		//set gravity & move;
		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);
	}

	public void Jump() {
		jump = true;
	}

	public void Sprint() {
		sprint = true;
	}

	public void Roll() {
		roll = true;
	}

	private void resetFlags() {
		jump = false;
		sprint = false;
		roll = false;
	}

	private void RotateTo(float angle) {
		currentRotation = transform.eulerAngles.y;
		transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(currentRotation, angle, turnRate), 0);
	}
}
