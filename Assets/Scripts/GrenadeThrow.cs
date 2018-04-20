using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour {

    public float throwForce = 20f;
    public GameObject grenadePrefab;

    private void Start()
    {
        InvokeRepeating("ThrowBomb", 2.0f, 3.0f);
    }
    private void Update()
    {
        
    }

    void ThrowBomb()
    {
            GameObject bomb = Instantiate(grenadePrefab, transform.position, transform.rotation);
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }


}
