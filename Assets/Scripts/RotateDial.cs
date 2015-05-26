using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RotateDial: MonoBehaviour {

	private Quaternion originalRotation;
	private float startAngle = 0f;
	private int numberOfSteps;

	private string[] Consonnes= { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v"};

	void Awake()
	{
		numberOfSteps = Consonnes.Length;
//		Vector3 centerParent = this.GetComponent<RectTransform>().position;
//
//		Debug.Log ("position parent x " + centerParent.x + "y " + centerParent.y);
//
		for (int i=0; i<Consonnes.Length; i++) {
			GameObject clone;
			clone= (GameObject) Instantiate(Resources.Load("TextDial_"));
			RectTransform cloneRT = clone.GetComponent<RectTransform>();
			clone.name="Dial_"+i;
			clone.transform.SetParent(this.transform, false);
			clone.GetComponent<Text>().text=(string) Consonnes[i];

//			cloneRT.localPosition= new Vector3(0,0,0);
//			Vector3 rotationCenter = new Vector3(0,0,0);
			cloneRT.RotateAround(Vector3.zero, Vector3.forward, i * 360/Consonnes.Length);

		}
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
		startAngle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
	}

	public void Dragging(){

		Vector2 screenPos = transform.position;
#if UNITY_IPHONE || UNITY_ANDROID		
		Vector2 vector = Input.GetTouch(0).position - screenPos;
#else
		Vector2 vector = (Vector2) Input.mousePosition - screenPos;
#endif
		float angle = Mathf.Atan2 (vector.x, vector.y) * Mathf.Rad2Deg;
		Quaternion newRotation = Quaternion.AngleAxis(angle - startAngle , -transform.forward);
		newRotation.y = 0;
		newRotation.eulerAngles = new Vector3(0,0,newRotation.eulerAngles.z);
		transform.rotation = originalRotation *  newRotation;
	}


//	int newAngle;
//	int numberOfStepsDone;
//	Vector3 screenPos = transform.position;
//	Vector3 vector = Input.mousePosition - screenPos;
//	int angle = (int) Mathf.Ceil(Mathf.Atan2 (vector.x, vector.y) * Mathf.Rad2Deg);
//	newAngle = (angle - startAngle);
//	numberOfStepsDone= newAngle/angleStep;
//	Quaternion newRotation = Quaternion.AngleAxis(numberOfStepsDone*angleStep , -transform.forward);
//	newRotation.y = 0;
//	newRotation.eulerAngles = new Vector3(0,0,newRotation.eulerAngles.z);
//	transform.rotation = originalRotation *  newRotation;


	//nomenclature des listeners, pourrait avoir plusieurs paramètres
	public delegate void PickedDial(int pickedDial, string nameOfPickedDial, int nameDial);
	//static donc peut être adressé par tout le monde
	public static event PickedDial OnPickedDial;

	public void EndDrag(){
		// send an event with the final dial
		int angleStep = 360 / numberOfSteps;
		Debug.Log ("angleStep " + angleStep);
		int numberOfStepsDone;
		numberOfStepsDone= ((int) (360-transform.eulerAngles.z)/angleStep)%numberOfSteps; 
		Debug.Log ("transform.eulerAngles.z " + transform.eulerAngles.z);
		Debug.Log ("numberofStepsDone : " + numberOfStepsDone);
		OnPickedDial (numberOfStepsDone, Consonnes[numberOfStepsDone], 0 );

	}


}
