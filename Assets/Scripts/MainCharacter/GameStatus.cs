using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStatus : MonoBehaviour {

    //God Item abilities
   [Header("God Items")]
    public bool Hel;
    public bool Odin;
    public bool Loki;
    public bool FreyaFrey;
    public bool Thor;
    public bool Tyr;
    public bool Balder;
    public bool Eir;
    public bool Heimdallr;

    [Header("Player State")]
    public Transform playerTransform;
    public Health playerHealth;
    //health, location Cannot be monobehavior 

    private GameState gs;
    public GameState Save()
    {
        gs.Hel = Hel;
        gs.Odin = Odin;
        gs.Loki = Loki;
        gs.FreyaFrey = FreyaFrey;
        gs.Thor = Thor;
        gs.Tyr = Tyr;
        gs.Balder = Balder;
        gs.Eir = Eir;
        gs.Heimdallr = Heimdallr;
        gs.playerHealth = playerHealth.GetMaxHealth();

        gs.x = playerTransform.position.x;
        gs.y = playerTransform.position.y;
        gs.z = playerTransform.position.z;
        gs.rotX = playerTransform.eulerAngles.x;
        gs.rotY = playerTransform.eulerAngles.y;
        gs.rotZ = playerTransform.eulerAngles.z;
        //gs.playerRotation = playerTransform.rotation.eulerAngles;

        return gs;
    }

    private void Awake()
    {
        gs = new GameState();
    }

    public void Deserialize(GameState save)
    {
        gs = save;
        Load();
    }

    private void Load()
    {
        Hel = gs.Hel;
        Odin = gs.Odin;
        Loki = gs.Loki;
        FreyaFrey = gs.FreyaFrey;
        Thor = gs.Thor;
        Tyr = gs.Tyr;
        Balder = gs.Balder;
        Eir = gs.Eir;
        Heimdallr = gs.Heimdallr;
        playerHealth.SetHealth(gs.playerHealth);
        playerTransform.position = new Vector3 (gs.x, gs.y, gs.z);
        Vector3 direction = new Vector3(gs.rotX, gs.rotY, gs.rotZ);
        playerTransform.Rotate(direction, Space.Self);
       
    }

}



[System.Serializable]
public class GameState
{
    public bool Hel;
    public bool Odin;
    public bool Loki;
    public bool FreyaFrey;
    public bool Thor;
    public bool Tyr;
    public bool Balder;
    public bool Eir;
    public bool Heimdallr;


    public float x, y, z;
    public float rotX, rotY, rotZ;
    //public Vector3 playerRotation;
    public float playerHealth;

}
