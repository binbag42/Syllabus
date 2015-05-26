using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayLetterDialStep : MonoBehaviour {


	static public DisplayLetterDialStep displayVoyelle;
	
	
	void Awake(){
		displayVoyelle = this;
	}


		// Use this for initialization
		void Start () {
			//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
			RotateDialStep.OnPickedDial += HandleOnEndDrag;
			
		}
		
		
		void HandleOnEndDrag (int dial, string name, int dialName)
		{
		GetComponent<Text> ().text = name;
		}
	}