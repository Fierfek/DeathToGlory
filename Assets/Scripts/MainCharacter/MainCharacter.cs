using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(LedgeControlls))]

public class MainCharacter : MonoBehaviour {

	Health health;
	CharacterMovement cm;
	CharacterController cc;
	LedgeControlls lc;
	MouseRotation mr;
    PlyrThrown pt;
			
	public static bool updateStats;
	public Shotgun shotgun;
	public PlayerCamera pCamera;

	LayerMask mask = 1 << 2;

	private RaycastHit hit;
	private bool throwing, spinup, hitSomething;
	private float hookAxis = 0f, angle;
	public Hook hook;
	public GameObject reticle;
	public LineRenderer line;
	public SkinnedMeshRenderer axe, gun;

	private bool hanging;
	private float timer;

	// Use this for initialization
	void Start() {
		health = GetComponent<Health>();
		cm = GetComponent<CharacterMovement>();
		cc = GetComponent<CharacterController>();
		lc = GetComponent<LedgeControlls>();
		lc.enabled = false;
		mr = GetComponentInChildren<MouseRotation>();

		hanging = false;

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
			if(hanging) {
				lc.Jump();
				timer = Time.time + 1f;
			} else {
				cm.Jump();
			}
		}

		if (Input.GetButton("Roll")) {
			cm.Roll();
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
		if(!lc.enabled) {
			if (hit.gameObject.tag.Equals("Environment")) {
				angle = Vector3.Angle(hit.normal, Vector3.up);
				if (angle > 40 && angle <= 85) {
					cm.slide(hit.normal);
				}
			}
		}
		//if(!cc.isGrounded && !hook.Throwing() && hook.isActiveAndEnabled) {
		//	hook.Stop();
	}

	public void stopHanging(Vector3 movement) {
		lc.enabled = false;
		cm.enabled = true;
		mr.enabled = true;

		cm.stopHanging(movement);

		axe.enabled = true;
		gun.enabled = true;

		hanging = false;
	}

	void OnTriggerEnter(Collider c) {

		if(c.tag.Equals("Ledge")) {
			Debug.Log("triggered");
			if(timer <= Time.time) {
				if (!hanging) {
					cm.enabled = false;
					mr.enabled = false;
					lc.enabled = true;

					hanging = true;
					lc.setLedge(c.GetComponentInParent<Ledge>(), c);
				}
			}
		}
	}

    public void SetParalyze(bool isParalyzed)
    {
        if (isParalyzed)
        {
            cm.enabled = false;
            cc.enabled = false;
            lc.enabled = false;
            pt.enabled = true;
        }
        else
        {
            cm.enabled = true;
            cc.enabled = true;
            lc.enabled = false;
            pt.enabled = false;
        }

     
    }
}