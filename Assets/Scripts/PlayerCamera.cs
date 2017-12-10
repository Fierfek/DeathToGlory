using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public Vector3 standardOffset;

	//Can be used for sprint or dash
	public Vector3 sprintOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Sprint")) {
			transform.localPosition = sprintOffset;
		} else {
			transform.localPosition = standardOffset;
		}
	}
}
