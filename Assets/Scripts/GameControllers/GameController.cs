using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Game controller. Variable initialisation and Load/Save player pref logics.
/// </summary>
/// 
[Serializable]
public class GameController : MonoBehaviour {

	public static GameController instance;

	//classe GameData définie ci-dessous, contient les données qui sont lues/sauvegardées dans le fichier
	private GameData data;
	// quantitié d'information à trouver dans le fichier GameData suivant nombre de joueurs possibles
	public static int numberMaxPlayer=6;
	// nombre de level dans le jeu => nb de level stockés dans les données joueurs
	public static int numberLevel=3;
	// nombre maximum de cartes dans un niveau
	public static int numberMaxCartesPerLevel=10;
	// différentes variables lues du fichier GameData
	public int currentLevel;
	public int currentScore;

	public bool firstStart;
	public bool isGameStartedFirstTime;
	public bool isMusicOn;
	public bool isFXOn=true;
	
	public int selectedPlayer;
	public int highScore;
	
//	public bool[] players;

	// noms des jouers
	public string[] playersName;
	public bool[] levels;
	public bool[] achievements;

	// var permettant de représenter les joueurs
	public PlayerLevel[] players;
	private PlayerLevel p;
	private Level l;
	private Carte c;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
		InitialiseGameVariables ();
	}

	void MakeSingleton(){
		if (instance != null) {
			//permet de détruire le gameObject si celui-ci est créé alors que l'on revient sur la sc1ene initiale où Unity recrée les différents GameObject de la hierarchy
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	} 


	void InitialiseGameVariables(){
		Load ();

		if (data != null) {
			isGameStartedFirstTime = data.getIsGameStartedFirstTime();
			firstStart=false;
		} else {
			isGameStartedFirstTime=true;
			firstStart=true;
		}

		// si la structure du fichier est modifiée (ajout/supression de champs...)
		// enlever ce test pour modifier la structure du fichier en l'écrasant comme si c'était un premier lancement

		if (isGameStartedFirstTime) {

		//game is started for the first time
			currentLevel = 0;
			currentScore = 0;

			isGameStartedFirstTime = false;
			isMusicOn = false;

			selectedPlayer = 0;
			highScore = 0;

			playersName = new string[numberMaxPlayer];
			levels = new bool[10];
			achievements = new bool[10];

			players=new PlayerLevel[GameController.numberMaxPlayer];
			//itération pour chaque joueur
			for (int i=0; i<GameController.numberMaxPlayer; i++){
				p=players[i];
				//itération pour chaque niveau
				for (int j=0; j<GameController.numberLevel; j++){
					Debug.Log ("j= "+j);
					l=p.levels[1];
					for (int k=0; k<GameController.numberMaxCartesPerLevel; k++){
						//init carte
						c=l.cards[k];
						Debug.Log("carte k= "+k+" EF= "+c.EF);
					}
				}
			}

			// pré format les noms des joueurs en tant que PlayerX, X étant un int
			for (int i=0; i<playersName.Length; i++){
				playersName[i]="Player "+i;
			}

			// 
			levels [0] = true;
			for (int i=1; i<levels.Length; i++) {
				levels [i] = false;
			}

			for (int i=0; i<achievements.Length; i++) {
				achievements [i] = false;
			}


			data = new GameData ();

			data.setIsGameStartedFirstTime(isGameStartedFirstTime);
			data.IsMusicOn=isMusicOn;
			data.SelectedPlayer=selectedPlayer;
			data.HighScore=highScore;
			data.Players=players;
			data.PlayersName=playersName;
			data.Levels=levels;
			data.Achievements=achievements;

			Save ();
			Load ();

		} else {
			// game has already been played so load game data
			isGameStartedFirstTime = data.getIsGameStartedFirstTime();
			isMusicOn = data.IsMusicOn;
			
			selectedPlayer = data.SelectedPlayer;
			highScore = data.HighScore;
			
			players = data.Players;
			playersName = data.PlayersName;
			levels = data.Levels;
			achievements = data.Achievements;

		}
	}


	public void Save(){

		FileStream file = null;

		try {
			BinaryFormatter bf = new BinaryFormatter();

			file = File.Create(Application.persistentDataPath + "/GameData.dat");

			if (data != null) {
				data.setIsGameStartedFirstTime(isGameStartedFirstTime);
				data.IsMusicOn=isMusicOn;
				data.SelectedPlayer=selectedPlayer;
				data.HighScore=highScore;
				data.Players=players;
				data.PlayersName=playersName;
				data.Levels=levels;
				data.Achievements=achievements;

				bf.Serialize(file, data);
			}

		} catch (Exception e){


		} finally {
			if (file!=null) {file.Close();}
		}

	}

	public void Load(){

		FileStream file = null;
		// vérifie si le fichier GameData.dat existe
		try{

			BinaryFormatter bf = new BinaryFormatter();

			file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

			data = (GameData) bf.Deserialize(file);
		}
		catch (Exception e){
		}
		finally{
			if (file!=null) {file.Close();}
		}


	}

}//GameController


//classe level= liste de Carte
[Serializable]
public class Level{
	public Carte[] cards;
	//default constructor
	public Level(){
		cards = new Carte[GameController.numberMaxCartesPerLevel];
	}
}

//public class Level{
//	public List<Carte> cards;
//	//default constructor
//	public Level(){
//		cards=new List<Carte>();
//		cards.Add(new Carte ());
//	}
//
//	public Level(int i){
//		cards=new List<Carte>();
//		for (int j=0; j<i; j++) {
//			cards.Add (new Carte ());
//		}
//	}
//
//
//}

//classe player = level[]
[Serializable]
public class PlayerLevel{
	public Level[] levels;
	//default constructor
	public PlayerLevel(){
		levels=new Level[GameController.numberLevel];
	}
	//accessor via indexer
//	public Level this[int index]{
//		get
//		{
//			return levels[index];
//		}
//		set
//		{
//			levels[index] = value;
//		}
//	
//	}

}

//Classe Carte contient le triplet Interval, Repetition, EF
[Serializable]
public class Carte{
	public int Interval { get; set; }
	public int Repetition { get; set; }
	public float EF { get; set; }

	public Carte() { this.Interval = 1;
		this.Repetition = 0;
		this.EF = 2.5f;}
	
	public Carte(int interval, int repetition, int ef)
	{ 
		this.Interval = interval;
		this.Repetition = repetition;
		if (ef < 1.3f) {
			this.EF = 1.3f;
		} else {
			this.EF = ef;
		}
	}
}


// Classe GameData permet de lire/écrire toutes les variables qui sont/seront sauvées dans le "playerpref"
[Serializable]
class GameData{
	private bool isGameStartedFirstTime;
	private bool isMusicOn;

	private int selectedPlayer;
	private int highScore;

	private PlayerLevel[] players;
	private string[] playersName;
	private bool[] levels;
	private bool[] achievements;

	//getter d'après la méthode du cours Make mobile Games and Earn Money
	public void setIsGameStartedFirstTime (bool isGameStartedFirstTime){
		this.isGameStartedFirstTime = isGameStartedFirstTime;
	}

	
	//setter d'après la méthode du cours Make mobile Games and Earn Money
	public bool getIsGameStartedFirstTime(){
		return this.isGameStartedFirstTime;
	}

	//getter and setter from method in my notes, uses Property. It means using capitalised word to access the private variables eg IsMusicOn for variable isMusicOn
	public bool IsMusicOn
	{
		get
		{
			return isMusicOn;
		}
		set
		{
			isMusicOn = value;
		}
	}

	public int SelectedPlayer
	{
		get
		{
			return selectedPlayer;
		}
		set
		{
			selectedPlayer = value;
		}
	}

	public int HighScore
	{
		get
		{
			return highScore;
		}
		set
		{
			highScore = value;
		}
	}
	

	public PlayerLevel[] Players
	{
		get
		{
			return players;
		}
		set
		{
			players = value;
		}
	}

	public string[] PlayersName 
	{
		get
		{
			return playersName;
		}
		set
		{
			playersName = value;
		}
	}


	public bool[] Levels
	{
		get
		{
			return levels;
		}
		set
		{
			levels = value;
		}
	}

	public bool[] Achievements
	{
		get
		{
			return achievements;
		}
		set
		{
			achievements = value;
		}
	}
}