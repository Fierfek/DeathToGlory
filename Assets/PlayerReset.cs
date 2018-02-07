using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
			other.gameObject.transform.position = new Vector3(0, 0, 0);
		}
	}
}
