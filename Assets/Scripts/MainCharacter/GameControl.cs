using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(GameStatus))]
public class GameControl : MonoBehaviour {

    //In case we want a list of saved games.
    //public static List<GameStatus> savedGames = new List<GameStatus>();
    public static GameControl currentState;
    public GameStatus data;

    //This makes sure that once the player transitions, progress is kept.
    private void Awake()
    {
        if(currentState == null)
        {
            DontDestroyOnLoad(gameObject);
            currentState = this;
        }
        if(currentState != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        data = GetComponent<GameStatus>();
        //Load(); //Test if load works.
    }

    public void Save()
    {
        
        BinaryFormatter encripter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        
        encripter.Serialize(file, data.Save());
        //encripter.Serialize(file, GameControl.savedGames);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter decripter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            data.Deserialize((GameState)decripter.Deserialize(file));
            //GameControl.savedGames = (List<GameStatus>)decripter.Deserialize(file);
            file.Close();

            Debug.Log("Game Loaded.");
        }
        else
        {
            Debug.Log("File does not exist.");
        }
    }


}




