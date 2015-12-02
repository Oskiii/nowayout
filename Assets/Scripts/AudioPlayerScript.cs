using UnityEngine;
using System.Collections;

public class AudioPlayerScript : MonoBehaviour {

	public AudioSource audioSource1;
	public AudioSource audioSource2;
	
	public AudioClip[] audioClips;
	public AudioClip[] audioEffects;
	private int currentlyPlaying;

	public static AudioPlayerScript current;

	void Awake (){
		current = this;
		audioSource1.loop = true;
	}

	public void PlaySong (int songID){

		audioSource1.Stop();

		audioSource1.clip = audioClips [songID];
		audioSource1.Play();

	}

	public void PlayEffect (int effectID){
		audioSource2.Stop ();
		audioSource2.clip = audioEffects[effectID];
		audioSource2.Play ();
	}
}
