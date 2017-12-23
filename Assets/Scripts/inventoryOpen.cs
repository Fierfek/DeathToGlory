using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryOpen : MonoBehaviour {

	bool currentState;
	public GameObject inventoryPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Inventory")) {
			currentState = !currentState;
			inventoryPanel.SetActive(currentState);
		}
	}
}
