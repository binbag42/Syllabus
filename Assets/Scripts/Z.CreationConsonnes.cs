using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreationConsonnes : MonoBehaviour {

	private string[] Consonnes= { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v"};
	
	void Awake()
	{


		// création des consonnes sur le dial
		int numberOfSteps = Consonnes.Length;
		int angleStep = 360 / numberOfSteps;
		// iteration pour tous les elements du array consonnes
		for (int i=0; i<Consonnes.Length; i++) {
			GameObject clone;
			clone= (GameObject) Instantiate(Resources.Load("TextDial_"));
			RectTransform cloneRT = clone.GetComponent<RectTransform>();
			clone.name="Dial_"+i;
			clone.transform.SetParent(this.transform, false);
			clone.GetComponent<Text>().text=(string) Consonnes[i];
			cloneRT.RotateAround(Vector3.zero, Vector3.forward, i * 360/Consonnes.Length);
			cloneRT.transform.localPosition= new Vector3(0,0,0);
			
		}
}
}