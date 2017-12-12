using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour {

	public float rotationSpeedX, rotationSpeedY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeedX, 0));
		transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotationSpeedY, 0, 0));
		transform.Rotate(0, 0, -transform.eulerAngles.z);
	}
}
