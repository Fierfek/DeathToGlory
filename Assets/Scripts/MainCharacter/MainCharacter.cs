﻿using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterController))]

public class MainCharacter : MonoBehaviour {

	Health health;
	CharacterMovement cm;
	CharacterController cc;
	
	public static bool updateStats;
	public Shotgun shotgun;
	public PlayerCamera pCamera;

	LayerMask mask = 1 << 2;

	private RaycastHit hit;
	private bool throwing, spinup, hitSomething;
	private float hookAxis = 0f;
	public Hook hook;
	public GameObject reticle;
	public LineRenderer line;

	// Use this for initialization
	void Start() {
		health = GetComponent<Health>();
		cm = GetComponent<CharacterMovement>();
		cc = GetComponent<CharacterController>();

		health.SetHealth(100);
		mask = ~mask;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Basic Attack")) {
			BasicAttack();
		}

		if (Input.GetButton("Sprint")) {
			cm.Sprint();
		}

		if (Input.GetButton("Jump")) {
			cm.Jump();
		}

		if (Input.GetButton("Roll")) {
			cm.Roll();
			//roll
		}

		if (Input.GetAxis("Hook Throw") > hookAxis && spinup) {
			spinup = false;
			reticle.SetActive(false);
			line.enabled = false;
			//TODO: change transform.position to the hand's position;
			hook.throwHook(transform.position, pCamera.transform.forward);
		}

		hookAxis = Input.GetAxis("Hook Throw");

		if (hookAxis < 0) {
			spinup = true;

			pCamera.ThrowHook();
			cm.Hook();

			reticle.SetActive(true);
			if (Physics.Raycast(transform.position, pCamera.transform.forward, out hit, 15f, mask)) {
				reticle.transform.position = hit.point;
			} else {
				reticle.transform.position = transform.position + pCamera.transform.forward * 15f;
			}
			if(spinup) {
				line.enabled = true;

				line.SetColors(Color.green, Color.green);
				line.SetWidth(.01f, .01f);
				line.SetPosition(0, transform.position);
				line.SetPosition(1, reticle.transform.position);
			}
		}

		if (hook.isActiveAndEnabled) {
			if(!hook.Done()) {
				pCamera.ThrowHook();
				cm.Hook();
			}

			if(!hook.Throwing() && hook.Moving()) {
				cm.gravityOff();
			}
		}

		//if(hitSomething)
			//Debug.DrawRay(transform.position, pCamera.transform.forward * hit.distance, Color.black); //Debug only
	}

	public void BasicAttack() {

	}

	//This checks if you collide with something before getting to the point you hooked;
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag.Equals("Environment")) {
			if (Vector3.Angle(hit.normal, Vector3.up) > 40) {
				cm.slide(hit.normal);
			}
		}

		//if(!cc.isGrounded && !hook.Throwing() && hook.isActiveAndEnabled) {
		//	hook.Stop();
				
	}
}