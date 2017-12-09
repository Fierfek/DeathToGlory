using UnityEngine;

//Module class
//Used to define how much health an object has

public class Health : MonoBehaviour {
	float health = 0;

	public void setHealth(float health) {
		this.health = health;
	}

	public float getHealth() {
		return health;
	}
}
