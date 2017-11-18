using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class movement : MonoBehaviour {

	CharacterController cc;

	public float speed = 5;
	public float jumpSpeed = 8;
	public float gravity = 20;
	public Vector3 move = Vector3.zero;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cc.isGrounded) {
			move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			move = transform.TransformDirection(move);
			move *= speed;

			if(Input.GetButton("Jump")) {
				move.y = jumpSpeed;
			}

			if(Input.GetButton("Sprint")) {
				speed = 10;
			} else {
				speed = 5;
			}
		}

		move.y -= gravity * Time.deltaTime;
		cc.Move(move * Time.deltaTime);
	}
}
