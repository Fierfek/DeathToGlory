using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public GameObject lookAt;
	public GameObject anchor;
	public Vector3 offset;
	public Vector3 battleOffset;

	public float distance = 1;

	// Use this for initialization
	void Start() {
		transform.localPosition = offset * distance;
	}

	// Update is called once per frame
	void Update() {
		transform.LookAt(lookAt.transform);
	}
}
