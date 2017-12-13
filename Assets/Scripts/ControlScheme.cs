using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]

public class ControlScheme : MonoBehaviour {

	private Vector3 moveDirection;
	public float moveSpeed = 1;
	public float sprintSpeed = 2;
	public float jumpSpeed = 1;
	public float gravity = 1;
	public float adjust = 0;
	
	CharacterController cc;

	public GameObject cameraAnchor;
	private float radToDeg = 180 / Mathf.PI;
	private Quaternion cameraRotation;
	private Vector3 forward, right;

	private void Start() {
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {

		if(cc.isGrounded) {
			//Find the foreward relative to the camera
			forward = cameraAnchor.transform.forward.normalized;
			forward.y = 0;
			right = new Vector3(forward.z, 0, -forward.x);

			//set the movement direction
			moveDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;

			//save current camera rotation, rotate the character, then unrotate the camera.
			cameraRotation = cameraAnchor.transform.rotation;
			transform.eulerAngles = new Vector3(0, Mathf.Atan2(moveDirection.x, moveDirection.z) * radToDeg, 0);
			cameraAnchor.transform.rotation = cameraRotation;

			if (Input.GetButton("Sprint")) {
				moveDirection *= sprintSpeed;
			} else {
				moveDirection *= moveSpeed;
			}
			
			if (Input.GetButton("Jump")) {
				moveDirection.y = jumpSpeed;
			}

		}

		//set gravity & move;
		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);
	}
}