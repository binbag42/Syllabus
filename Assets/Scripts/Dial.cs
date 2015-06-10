using UnityEngine;
using System.Collections;
using System;

public class Dial : MonoBehaviour {
	private Quaternion originalRotation;
	// variable pour suivre la rotation lors d'un drag
	private float startAngle = 0;
	// variable contenant le nombre de steps du dial
	private int numberOfSteps;
	// identify the dial: 0 for the first dial, 1 for the next one and so on
	public int nameDial;

	private int angleStep;
	private int numberOfStepsDone;

	public string[] letters;

	void Start(){
		numberOfSteps = letters.Length;
		angleStep = 360 / numberOfSteps;
		Debug.Log ("numberofSteps " + numberOfSteps);
		Debug.Log ("angleStep " + angleStep);

	}
	
	
	public void StartDrag(){
		
		// récupère les éléments au début du drag.
		originalRotation = transform.rotation;
		//	transform.position=The position of the transform in world space.
		//	Attention le transform est en fait un RectTransform semble renvoyer directement une info locale donc pas besoin de passer par Camera.main.WorldToScreen

		Vector3 screenPos = transform.position;
		#if UNITY_IPHONE || UNITY_ANDROID		
		Vector3 vector = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0)-screenPos;
		#else
		Vector3 vector = Input.mousePosition - screenPos;
		#endif
		// angle between the x-axis and a 2D vector starting at zero and terminating at (vector.x,vector.y).
		startAngle = (int) Mathf.Ceil(Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg);
	}
		
		public void Dragging(){
			#if UNITY_IPHONE || UNITY_ANDROID
			
			float newAngle;
			Vector2 screenPos = new Vector2 (transform.position.x, transform.position.y);
			Vector2 vector = Input.GetTouch(0).position - screenPos;
			float angle = Mathf.Ceil(Mathf.Atan2 (vector.x, vector.y) * Mathf.Rad2Deg);
			newAngle = (angle - startAngle);
			Quaternion newRotation = Quaternion.AngleAxis(newAngle, -transform.forward);
			newRotation.y = 0;
			newRotation.eulerAngles = new Vector3(0,0,newRotation.eulerAngles.z);
			transform.rotation = originalRotation *  newRotation;
			
			#else
			
			float newAngle;
			Vector3 screenPos = transform.position;
			Vector3 vector = Input.mousePosition - screenPos;
			float angle = Mathf.Ceil(Mathf.Atan2 (vector.x, vector.y) * Mathf.Rad2Deg);
			newAngle = (angle - startAngle);
			Quaternion newRotation = Quaternion.AngleAxis(newAngle , -transform.forward);
			newRotation.y = 0;
			newRotation.eulerAngles = new Vector3(0,0,newRotation.eulerAngles.z);
			transform.rotation = originalRotation *  newRotation;
			
			#endif
		}
	
	
	//nomenclature des listeners avec plusieurs paramètres
	public delegate void PickedDial(int pickedDial, string nameOfPickedDial, int nameDial);
	//static donc peut être adressé par tout le monde
	public static event PickedDial OnPickedDial;
	
	public void EndDrag(int nameDial){
		// send an event with the final dial
		numberOfStepsDone= ((int) (360-transform.eulerAngles.z)/angleStep)%numberOfSteps;
		OnPickedDial (numberOfStepsDone, letters[numberOfStepsDone], nameDial );
	}
}