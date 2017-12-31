using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public Transform lookAt;
	public Transform anchor;
	private Vector3 offset;

	[Header("Offsets")]
	public Vector3 standartOffset;
	public Vector3 battleOffset;

	private float distance;
	private RaycastHit hit;

	// Use this for initialization
	void Start() {
		offset = standartOffset;
		transform.localPosition = offset;
	}

	// Update is called once per frame
	void Update() {
		transform.LookAt(lookAt);

		if (Physics.Linecast(lookAt.position, anchor.TransformPoint(offset), out hit)) {
			distance = -hit.distance;
			transform.localPosition = new Vector3(0.0f, 1.0f, distance);
		} else {
			transform.localPosition = offset;
		}
	}
}