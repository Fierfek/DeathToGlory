using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

public class MainCharacter : MonoBehaviour {

	Health health;
	CharacterMovement cm;
	CharacterController cc;
	Rigidbody r;
	
	public static bool updateStats;
	public Attack attack;
	public Shotgun shotgun;
	public PlayerCamera pCamera;

    //public Slider healthSlider;

	LayerMask mask = 1 << 2;

	private RaycastHit hit;
	private bool throwing, spinup, hitSomething;
	private float hookAxis = 0f;
	public Hook hook;
	public GameObject reticle;

	// Use this for initialization
	void Start() {
		health = GetComponent<Health>();
		cm = GetComponent<CharacterMovement>();
		cc = GetComponent<CharacterController>();
		r = GetComponent<Rigidbody>();

		health.SetHealth(100);		
		mask = ~mask;
		r.isKinematic = true;
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

		if (Input.GetAxis("Hook Throw") > hookAxis && spinup && hitSomething) {
			spinup = false;
			//TODO: change transform.position to the hand's position;
			hook.throwHook(transform.position, hit.point, 10f);
		}

		hookAxis = Input.GetAxis("Hook Throw");

		if (hookAxis < 0 || hook.Throwing()) {
			spinup = true;
			pCamera.ThrowHook();
			cm.HookShot();
			if(!hook.Throwing()) {
				if (Physics.Raycast(transform.position, pCamera.transform.forward, out hit, 15f, mask)) {
					hitSomething = true;
					reticle.SetActive(true);
					reticle.transform.position = hit.point; 
				} else {
					hitSomething = false;
					reticle.SetActive(false);
				}
			}
		}

		if (hook.isActiveAndEnabled ) {
			if(!hook.Throwing() && !hook.Done() && !cc.isGrounded) {
				cm.gravityOff();
			}
		}

		//if(hitSomething)
		//	Debug.DrawRay(transform.position, pCamera.transform.forward * hit.distance, Color.black); //Debug only
	}

	public void BasicAttack() {
		attack.DoAttack(.5f);
	}

	//This checks if you collide with something before getting to the point you hooked;
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(!cc.isGrounded && !hook.Throwing() && hook.isActiveAndEnabled) {
			hook.Stop();
		}
	}

	
}