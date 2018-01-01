using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public Transform lookAt;
	public Transform anchor;
	private Vector3 offset;

	[Header("Offsets")]
	public Vector3 standartOffset;
	public Vector3 battleOffset;
	public Vector3 hookOffset;

	private float distance;
	private RaycastHit hit;

	private bool hook = false;
	public float speed = .2f;
	private float percent = 1f;

	// Use this for initialization
	void Start() {
		offset = standartOffset;
		transform.localPosition = offset;
	}

	// Update is called once per frame
	void Update() {

		if(hook) {
			offset = Vector3.MoveTowards(offset, hookOffset, speed);
		} else {
			offset = Vector3.MoveTowards(offset, standartOffset, speed);
		}

		if (Physics.Linecast(lookAt.position, anchor.TransformPoint(offset), out hit)) {
			if(hit.collider.gameObject.tag.Equals("Player")) {

			} else {
				distance = -hit.distance;
				transform.localPosition = new Vector3(0.0f, 1.0f, distance);
			}			
		} else {
			transform.localPosition = offset;
		}

		hook = false;	
	}

	public void ThrowHook() {
		hook = true;
	}
}