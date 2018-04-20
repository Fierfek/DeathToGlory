using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHide : MonoBehaviour {

	CursorLockMode mode;

	// Use this for initialization
	void Start () {
		mode = CursorLockMode.Locked;
		Cursor.visible = false;
		//Cursor.lockState = mode;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
