using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class GamePlayController : MonoBehaviour {

	public static GamePlayController instance;

	private void CreateInstance(){
		if (instance == null) {
			instance = this;
		}

	}

	// Use this for initialization
	void Awake () {
		CreateInstance ();
	}
	

	void InitializeGamePlayController(){

	}
}
