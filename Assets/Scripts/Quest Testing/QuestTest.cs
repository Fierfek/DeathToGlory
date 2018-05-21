using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTest : MonoBehaviour {

    public GameObject door1;
    public GameObject door2;
    public Text textbox;
    bool encounter1Strt = false;
    bool encounter2Strt = false;
    bool encounter1Fin = false;
    bool encounter2Fin = false;

    float counter = 10f;

	// Use this for initialization
	void Start () {
        door1.SetActive(false);
        door2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (encounter1Strt)
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

        if (encounter1Fin)
        {
            textbox.text = "Encounter Area A Completed!";
        }

        if (encounter2Strt )
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

        if (encounter2Fin)
        {
            textbox.text = "Encounter Area B Completed!";
        }



	}


    public void RecieveTriggerEnter(string fromObject, Collider other)
    {
        if(fromObject == "Encounter Area A" && !encounter1Strt)
        {
            if(other.tag == "Player")
            {
                door1.SetActive(true);
                door2.SetActive(true);
                encounter1Strt = true;
                Debug.Log("Encounter A Starting!");
            }
        }

        if(fromObject == "Encounter Area B" && !encounter2Strt)
        {
            if(other.tag == "Player")
            {
                door2.SetActive(true);
                encounter2Strt = true;
                Debug.Log("Encounter B Starting!");
                counter = 15f;
            }
        }
    }

}
