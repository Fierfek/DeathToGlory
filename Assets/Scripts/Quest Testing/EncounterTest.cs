using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterTest : MonoBehaviour {

    public GameObject door2;
    public Text textbox;
    bool encounter1Strt = false;
    bool encounter2Strt = false;
    bool encounter1Fin = false;
    bool encounter2Fin = false;


    float counter = 10f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (encounter2Strt)
        {

            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }

            else
            {
                encounter2Fin = true;
                Debug.Log("Door2 Opened.");
                door2.SetActive(false);
            }
            string time = counter.ToString("f2");
            textbox.text = time;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (!encounter2Fin)
            {
                encounter2Strt = true;
                door2.SetActive(true);
                Debug.Log("Entered Zone 2");
                counter = 15f;
            }
        }
    }
}
