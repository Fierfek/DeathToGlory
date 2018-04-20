using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public void OnTriggerEnter(Collider c) {
		if(c.name.Equals("Draugr")) {
			c.transform.GetComponent<Health>().TakeDamage(10);
		}
	}
}
