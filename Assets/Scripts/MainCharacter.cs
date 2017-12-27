using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ControlScheme))]
//hello
public class MainCharacter : MonoBehaviour {

	Health health;
    public Inventory inventory;
    public static bool updateStats;
    public UniqueSlot[]  equipped;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();

		health.setHealth(100);


        CharacterStats.moveSpeed = 5f;
        CharacterStats.sprintSpeed = 10f;
        CharacterStats.jumpSpeed = 5f;
        CharacterStats.gravity = 9.8f;
        CharacterStats.armor = 3;
        CharacterStats.magicAtk = 3;
        CharacterStats.magicDef = 3;
        CharacterStats.attack = 3;
        CharacterStats.spirit = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (updateStats)
        {
            foreach (UniqueSlot slot in equipped)
            {
                CharacterStats.armorMod += slot.CurrentItem.armorMod;
                CharacterStats.movementMod += slot.CurrentItem.movementMod;
                CharacterStats.magicAtkMod += slot.CurrentItem.magicAtkMod;
                CharacterStats.magicDefMod += slot.CurrentItem.magicDefMod;
                CharacterStats.attackMod += slot.CurrentItem.attackMod;
                CharacterStats.spiritMod += slot.CurrentItem.spiritMod;
            }
            updateStats = false;
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            inventory.AddItem(other.GetComponent<Item>());
        }
    }
}