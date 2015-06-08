using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CallMusicController : MonoBehaviour {

	public void CallClickPlay(){
		MusicController.instance.PlayClick ();
	}
}
