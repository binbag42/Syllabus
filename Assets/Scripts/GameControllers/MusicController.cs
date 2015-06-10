using UnityEngine;
using System.Collections;

// handle play of background music and various sounds
// checks the playerpref to decide if music/FXhave to be played

public class MusicController : MonoBehaviour {

	public static MusicController instance;

	private AudioSource bgMusic, click;

	// save the timestamp of the bgMusic when stopped to restart where it was stopped
	//	initialise at 0 so that the first time ti starts at the beginning
	private float time=0.0f;

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

	public void OnLevelWasLoaded(){
		if (Application.loadedLevelName == "LevelMenu") {
			if (GameController.instance.isMusicOn){
				if (!bgMusic.isPlaying){
					bgMusic.time=time;
					bgMusic.Play();
				}
			}
		
		}
	}

	public void PlayBgMusic(){
		if (!bgMusic.isPlaying) {
			bgMusic.time=time;
			bgMusic.Play();
		}
	}

	public void StopBgMusic(){
		if (bgMusic.isPlaying) {
			time=bgMusic.time;
			bgMusic.Stop();
		}
	}

	public void PlayClick(){
		Debug.Log ("PlayClick");

		//joue un click si les FX sont on
		if (GameController.instance.isFXOn) {
			click.Play ();
		}
	}

	public void GameIsLoadedTurnOffMusic(){
		if (bgMusic.isPlaying) {
			time=bgMusic.time;
			bgMusic.Stop();
		}
	}

}
