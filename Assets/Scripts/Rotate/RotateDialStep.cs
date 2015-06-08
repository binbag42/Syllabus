using UnityEngine;
using System.Collections;
using System;

public class RotateDialStep : MonoBehaviour {

	private Quaternion originalRotation;
	private int startAngle = 0;
	private static int numberOfSteps = 8;
	private int angleStep = 360 / numberOfSteps;
	private enum dials {A, E, I, O, U, É, È, OU};

	public void StartDrag(){


		// récupère les éléments au début du drag.
		originalRotation = transform.rotation;
		//	transform.position=The position of the transform in world space.
		//	Attention le transform est en fait un RectTransform semble renvoyer directement une info locale (parceque fils d'un canvas?) donc pas besoin de passer par Camera.main.WorldToScreen
		
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

		int newAngle;
		int numberOfStepsDone;
		Vector2 screenPos = new Vector2 (transform.position.x, transform.position.y);
		Vector2 vector = Input.GetTouch(0).position - screenPos;
		int angle = (int) Mathf.Ceil(Mathf.Atan2 (vector.x, vector.y) * Mathf.Rad2Deg);
		newAngle = (angle - startAngle);
		numberOfStepsDone= newAngle/angleStep;
		Quaternion newRotation = Quaternion.AngleAxis(numberOfStepsDone*angleStep , -transform.forward);
		newRotation.y = 0;
		newRotation.eulerAngles = new Vector3(0,0,newRotation.eulerAngles.z);
		transform.rotation = originalRotation *  newRotation;

#else

		int newAngle;
		int numberOfStepsDone;
		Vector3 screenPos = transform.position;
		Vector3 vector = Input.mousePosition - screenPos;
		int angle = (int) Mathf.Ceil(Mathf.Atan2 (vector.x, vector.y) * Mathf.Rad2Deg);
		newAngle = (angle - startAngle);
		numberOfStepsDone= newAngle/angleStep;
		Quaternion newRotation = Quaternion.AngleAxis(numberOfStepsDone*angleStep , -transform.forward);
		newRotation.y = 0;
		newRotation.eulerAngles = new Vector3(0,0,newRotation.eulerAngles.z);
		transform.rotation = originalRotation *  newRotation;

#endif
	}

	//nomenclature des écouteurs, pourrait avoir plusieurs paramètres
	public delegate void PickedDial(int pickedDial, string nameOfPickedDial, int nameDial);
	//static donc peut être adressé par tout le monde
	public static event PickedDial OnPickedDial;
	
	public void EndDrag(){
		// send an event with the final dial

		int numberOfStepsDone;
		numberOfStepsDone= (int) transform.localEulerAngles.z/angleStep; 

		OnPickedDial (numberOfStepsDone, Enum.GetName(typeof(dials), numberOfStepsDone), 1 );
		
	}
}
