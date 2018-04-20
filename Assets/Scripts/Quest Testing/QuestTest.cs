using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTest : MonoBehaviour {

    public GameObject door1;
    public GameObject door2;
    public GameObject [] enemyPrefabA;
    public GameObject[] enemyPrefabB;
    public Transform [] spawnPointA;
    public Transform [] spawnPointB;


    int enemyCounter;
    List<Enemy> enemiesAlive;
    public float despawnTime;


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
        enemiesAlive = new List<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {

        if (encounter1Strt && !encounter1Fin)
        {
            if(enemyCounter > 0)
            {

               enemyCounter = enemiesAlive.Count;
               string enemiesLeft = enemyCounter.ToString();               
               textbox.text =  enemiesLeft;
                
            }

           /*
            if (counter > 0)
            {
                counter -= Time.deltaTime; // For testing the area closing and opening area with a timer.
            }
            */
            else
            {
                encounter1Fin = true;
                door1.SetActive(false);
                door2.SetActive(false);
            }



            //string time = counter.ToString("f2");
            //textbox.text = time;
            
        }

        if (encounter1Fin)
        {
            textbox.text = "Encounter Area A Completed!";
            enemiesAlive.Clear();
        }

        if (encounter2Strt && !encounter2Fin)
        {
            /*
            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }*/

            if (enemyCounter > 0)
            {
                enemyCounter = enemiesAlive.Count;
                string enemiesLeft = enemyCounter.ToString();
                textbox.text = enemiesLeft;

            }

            else
            {
                encounter2Fin = true;
                Debug.Log("Door2 Opened.");

                door2.SetActive(false);
            }
            /*
            string time = counter.ToString("f2");
            textbox.text = time;
            */
        }

        if (encounter2Fin)
        {
            textbox.text = "Encounter Area B Completed!";
            enemiesAlive.Clear();
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
                enemyCounter = enemyPrefabA.Length;
                //Enemies spawn

                //Instantiate using a for loop.
                for(int i =0; i< enemyCounter; i++)
                {

                    GameObject minion = Instantiate(enemyPrefabA[i], spawnPointA[i].position, spawnPointA[i].rotation);
                    enemiesAlive.Add(minion.GetComponent<Enemy>());
                    Debug.Log("A minion spawned.");

                    //bool isDead = minion.GetComponent<Enemy>().isDead(); //Just checking if the bool works.
                }

                Debug.Log("Encounter A Starting!");
            }
        }

        if(fromObject == "Encounter Area A" && encounter1Strt) // For removing enemies from list when dead.
        {
            if(other.tag== "Enemy" && other.GetComponent<Enemy>().isDead())
            {
                enemiesAlive.Remove(other.GetComponent<Enemy>());
                Destroy(other, despawnTime);
            }
        }

        if(fromObject == "Encounter Area B" && !encounter2Strt)
        {
            if(other.tag == "Player")
            {
                door2.SetActive(true);
                encounter2Strt = true;
                enemyCounter = enemyPrefabB.Length;
                //Enemies spawn

                for (int i = 0; i < enemyCounter; i++)
                {
                    GameObject minion = Instantiate(enemyPrefabA[i], spawnPointB[i], spawnPointB[i]);
                    enemiesAlive.Add(minion.GetComponent<Enemy>());
                }

                Debug.Log("Encounter B Starting!");
                counter = 15f;
            }
        }

        if(fromObject == "Encounter Area B" && encounter2Strt)
        {
            enemiesAlive.Remove(other.GetComponent<Enemy>());
            Destroy(other, despawnTime);
        }
    }


}
