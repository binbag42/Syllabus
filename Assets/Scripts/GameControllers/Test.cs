using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class Test : MonoBehaviour {

	// var permettant de représenter les joueurs
	public PlayerLevelT[] players;
	private PlayerLevelT p;
	private int l;
	private CarteT c;


	// Use this for initialization
	void Start () {
		InitialiseGameVariables ();
	}


	void InitialiseGameVariables(){
//		players = InitializeArray<PlayerLevelT>(6);
	 players = new PlayerLevelT[6].Select(h => new PlayerLevelT()).ToArray();

		//itération pour chaque joueur
		for (int i=0; i<6; i++) {


			p=players[i];



			//itération pour chaque niveau
			for (int j=0; j<3; j++) {

				l = p.levels;
				Debug.Log ("j= " + j+"l= "+l);

//				for (int k=0; k<10; k++) {
////					//init carte
////					c = l.cards [k];
//					c=new CarteT();
//					Debug.Log ("carte k= " + k + " EF= " + c.EF);
//				}
			}
		}
	}


	//classe level= liste de Carte
	[Serializable]
	public class LevelT{
		public CarteT[] cards;
		//default constructor
		public LevelT(){
			cards = new CarteT[GameController.numberMaxCartesPerLevel];
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
	public class PlayerLevelT{
		public int levels;
		//default constructor
		public PlayerLevelT(){
//			levels=new int[GameController.numberLevel];
			levels=1;
		}
//		accessor via indexer
//		public int this[int index]{
//				get
//				{
//					return levels[index];
//				}
//				set
//				{
//					levels[index] = value;
//				}
//			
//			}
	
	
		T[] InitializeArray<T>(int length) where T : new()
		{
			T[] array = new T[length];
			for (int i = 0; i < length; ++i)
			{
				array[i] = new T();
			}
			
			return array;
		}
	


	}
	
	//Classe Carte contient le triplet Interval, Repetition, EF
	[Serializable]
	public class CarteT{
		public int Interval { get; set; }
		public int Repetition { get; set; }
		public float EF { get; set; }
		
		public CarteT() { this.Interval = 1;
			this.Repetition = 0;
			this.EF = 2.5f;}
		
		public CarteT(int interval, int repetition, int ef)
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

}
