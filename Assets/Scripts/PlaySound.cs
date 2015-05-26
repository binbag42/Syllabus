using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Play sound. As soon as a voyel and consonne have been picked, play the sound of each letter then of the syllable.
/// </summary>

public class PlaySound : MonoBehaviour {

	private string nameConsonne="";
	private string nameVoyel="";
	
	// Use this for initialization
	void Start () {
		//enregistre l'écouteur HandleOnEndDrag, pour les 2 évenements OnPickedDial, qui est de même caractérsitique que la définition du delegate
		RotateDial.OnPickedDial += HandleOnEndDrag;
		RotateDialStep.OnPickedDial += HandleOnEndDrag;
	
	}
	
	//dialRef=0 si consonne, 1 si voyelle
	void HandleOnEndDrag (int dial, string name, int dialRef)
	{
//		Debug.Log ("HandleOnEndDrag : " + dial + " name " + name + " dialRef " + dialRef);
		if (dialRef==1) {
			nameVoyel = name;
		} else {
			nameConsonne = name;
		}
		if (nameConsonne != "" && nameVoyel != "") {

//			DisplayLetterDialStep.displayVoyelle.GetComponent<Text>().color=Color.white;

			//sound syllabe
			PlayWords(nameConsonne+nameVoyel);
//			PhaaxTTS.Say ("fr", nameConsonne+nameVoyel, 1.0f, 1.0f);
			

		}
	}

	void PlayWords(string words){
		//wait until nothing is playing anymore
		while (PhaaxTTS.IsPlaying ()) {
		}
		PhaaxTTS.Say ("fr", words, 1.0f, 1.0f);
		}
}


