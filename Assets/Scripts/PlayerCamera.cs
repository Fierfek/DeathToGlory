using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public GameObject lookAt;
	public GameObject anchor;
	public Vector3 offset;

	public float distance;

	// Use this for initialization
	void Start() {
		transform.localPosition = offset;
	}

	// Update is called once per frame
	void Update() {
		transform.LookAt(lookAt.transform);
	}
}
