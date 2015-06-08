﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class DisplayLetter : MonoBehaviour {
	
	static public DisplayLetter displayConsonne;


	void Awake(){
		displayConsonne = this;
	}
		// Use this for initialization
		void Start () {
			//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
			RotateDial.OnPickedDial += HandleOnEndDrag;
		}
		
		
		void HandleOnEndDrag (int dial, string name, int nameDial)
		{
		GetComponent<Text> ().text = name;
		}
	}