using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Shrine : MonoBehaviour {

    public float shrineHealRate;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameControl.currentState.Save();
            while (other.GetComponent<Health>().GetHealth() < other.GetComponent<Health>().GetMaxHealth())
            {
                other.GetComponent<Health>().Heal(shrineHealRate);
            }

        }
    }


}
