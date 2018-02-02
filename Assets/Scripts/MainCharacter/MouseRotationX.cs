using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationX : MonoBehaviour {

	public GameObject cameraAnchor;

	// Update is called once per frame
	void Update() {
		transform.rotation = Quaternion.Euler(0, cameraAnchor.transform.rotation.eulerAngles.y, 0);
	}
}