using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Display. Add a listener to the OnEndDrag events, print the corresponding letter in the "proper" text component ie voyelles or consonnes
/// </summary>

public class Display3 : MonoBehaviour {

	static public Display3 display;

	private string firstletter, secondletter, word;
	private string wordToGuess="word";
	private string tail;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
		tail = "tail";
	}

	void Start () {
		//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
		Dial.OnPickedDial += HandleOnEndDrag;

	}

	void MakeSingleton(){
		if (display != null) {
			//permet de détruire le gameObject si celui-ci est créé alors que l'on revient sur la sc1ene initiale où Unity recrée les différents GameObject de la hierarchy
			Destroy (gameObject);
		} else {
			display = this;
		}
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
		word = firstletter + secondletter+tail;
		GetComponent<Text> ().text = word;

	}

	public void DisplayClue(Cards cardToGuess ){
		wordToGuess = cardToGuess.Name;
		tail = cardToGuess.Tail;
		// affiche image


		// affiche tail
		GetComponent<Text> ().text = tail;

	}



	// 


}
