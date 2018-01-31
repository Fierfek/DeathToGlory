
using UnityEngine;

//attatch to enemy weapon
//checks for collision of main character and this object
public class EnemyWeapon : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        transform.root.gameObject.SendMessage("OnTriggerEnter", collider);
    }
}
