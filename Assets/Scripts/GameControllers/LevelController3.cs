﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// level logics for game level3
// level3= present  a card with a tail, have to guess the first syllable

// classe pour les objets récupérés du fichier xml cards3
public class Cards
{
	//  Mot à deviner
	public string Name { get; set; }
	// premiere syllabe de Name
	public string Start { get; set; }
	// tout sauf première syllabe de Name
	public string Tail { get; set; }
	// lien vers image
	public string Link { get; set; }

	public Cards() { }

	//constructeur avec 4 parametres
	public Cards(string name, string start, string tail, string link) 
	{ 
		this.Name = name; 
		this.Start = start; 
		this.Tail = tail;
		this.Link=link; 
	}
}

public class LevelController3:MonoBehaviour {
	static public LevelController3 levelCtrl;
	
	// pointeur sur le fichier XML des cartes à charger
	public TextAsset ConfigFile;
	// list des cartes extraites du fichier XML ConfigFile
	private List<Cards> cardsInDeck=new List<Cards>();
	// première et seconde lettre des dials, word= firstletter+secondletter+currentTail
	private string firstletter, secondletter, word;
	// mot en cours à deviner
	private string wordToGuess;
	// fin du mot qui est en cours d'etre deviné
	private string currentTail;
	//positionné à TRUE par LevelController HandleOnEndDrag listener si gagné
	public bool flag { get; set; }

	void Awake(){
		//charge les cartes du fichier XML ConfigFile dans la liste de cards cardsInDeck
		LoadConfigFile ();
		flag = false;
		levelCtrl = this;
	}

	void Start(){
		//lance le level
		startLevel ();
		//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
		Dial.OnPickedDial += HandleOnEndDrag;
		OnWordGuessed += HandleOnWordGuessed;
	}


	void OnDestroy() {
		// Unsubscribe, so this object can be collected by the garbage collection
		Dial.OnPickedDial -= HandleOnEndDrag;
		OnWordGuessed -= HandleOnWordGuessed;
	}
	
	private void startLevel(){
		int currentProfile;
		//récupère les anciens settings
		GameController.instance.Load ();
		// populate le numéro du joueur en cours
		currentProfile = GameController.instance.selectedPlayer;
		AfficheCarte ();
	}

	public void AfficheCarte(){
		// reset le dial 
		firstletter = " ";
		secondletter = " ";
		word = "";
		// prend une carte au hasard
		int i;
		i = UnityEngine.Random.Range(0, GameController.numberMaxCartesPerLevel);
		// vérifie si la carte n'est pas null
		if(cardsInDeck[i].Name!=null){
			wordToGuess = cardsInDeck [i].Name;
			currentTail = cardsInDeck [i].Tail;
			Display3.display.DisplayClue (cardsInDeck[i]);
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
		word = firstletter + secondletter+currentTail;
		// vérifie si gagné et si oui envoie un event
		if (word.ToUpper() == wordToGuess.ToUpper()) {
			Debug.Log ("gagné ");
			flag=true;
			OnWordGuessed();
		}
	}

	// listener de l'event OnWordGuessed
	void HandleOnWordGuessed(){
		//update the player data
		Debug.Log ("OnWordGuessed2");

	}

	//nomenclature des listeners avec plusieurs paramètres
	public delegate void WordGuessed();
	//static donc peut être adressé par tout le monde
	public static event WordGuessed OnWordGuessed;


	// XML parsing to populate cardsInDeck
	private void LoadConfigFile(){

		// Load and parse the XML file
		XmlDocument xmlData = new XmlDocument ();
		xmlData.LoadXml (ConfigFile.text);
		
		// Get the list of cards nodes
		XmlNodeList cardsList = xmlData.GetElementsByTagName ("card");

		foreach (XmlNode cardInfo in cardsList) {
			XmlNodeList cardcontent = cardInfo.ChildNodes;
			Cards newCard = new Cards();

			foreach (XmlNode cardsItems in cardcontent) { // levels itens nodes.
				if(cardsItems.Name == "object"){
					switch (cardsItems.Attributes ["name"].Value) {
						case "Name":
							newCard.Name = cardsItems.InnerText;
							break; // put this in the dictionary.
						case "Start":
							newCard.Start = cardsItems.InnerText;
							break; // put this in the dictionary.
						case "Tail":
							newCard.Tail = cardsItems.InnerText;
							break; // put this in the dictionary.
						case "Link":
							newCard.Link = cardsItems.InnerText;
							break; // put this in the dictionary.
					}
				}
			}	
			cardsInDeck.Add(newCard);
		}	
	}
}