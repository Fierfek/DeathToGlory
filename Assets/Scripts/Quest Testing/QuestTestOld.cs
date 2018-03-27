using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTestOld : MonoBehaviour {

    public GameObject door1;
    public GameObject door2;
    public Text textbox;
    bool encounter1Strt = false;
    bool encounter2Strt = false;
    bool encounter1Fin = false;
    bool encounter2Fin = false;
    public bool isEncounter2 = false;

    float counter = 10f;

	// Use this for initialization
	void Start () {
        door1.SetActive(false);
        door2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (encounter1Strt && !isEncounter2)
        {
            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }
            else
            {
                encounter1Fin = true;
                door1.SetActive(false);
                door2.SetActive(false);
            } 
            string time = counter.ToString("f2");
            textbox.text =  time;
            
        }

        if (encounter2Strt && isEncounter2)
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

        else
        {

        }
        

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!encounter1Fin  && !isEncounter2)
            {
                encounter1Strt = true;
                door1.SetActive(true);
                door2.SetActive(true);
                
                Debug.Log("Entered Zone 1");
            }
            if(!encounter2Fin && isEncounter2)
            {
                encounter2Strt = true;
                door2.SetActive(true);
                Debug.Log("Entered Zone 2");
                counter = 15f;
            }
        }
    }



}
