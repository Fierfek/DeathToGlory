using UnityEngine;

//Module class
//Used to define how much health an object has

[System.Serializable]
public class Health : MonoBehaviour {
	float currentHealth = 1;
	float startingHealth;
	float maxHealth;
	private float tempHealth;

	//initialize health
	public void SetHealth(float health) {
		maxHealth = currentHealth = startingHealth = health;
	}

	//Heal sets health to new max
	public void ModifyMax(float modifier, bool healToFull) {
		maxHealth += modifier;

		if (healToFull) {
			currentHealth = maxHealth;
		}
	}

	//Gets current health of object
	public float GetHealth() {
		return currentHealth;
	}

	//Get current max health of object
	public float GetMaxHealth() {
		return maxHealth;
	}

	//take damage, return true if hp drops to 0, 
	public bool TakeDamage(float damage) {
		if (damage > 0) {
			currentHealth -= damage;

			if (currentHealth <= 0) {
				return true;
			}
		}
		return false;
	}

	//heal object for an amount
	public void Heal(float healAmount) {
		tempHealth = currentHealth + healAmount;
		if (tempHealth > maxHealth) {
			currentHealth = maxHealth;
		} else {
			currentHealth = tempHealth;
		}
	}
}