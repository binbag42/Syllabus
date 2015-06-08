using UnityEngine;
using System.Collections;

public class LevelController:MonoBehaviour {

	public void GoToMainMenu(){
		MusicController.instance.PlayClick ();
		Application.LoadLevel ("MainMenu");
	}
}
