using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour {

    GameObject floor;
    int childCount;

	// Use this for initialization
	void Start () {
        floor = GameObject.Find("Floor");
        childCount = floor.transform.childCount;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
