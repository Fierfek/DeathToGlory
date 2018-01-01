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

	// Use this for initialization
	void Start() {
		health = GetComponent<Health>();
		health.SetHealth(100);
		cm = GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Basic Attack")) {
			BasicAttack();
		}

		if (Input.GetButton("Sprint")) {
			cm.Sprint();
		}

		if (Input.GetButtonDown("Jump")) {
			cm.Jump();
		}

		if (Input.GetButton("Roll")) {
			cm.Roll();
			//roll
		}

		if(Input.GetAxis("Hook Throw") == -1) {
			pCamera.ThrowHook();
			cm.HookShot();
		}
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