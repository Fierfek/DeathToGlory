using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrThrown : MonoBehaviour {


    public float hurlSpeed = 10f;
    public Vector3 hurlDirection;
    bool throwing;

    private MainCharacter mc;
    private Health hlth;
    CharacterController cc;

    // Use this for initialization
    void Start () {
        hurlDirection = Vector3.zero;
        throwing = false;
        mc = GetComponent<MainCharacter>();
        hlth = GetComponent<Health>();
        cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (throwing)
        {
            cc.Move(hurlDirection * hurlSpeed * Time.deltaTime);
        }
	}

    public void StartThrow(Vector3 direction)
    {
        throwing = true;
        hurlDirection = direction;
        //Damage player
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Pillar"))
        {
            throwing = false;
            mc.SetParalyze(false);
        }
    }
}
