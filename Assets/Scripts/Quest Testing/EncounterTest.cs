using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterTest : MonoBehaviour {

    private QuestTest eventScript;
    // Use this for initialization
    void Start () {
        eventScript = transform.parent.GetComponent<QuestTest>();
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        eventScript.RecieveTriggerEnter(name, other);
    }
}
