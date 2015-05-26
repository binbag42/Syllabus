using UnityEngine;
using System.Collections;

public class Dial : MonoBehaviour {

	private static Dial myInstance = null;

	public static Dial Instance {
		get {
			if (myInstance == null) {
				myInstance=GameObject.FindObjectOfType(typeof(Dial)) as Dial;
				if (myInstance == null) {
					myInstance = new GameObject("Dial").AddComponent<Dial>();
				}
			}
			return myInstance;
		}
	}

	
	void Awake () {
		if (myInstance == null) {
			myInstance=Dial.Instance;
		}
	}
}