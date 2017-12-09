using UnityEngine;

//Handles the control input

[RequireComponent(typeof(CharacterController))]

public class ControlScheme : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public float gravity;

	public Camera playerCamera;

	Vector3 moveDirection;

	CharacterController cc;

	private void Start() {
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		if(cc.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= moveSpeed;

			if (Input.GetButton("Jump")) {
				moveDirection.y = jumpSpeed;
				print("jumping");
			}

		} else {
			//links to in air combos
		}
		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);

		transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
		transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));
		transform.Rotate(0, 0, -transform.eulerAngles.z);


	}
}
