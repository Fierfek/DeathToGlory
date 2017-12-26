using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public int armor;
    public double movement;
    public float movementMod;
    public int magicDefense;
    public int magicAttack;
    public int attackStrength;
    public int spirit;
    


	// Use this for initialization
	void Start () {
        maxHealth = 10;
        movementMod = 1.00000f;
        movement = 1 * movementMod;
        armor = 3;
        magicAttack = 3;
        magicDefense = 3;
        attackStrength = 3;
        spirit = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
