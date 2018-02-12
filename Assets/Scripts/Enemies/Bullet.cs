using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletDamage = 3f;
    public GameObject gunPosition;
    public bool direction = false;
    protected Health playerHealth;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Spin();
	}

    //
    private void OnTriggerEnter(Collider other)
    {
        //playerHealth.TakeDamage(bulletDamage); 
        Debug.Log("Bullet Damage");
    }

    //This is where the bullet object spins. 

    
void Spin()
    {
        if (direction)
        {
            transform.RotateAround(gunPosition.transform.position, Vector3.down, -90 * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(gunPosition.transform.position, Vector3.down, 90 * Time.deltaTime);
        }

    }
}
