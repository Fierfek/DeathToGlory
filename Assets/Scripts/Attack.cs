using UnityEngine;

//All damage according to character orientation. Character orientations should be halted druing attacks.

[RequireComponent(typeof(Rigidbody))]

public class Attack : MonoBehaviour {

	Rigidbody rigidbody;
	public GameObject[] path;
	float transitionTime, t, percent;
	int i, length;
	bool active;

	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
	}


	public void DoAttack(GameObject[] pathData, float time) {
		path = pathData;
		length = path.Length;
		transitionTime = time / length;
		i = 0;
		t = 0;
		active = true;
		gameObject.SetActive(active);
	}

	public void DoAttack(float time) {
		length = path.Length;
		transitionTime = time / length;
		i = 0;
		t = 0;
		active = true;
		gameObject.SetActive(active);
	}

	// Update is called once per frame
	void Update () {
		if(active) {
			t += Time.deltaTime;			

			if(t >= transitionTime) {
				i += 1;
				t = 0;
			}

			if (i >= length - 1) {
				active = false;
				gameObject.SetActive(active);
			} else {
				percent = t / transitionTime;

				/*transform.localPosition = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0
						 + (t * t * t - 2.0f * t * t + t) * m0
						 + (-2.0f * t * t * t + 3.0f * t * t) * p1
						 + (t * t * t - t * t) * m1;*/
				
				transform.localPosition = Vector3.Lerp(path[i].transform.localPosition, path[i + 1].transform.localPosition, percent);
				transform.localEulerAngles = new Vector3(Mathf.LerpAngle(path[i].transform.eulerAngles.x, path[i + 1].transform.eulerAngles.x, percent), Mathf.LerpAngle(path[i].transform.eulerAngles.y, path[i + 1].transform.eulerAngles.y, percent), Mathf.LerpAngle(path[i].transform.eulerAngles.z, path[i + 1].transform.eulerAngles.z, percent));
				transform.localScale = Vector3.Lerp(path[i].transform.localScale, path[i + 1].transform.localScale, percent);
			}			
		}
	}
}
