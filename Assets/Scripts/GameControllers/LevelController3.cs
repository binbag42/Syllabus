using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

// level logics for game level3
// level3= present of a card with the tail, have to guess the first syllable

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



	public TextAsset ConfigFile;
	private Cards[] cardsInDeck=new Cards[4];

	void Awake(){
		LoadConfigFile ();
		for (int i=0; i<3; i++){
			Debug.Log (cardsInDeck[i].Name);
		}
	}


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
			cardsInDeck[j++] = newCard;
		}	
	}
}