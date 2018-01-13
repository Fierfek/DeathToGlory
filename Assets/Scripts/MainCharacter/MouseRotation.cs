using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour {

	public float rotationSpeedX = 3, rotationSpeedY = 3;
	public float yMin = -20f, yMax = 80f;

	private Quaternion rotation;
	private float x, y;

	// Update is called once per frame
	void Update() {
		x += Input.GetAxis("Look Horizontal") * rotationSpeedX;
		y -= Input.GetAxis("Look Vertical") * rotationSpeedY;

		y = ClampAngle(y, yMin, yMax);


		rotation = Quaternion.Euler(y, x, 0);

		transform.rotation = rotation;
	}

	private float ClampAngle(float angle, float min, float max) {
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}