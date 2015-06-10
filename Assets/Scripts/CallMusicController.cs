using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// used for UI elements to call methods of the MusicController static instance
// in the Inspector

public class CallMusicController : MonoBehaviour {

	public void CallClickPlay(){
		MusicController.instance.PlayClick ();
	}
}
