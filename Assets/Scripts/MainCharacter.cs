using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ControlScheme))]
//hello
public class MainCharacter : MonoBehaviour {

	Health health;
    public Inventory inventory;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();

		health.setHealth(100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            inventory.AddItem(other.GetComponent<Item>());
        }
    }
}