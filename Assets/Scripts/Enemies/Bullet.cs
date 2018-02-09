using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletDamage = 3f;
    public GameObject gunPosition;
    protected Health playerHealth;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Spin();
	}

    private void OnTriggerEnter(Collider other)
    {
        //playerHealth.TakeDamage(bulletDamage); 
        Debug.Log("Bullet Damage");
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    void Spin()
    {
        transform.RotateAround(gunPosition.transform.position, Vector3.down, 90 * Time.deltaTime);
    }
}
