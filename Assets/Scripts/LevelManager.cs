using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name)
    {
        Debug.Log("Level Load Requested for: " + name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit Requested.");
    }
}
