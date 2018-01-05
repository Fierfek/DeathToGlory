using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Shrine : MonoBehaviour {

    public float shrineHealRate;
    public GameControl gameControl;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameControl.Save();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {


            other.GetComponent<Health>().Heal(shrineHealRate);


        }
    }


}
