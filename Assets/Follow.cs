using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	Transform followPoint;
	bool follow;

	public void Setup(Transform t) {
		followPoint = t;
	}
	
	// Update is called once per frame
	void Update () {
		if(follow) {
			transform.position = followPoint.position;
		}
	}

	public void SetFollow(bool t) {
		follow = t;
	}
}
