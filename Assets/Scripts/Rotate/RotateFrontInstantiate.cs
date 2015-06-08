using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateFrontInstantiate : MonoBehaviour {

	private int frontLetter = 0;
	private GameObject[] alphabet;
	private int step = 5;
	private bool isTurning = false;
	private string[] Consonnes= { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v"};

	// Use this for initialization
	void Awake() {
		for (int i=0; i<Consonnes.Length; i++) {
			GameObject clone;
			clone= (GameObject) Instantiate(Resources.Load("Letter_"));
			clone.name="Letter_"+i;
			clone.GetComponentInChildren<TextMesh>().text=(string) Consonnes[i];
		}

				alphabet = GameObject.FindGameObjectsWithTag ("Letter");
	}

	void Start () {
//		for (int i=0; i<alphabet.Length; i++) {
//			alphabet [i] = GameObject.Find ("Letter_"+i);
//		}
//		
		MoveLetterForward (0);

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !isTurning){
			isTurning = true;
			StartCoroutine(RotateFrontLetter ());
		}
	}

	IEnumerator RotateFrontLetter(){
		if (frontLetter== alphabet.Length) frontLetter=0;
		MoveNext();
		for (int angle=0; angle<(180/step); angle++) {
			alphabet[frontLetter].transform.Rotate (step, 0, 0);
			yield return 0;
		}
		//Letter is upside down and forward is local to the letter so it is backward in global
		MoveLetterForward(frontLetter);

		for (int angle=180/step; angle<(360/step); angle++) {
			alphabet[frontLetter].transform.Rotate (step, 0, 0);
			yield return 0;
		}
		frontLetter++;
		isTurning = false;
		yield return 0;
	}

	private void MoveNext(){
		int nextLetter = frontLetter+1;
		if (nextLetter >= alphabet.Length) {
			nextLetter = 0;
		}
		MoveLetterForward (nextLetter);

	}

	private void MoveLetterForward(int index){
		// alternatives identiques avec résultat identique
		//		alphabet [index].transform.position -= Vector3.forward;
		//		alphabet [index].transform.position = new Vector3 (0,0,-1);

		alphabet[index].transform.Translate(-Vector3.forward);

	}

}
