using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestTestV2 : MonoBehaviour
{

    public GameObject[] doors;
    public GameObject[] enemyPrefabA;
    public Transform[] spawnPointA;
    public bool[] encounterStart = {false, false };
    public bool[] encounterEnd = {false, false };

    int enemyCounter;
    List<Enemy> enemiesAlive;
    public float despawnTime;


    public Text textbox;
    bool encounter1Strt = false;
    bool encounter1Fin = false;


    //float counter = 10f; //Here is the timer for testing.

    // Use this for initialization
    void Start()
    {
        
        //Opening the doors.
        for(int i = 0; i< doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
        enemiesAlive = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

        if (encounterStart[0] && !encounterEnd[0])
        {
            if (enemyCounter > 0)
            {
                //Showing the number of enemies alive in an area.
                enemyCounter = enemiesAlive.Count;
                string enemiesLeft = enemyCounter.ToString();
                textbox.text = enemiesLeft;

            }

            /*
             if (counter > 0)
             {
                 counter -= Time.deltaTime; // For testing the area closing and opening area with a timer.
             }
             */
            else
            {
                //Opening the doors after objective done.
                encounterEnd[0] = true;
                for(int i = 0; i< doors.Length; i++)
                {
                    doors[i].SetActive(false);
                }
            }


            //This is just for displaying the timer for the test.
            //string time = counter.ToString("f2");
            //textbox.text = time;

        }

        if (encounter1Fin)
        {
            //Clearing the enemies alive list and showing the area completion message.
            textbox.text = "Encounter Area A Completed!";
            enemiesAlive.Clear();
        }




    }

    //This closes all the doors in an area (fromObject) where the player or enemies (other) enter/spawn
    //The box collider we want to use must have a private instance of QuestTest in order to use.
    //This function must be inside OnTriggerEnter of a collider script.
    public void RecieveTriggerEnter(string fromObject, Collider other)
    {
        if (fromObject == "Encounter Area A" && !encounterStart[0])
        {
            if (other.tag == "Player")
            {
                //Doors are closed here
                for(int i = 0; i<doors.Length; i++)
                {
                    doors[i].SetActive(true);
                }
                encounter1Strt = true;
                enemyCounter = enemyPrefabA.Length;
                //Enemies spawn


                for (int i = 0; i < enemyCounter; i++)
                {
                    //This will spawn an enemy and add said enemy to the list of enemies alive
                    GameObject minion = Instantiate(enemyPrefabA[i], spawnPointA[i].position, spawnPointA[i].rotation);
                    enemiesAlive.Add(minion.GetComponent<Enemy>());
                    Debug.Log("A minion spawned.");

                }

                Debug.Log("Encounter A Starting!");
            }
        }

        if (fromObject == "Encounter Area A" && encounterStart[0]) // For removing enemies from list when dead.
        {
            //Checking if the enemy is dead and is in the list to despawn.
            if (other.tag == "Enemy" && other.GetComponent<Enemy>().isDead() && enemiesAlive.Contains(other.GetComponent<Enemy>()))
            {
                enemyCounter--;
                enemiesAlive.Remove(other.GetComponent<Enemy>()); // This will remove the enemy from the alive list.
                Destroy(other, despawnTime); //Enemies will despawn after a certain amount of time(despawnTime).
            }
        }


    }


}

