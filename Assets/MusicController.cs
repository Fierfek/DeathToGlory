using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	public AudioSource speaker, speaker2;
	AudioClip scheduledClip;
	float startTime;
	float waitTime;
	bool scheduledLoop, scheduled;

	// Use this for initialization
	void Awake () {
		scheduled = false;
		speaker.loop = false;
		speaker2.loop = true;
	}

	public void PlayOne(AudioClip clip) {
		speaker.clip = clip;
		speaker.Play();
	}

	public void setVolumeOne(float percent) {
		if(percent >= 0f && percent <= 1f)
			speaker.volume = percent;
	}

	public void Schedule(AudioClip clip) {
		speaker2.clip = clip;
		speaker2.PlayScheduled(AudioSettings.dspTime + speaker.clip.length - speaker.time);
	}

	void Update() {
		
	}
}
