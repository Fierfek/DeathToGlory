using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationX : MonoBehaviour {

	public float rotationSpeedX, rotationSpeedY;

	private Quaternion rotation;
	private float x;

	// Update is called once per frame
	void Update() {
		x += Input.GetAxis("Look Horizontal") * rotationSpeedX;

		rotation = Quaternion.Euler(0, x, 0);

		transform.rotation = rotation;
	}
}