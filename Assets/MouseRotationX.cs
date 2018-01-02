using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationX : MonoBehaviour {

	private Quaternion rotation;
	public GameObject cameraAnchor;

	// Update is called once per frame
	void Update() {
		rotation = Quaternion.Euler(0, cameraAnchor.transform.rotation.eulerAngles.y, 0);
		transform.rotation = rotation;
	}
}