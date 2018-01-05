using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterMovement))]

public class MainCharacter : MonoBehaviour {

	Health health;
	CharacterMovement cm;
	
	public static bool updateStats;
	public Attack attack;
	public PlayerCamera pCamera;
    public float shrineHealRate;

	LayerMask mask = 1 << 2;

	private RaycastHit hit;
	private bool throwing, spinup, hitSomething;
	private float hookAxis = 0f;
	public Hook hook;
	public GameObject reticle;

	// Use this for initialization
	void Start() {
		health = GetComponent<Health>();
		health.SetHealth(100);
		cm = GetComponent<CharacterMovement>();
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

		if (Input.GetAxis("Hook Throw") > hookAxis && spinup && hitSomething) {
			spinup = false;
			throwing = true;
			//TODO: change transform.position to the hand's position;
			hook.throwHook(transform.position + pCamera.transform.forward, hit.point, 10f);
			print(hit.collider.gameObject.name);
		}

		hookAxis = Input.GetAxis("Hook Throw");

		if (hookAxis < 0 || throwing) {
			spinup = true;
			pCamera.ThrowHook();
			cm.HookShot();
			if(!throwing) {
				if (Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit, 15f, mask)) {
					hitSomething = true;
					reticle.SetActive(true);
					reticle.transform.position = hit.point; 
				} else {
					hitSomething = false;
					reticle.SetActive(false);
				}
			}

			if(hook.Done()) {
				throwing = false;
			} else {
				cm.gravityOff();
			}
		}

		if(hitSomething)
			Debug.DrawRay(pCamera.transform.position, pCamera.transform.forward * hit.distance, Color.black); //Debug only
	}

	public void BasicAttack() {
		attack.DoAttack(.5f);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shrine")
        {
            while(health.GetHealth() < health.GetMaxHealth())
            {
                health.Heal(shrineHealRate);
            }
            if (Input.GetButton("Interact"))
            {
                GameControl.saveState.Save();
            }
        }
    }
}