using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameControl : MonoBehaviour {

    //In case we want a list of saved games.
    //public static List<GameStatus> savedGames = new List<GameStatus>();
    public static GameControl currentState;

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


    public void Save()
    {
        
        BinaryFormatter encripter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        GameStatus data = new GameStatus();
        
        encripter.Serialize(file, data);
        //encripter.Serialize(file, GameControl.savedGames);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter decripter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            GameStatus data = (GameStatus)decripter.Deserialize(file);
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




