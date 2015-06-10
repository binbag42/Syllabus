using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Display consonnes. Add a listener to the OnEndDrag events for consonnes, print the letter in the text component
/// </summary>

public class DisplayConsonnes : MonoBehaviour {
	
	static public DisplayConsonnes displayConsonnes;


	void Awake(){
		displayConsonnes = this;
	}
		// Use this for initialization
		void Start () {
			//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
			RotateConsonnes.OnPickedDial += HandleOnEndDrag;
		}
		
		
		void HandleOnEndDrag (int dial, string name, int nameDial)
		{
		GetComponent<Text> ().text = name;
		}
	}