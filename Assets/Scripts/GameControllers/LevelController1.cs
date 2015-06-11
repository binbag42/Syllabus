using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// menu controller of a game level + initialise variables/objects of the level

public class LevelController1:MonoBehaviour {
	
	public string[] Consonnes= { "B", "C", "D", "F", "G", "J", "L", "M", "N", "P", "R", "S", "T", "V"};
	public string[] Voyelles= {"A", "OU", "È", "É", "U", "O", "I", "E"};

	void Awake(){

		GameObject.Find ("ConsonnesDial").GetComponent <Dial> ().letters=Consonnes;
		GameObject.Find ("ConsonnesDial").GetComponent <Dial> ().nameDial=0;
		GameObject.Find ("VoyellesDial").GetComponent <Dial> ().letters=Voyelles;
		GameObject.Find ("VoyellesDial").GetComponent <Dial> ().nameDial=1;

	}

	public void GoToMainMenu(){
		MusicController.instance.PlayClick ();
		Application.LoadLevel ("MainMenu");
	}
	
}
