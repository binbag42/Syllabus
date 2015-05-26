using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RotateTween : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DORotate (Vector3.right,0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
