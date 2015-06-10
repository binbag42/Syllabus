using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Display voyelles. Add a listener to the OnEndDrag events for voyelles, print the letter in the text component
/// </summary>

public class DisplayVoyelles : MonoBehaviour {


	static public DisplayVoyelles displayVoyelles;
	
	
	void Awake(){
		displayVoyelles = this;
	}


		// Use this for initialization
		void Start () {
			//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
			RotateVoyelles.OnPickedDial += HandleOnEndDrag;
			
		}
		
		
		void HandleOnEndDrag (int dial, string name, int dialName)
		{
		GetComponent<Text> ().text = name;
		}
	}