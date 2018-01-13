using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class DropPlatform : MonoBehaviour {

	public float speed, time, wait;
	private bool drop = false;
	private Vector3 startingPosition;
	private float startTime;

	// Use this for initialization
	void Start () {
		startingPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(drop) {
			if(Time.time < startTime + time) {
				transform.localPosition -= Vector3.down * speed * Time.deltaTime;
			} else {
				drop = false;
				transform.localPosition = startingPosition;
			}
		}
	}

	void OnColliderEnter(Collider other) {
		drop = true;
		startTime = Time.time;
	}
}
