using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

	public Animator startButton;
	public Animator settingsButton;
	public Animator dialog;
	public Animator contentPanel;
	public Animator gearImage;
	public Animator welcomeText;

	public GameObject txtWelcome;
	public GameObject mainCamera;
	public GameObject soundBtn;

	//nom du joueur
	private string currentProfile;

	// Use this for initialization
	void Start()
	{
		RectTransform transform = contentPanel.gameObject.transform as RectTransform;        
		Vector2 position = transform.anchoredPosition;
		position.y -= transform.rect.height;
		transform.anchoredPosition = position;

		//récupère les anciens settings
		GameController.instance.Load ();

		//comportement suivant les anciens settings
		if (!GameController.instance.isMusicOn) {
			mainCamera.GetComponent<AudioSource> ().mute = false;
		} else {
			mainCamera.GetComponent<AudioSource> ().mute = true;
		}

	}

	public void StartGame () {
		Application.LoadLevel ("RotateDial");
	}

	public void OpenSettings()
	{
		//comportement suivant les anciens settings
		if (GameController.instance.isMusicOn) {
			soundBtn.GetComponent<Toggle> ().isOn = true;
		} else {
			soundBtn.GetComponent<Toggle> ().isOn = false;
		}

		//gestion des animations
		startButton.SetBool("isHidden", true);
		settingsButton.SetBool("isHidden", true);
		welcomeText.SetBool ("isHidden", true);
		dialog.enabled = true;
		dialog.SetBool("isHidden", false);
	}

	public void CloseSettings()
	{
		//gestion des animations
		startButton.SetBool("isHidden", false);
		settingsButton.SetBool("isHidden", false);
		dialog.SetBool("isHidden", true);
		welcomeText.SetBool ("isHidden", false);

		//sauvegarde de l'état des settings
		if (soundBtn.GetComponent<Toggle> ().isOn) {
			GameController.instance.isMusicOn = true;
		} else {
			GameController.instance.isMusicOn = false;
		}
		GameController.instance.Save ();


	}
	

	public void ToggleMenu()
	{
		contentPanel.enabled = true;

		bool isHidden = contentPanel.GetBool("isHidden");
		contentPanel.SetBool("isHidden", !isHidden);
		gearImage.enabled = true;
		gearImage.SetBool("isHidden", !isHidden);

	}

}
