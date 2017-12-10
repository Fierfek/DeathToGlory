using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]

public class ControlScheme : MonoBehaviour {

	public float moveSpeed = 1;
	public float jumpSpeed = 1;
	public float gravity = 1;
	public float cameraPanSpeedX = 1;
	public float cameraPanSpeedY = 1;

	public Camera playerCamera;

	Vector3 moveDirection;

	CharacterController cc;

	private void Start() {
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		//This is close to out of combat controls.
		if(cc.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= moveSpeed;

			if (Input.GetButton("Jump")) {
				moveDirection.y = jumpSpeed;
			}

		}
		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);

		transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")  * cameraPanSpeedX, 0));
		transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * cameraPanSpeedY, 0, 0));
		transform.Rotate(0, 0, -transform.eulerAngles.z);
	}
}
