using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Display. Add a listener to the OnEndDrag events, print the corresponding letter in the "proper" text component ie voyelles or consonnes
/// </summary>

public class Display3 : MonoBehaviour {

	static public Display3 display;

	private string firstletter, secondletter, word;

	// Use this for initialization
	void Awake () {
		display = this;
	}

	void Start () {
		//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
		Dial.OnPickedDial += HandleOnEndDrag;
		StartLevel ();
	}
	
	
	void HandleOnEndDrag (int numberOfStepsDone, string name, int dialName){
		switch (dialName){
		case 0:
			// event sent from a consonne/leftmost dial
			firstletter=name;
			break;
		case 1:
			// event sent from a voyelle/rightmost dial
			secondletter=name;
			break;
		}
		word = firstletter + secondletter;
		GetComponent<Text> ().text = word;
	}

	void StartLevel(){
		// affiche image


		// affiche tail



	}

	// 


}
