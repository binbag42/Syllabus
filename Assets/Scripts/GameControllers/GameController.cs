using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour {

	public static GameController instance;

	//classe GameData définie ci-dessous
	private GameData data;

	public int numberMaxPlayer=6;

	public int currentLevel;
	public int currentScore;

	public bool firstStart;
	public bool isGameStartedFirstTime;
	public bool isMusicOn;
	public bool isFXOn=true;
	
	public int selectedPlayer;
	public int highScore;
	
	public bool[] players;
	public string[] playersName;
	public bool[] levels;
	public bool[] achievements;

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

		if (isGameStartedFirstTime) {
			//game is started for the first time
			currentLevel = 0;
			currentScore = 0;

			isGameStartedFirstTime = false;
			isMusicOn = false;

			selectedPlayer = 0;
			highScore = 0;

			players = new bool[numberMaxPlayer];

			playersName = new string[numberMaxPlayer];
			levels = new bool[10];
			achievements = new bool[10];
			 
			players [0] = true;
			for (int i=1; i<players.Length; i++) {
				players [i] = false;
			}
			
			for (int i=0; i<playersName.Length; i++){
				playersName[i]="Player "+i;
			}

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

[Serializable]
class GameData{
	private bool isGameStartedFirstTime;
	private bool isMusicOn;

	private int selectedPlayer;
	private int highScore;

	private bool[] players;
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


	public bool[] Players
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