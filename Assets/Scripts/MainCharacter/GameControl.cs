using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameControl : MonoBehaviour {

    //In case we want a list of saved games.
    //public static List<GameStatus> savedGames = new List<GameStatus>();
    public static GameControl saveState;

    //This makes sure that once the player transitions, progress is kept.
    private void Awake()
    {
        if(saveState == null)
        {
            DontDestroyOnLoad(gameObject);
            saveState = this;
        }
        if(saveState != this)
        {
            Destroy(gameObject);
        }
    }


    public void Save()
    {
        
        BinaryFormatter encripter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        //encripter.Serialize(file, GameControl.savedGames);
        encripter.Serialize(file, saveState);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter decripter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            saveState = (GameControl)decripter.Deserialize(file);
            //GameControl.savedGames = (List<GameStatus>)decripter.Deserialize(file);
            file.Close();
            Debug.Log("Game Loaded.");
        }
    }
}


