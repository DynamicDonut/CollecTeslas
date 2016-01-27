using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public AudioClip[] musicLib;
	GameObject myPlayer;
	AudioSource myMusic;

	public bool swapMusic = false;

	// Use this for initialization
	void Start () {
		myPlayer = GameObject.Find ("Player");
		myMusic = GetComponent<AudioSource> ();
		myMusic.clip = musicLib [0];
		myMusic.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (swapMusic) {
			if (myPlayer.GetComponent<PlayerMovement> ().lowPower) {
				myMusic.clip = musicLib [1];
			} else if (!myPlayer.GetComponent<PlayerMovement> ().lowPower) {
				myMusic.clip = musicLib [0];
			}
			myMusic.Play ();
			swapMusic = false;
		}
	}
}
