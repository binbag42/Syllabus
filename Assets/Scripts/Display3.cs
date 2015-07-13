using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Display. Add a listener to the OnEndDrag events, print the corresponding letter in the "proper" text component ie voyelles or consonnes
/// </summary>

public class Display3 : MonoBehaviour {

	static public Display3 display;
	// éléments du canvas
	public GameObject imageClue;
	public GameObject feuArtifice;
	private GameObject[] elementsToHide;

	private string firstletter, secondletter, word;
	private string wordToGuess="word";
	private string tail;

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
		elementsToHide=GameObject.FindGameObjectsWithTag ("toHide");
		tail = "tail";
	}

	void Start () {
		//enregistre l'écouteur HandleOnEndDrag, à l'évènement OnPickedDial, qui est de même caractérsitique que la définition du delegate
		Dial.OnPickedDial += HandleOnEndDrag;
		LevelController3.OnWordGuessed += HandleOnWordGuessed;

	}

	void OnDestroy() {
		// Unsubscribe, so this object can be collected by the garbage collection
		Dial.OnPickedDial -= HandleOnEndDrag;
		LevelController3.OnWordGuessed -= HandleOnWordGuessed;
	}

	void MakeSingleton(){
		if (display != null) {
			//permet de détruire le gameObject si celui-ci est créé alors que l'on revient sur la sc1ene initiale où Unity recrée les différents GameObject de la hierarchy
			Destroy (gameObject);
		} else {
			display = this;
		}
	} 

	void HandleOnWordGuessed(){
		// animation du display
		Debug.Log ("OnWordGuessed1");
		//suppression du display et reset
		firstletter = " ";
		secondletter = " ";
		GetComponent<Text> ().text = "";
		//déactive des éléments du GUI
		imageClue.SetActive (false);
		foreach(GameObject item in elementsToHide)
		{
			item.SetActive(false);
		}
		// animation pour faire jolie
		feuArtifice.transform.position = Vector3.zero;
		feuArtifice.SetActive (true);
	}

	public void HandleOnFireworkDrag(){
		Debug.Log ("HandleOnFireworkDrag");
		#if UNITY_IPHONE || UNITY_ANDROID
		feuArtifice.transform.position= Input.GetTouch (0).position;
		#else	
			Vector3 vector = Input.mousePosition;
			feuArtifice.transform.position=new Vector2(vector.x, vector.y);
		#endif
	}

	public void HandleOnClickMenu(string menuName){
		Application.LoadLevel (menuName);
	}

	public void HandleOnClickReload(){
		//suppression animation pour faire jolie si nécessaire
		feuArtifice.SetActive (false);
		// affichage des éléemts du GUI
		imageClue.SetActive (true);
		foreach(GameObject item in elementsToHide)
		{
			item.SetActive(true);
		}
		// affiche nouvelle carte
		LevelController3.levelCtrl.AfficheCarte ();
	}

	void HandleOnEndDrag (int numberOfStepsDone, string name, int dialName){

		if (!LevelController3.levelCtrl.flag) {
			switch (dialName) {
			case 0:
			// event sent from a consonne/leftmost dial
				firstletter = name;
				break;
			case 1:
			// event sent from a voyelle/rightmost dial
				secondletter = name;
				break;
			}
			Debug.Log ("OnEndDrag");
			word = firstletter + secondletter + tail;
			GetComponent<Text> ().text = word;
		} else {
			LevelController3.levelCtrl.flag=false;
		}
	}

	public void DisplayClue(Cards cardToGuess ){
		wordToGuess = cardToGuess.Name;
		tail = cardToGuess.Tail;
		// affiche image
		imageClue.GetComponent<Image>().sprite=Resources.Load<Sprite>("CardsImage/"+wordToGuess);
		imageClue.SetActive (true);
		// affiche tail
		GetComponent<Text> ().text = tail;

	}



	// 


}
