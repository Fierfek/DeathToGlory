using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

	public Transform playerCamera;
	public AudioClip opening, openingLoop;
	MusicController music;

	// Use this for initialization
	void Start () {
		music = playerCamera.GetComponent<MusicController>();
		music.PlayOne(opening);
		music.Schedule(openingLoop);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
