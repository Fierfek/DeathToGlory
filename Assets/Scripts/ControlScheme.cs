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

	private void Start() {
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {

		if(cc.isGrounded) {
			//save current camera rotation, rotate the character, then unrotate the camera.
			cameraRotation = cameraAnchor.transform.rotation;
			transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * radToDeg, 0);
			cameraAnchor.transform.rotation = cameraRotation;

			//set the movement direction
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

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
