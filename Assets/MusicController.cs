using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public static MusicController instance;

	private AudioSource bgMusic, click;

	private float time;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();

		AudioSource[] audioSources = GetComponents<AudioSource> ();
		bgMusic = audioSources [0];
		click = audioSources [1];

	}

	void MakeSingleton(){
		if (instance != null) {
			//permet de détruire le gameObject si celui-ci est créé alors que l'on revient sur la sc1ene initiale où Unity recrée les différents GameObject de la hierarchy
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	} 

}
