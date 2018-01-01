using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatus {

    //God Item abilities
    public static GameStatus currentSave;
    //public string saveName; when we need for save game names.
    public bool Hel;
    public bool Odin;
    public bool Loki;
    public bool FreyaFrey;
    public bool Thor;
    public bool Tyr;
    public bool Balder;
    public bool Eir;
    public bool Heimdallr;

    public GameStatus()
    {
        //saveName = "";
        Hel = false;
        Odin = false;
        Loki = false;
        FreyaFrey = false;
        Thor = false;
        Tyr = false;
        Balder = false;
        Eir = false;
        Heimdallr = false;
    }



}
