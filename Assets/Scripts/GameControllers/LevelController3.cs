using UnityEngine;
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
	public string Name { get; set; }
	public string Start { get; set; }
	public string Tail { get; set; }
	public string Link { get; set; }

	public Cards() { }
	
	public Cards(string name, string start, string tail, string link) 
	{ 
		this.Name = name; 
		this.Start = start; 
		this.Tail = tail;
		this.Link=link; 
	}
}

public class LevelController3:MonoBehaviour {
	// pointeur sur le fichier XML des cartes à charger
	public TextAsset ConfigFile;
	private List<Cards> cardsInDeck=new List<Cards>();

	private string firstletter, secondletter, word;
	private string wordToGuess;
	private string currentTail;

	void Awake(){
		//charge le fichier les cartes du fichier XML ConfigFile dans la liste de cards cardsInDeck
		LoadConfigFile ();

	}

	void Start(){
		//lance le level
		startLevel ();
		//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
		Dial.OnPickedDial += HandleOnEndDrag;
	}
	
	private void startLevel(){
		int i = 1;
		wordToGuess = cardsInDeck [i].Name;
		currentTail = cardsInDeck [i].Tail;
		Display3.display.DisplayClue (cardsInDeck[i]);
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
		}
	}

	// XML parsing to populate cardsInDeck
	private void LoadConfigFile(){

		// Load and parse the XML file
		XmlDocument xmlData = new XmlDocument ();
		xmlData.LoadXml (ConfigFile.text);
		
		// Get the list of cards nodes
		XmlNodeList cardsList = xmlData.GetElementsByTagName ("card");

		int j=0;
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