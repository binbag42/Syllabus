using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Main menu controller. Handle the animations and various logics/sub menus/panels of the main menu.
/// </summary>

public class MainMenuController : MonoBehaviour {

	public Animator startButton;
	public Animator settingsButton;
	public Animator dialog;
	public Animator contentPanel;
	public Animator gearImage;
	public Animator welcomeText;

	public GameObject txtWelcome;
	public GameObject musicController;
	public GameObject musicToggleButton;
	public GameObject fxToggleButton;
	public GameObject playerNameField;

	public GameObject infoPanel;
	public GameObject infoQuestionText1;
	public GameObject infoQuestionText2;
	public GameObject infoAnswerInputField;
	public GameObject parentalGatePanel;

	private int infoAnswer;

	//numéro et nom du joueur courant
	private int currentProfile;

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
		if (GameController.instance.isMusicOn) {
			MusicController.instance.PlayBgMusic();
		} else {
			MusicController.instance.StopBgMusic();
		}
		currentProfile = GameController.instance.selectedPlayer;
		if (!GameController.instance.firstStart) {txtWelcome.GetComponent<Text> ().text = "Welcome back "+GameController.instance.playersName [currentProfile];}
	}

	public void StartGame () {
		MusicController.instance.PlayClick ();
//		Application.LoadLevel ("RotateDial");
		Application.LoadLevel ("LevelSetupScene");
	}

	#region InfoPannel

	public void NewNameEnterred() {
		//sauvegarde dans l'instance du GameController le nouveau nom, qui sera enregistré dans file au moment où le setting sera fermé
		GameController.instance.playersName [currentProfile] = playerNameField.GetComponent<InputField> ().text;
	}

	public void PressedPlayerButtonRight(){
		if (++currentProfile == GameController.instance.numberMaxPlayer) {
			currentProfile = 0;
		}
			playerNameField.GetComponent<InputField>().text = GameController.instance.playersName[currentProfile];

	}

	public void PressedPlayerButtonLeft(){
		if (--currentProfile < 0) {
			currentProfile = GameController.instance.numberMaxPlayer-1;
		}
		playerNameField.GetComponent<InputField>().text = GameController.instance.playersName[currentProfile];
		
	}
	public void InfoMenuOpen(){
		//activate info panel when info button is pressed
		
		infoPanel.SetActive (true);
		int a=Random.Range(3,10);
		int b=Random.Range(3,10);
		int c=Random.Range(1,a*b);
		infoQuestionText1.GetComponent<Text> ().text = "a= "+a + "    b= " + b + "    c= " + c;
		infoQuestionText2.GetComponent<Text> ().text = "a*b-c=";
		infoAnswer = a * b - c;
		
	}
	
	public void InfoMenuEvaluation(){
		if (infoAnswerInputField.GetComponent<InputField> ().text == infoAnswer.ToString()) {
			infoPanel.SetActive (false);
			parentalGatePanel.SetActive (true);
		}
	}
	
	public void ParentalGateClose(){
		parentalGatePanel.SetActive (false);
	}
	
	public void InfoMenuClose(){
		infoPanel.SetActive (false);
	}

	#endregion


	#region Settings

	public void OpenSettings()
	{
		//comportement suivant les anciens settings
		//choix du sound on or off
		if (GameController.instance.isMusicOn) {
			musicToggleButton.GetComponent<Toggle> ().isOn = false;
		} else {
			musicToggleButton.GetComponent<Toggle> ().isOn = true;
		}
		//nom du dernier player
		playerNameField.GetComponent<InputField>().text = GameController.instance.playersName[currentProfile];
		
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

		//sauvegarde de l'état des settings (son oui/non et numéro du profil)
		if (musicToggleButton.GetComponent<Toggle> ().isOn) {
			GameController.instance.isMusicOn = false;
		} else {
			GameController.instance.isMusicOn = true;
		}
		GameController.instance.selectedPlayer = currentProfile;
		GameController.instance.Save ();
		//change le texte de bienvenue avec le nouveau nom sélectionné dans la fenetre settings
		txtWelcome.GetComponent<Text> ().text = "Welcome "+GameController.instance.playersName [currentProfile];
	}

	public void SoundButtonPressed(){
		//allume et éteint la musique lorsque le bouton MusicToggle est appuyé par l'utilisateur
		if (musicToggleButton.GetComponent<Toggle> ().isOn) {
			MusicController.instance.StopBgMusic ();
		} else {
			MusicController.instance.PlayBgMusic ();
		}

	}

	public void FXButtonPressed(){
		//allume et éteint les FX lorsque le bouton FXToggle est appuyé par l'utilisateur
		if (fxToggleButton.GetComponent<Toggle> ().isOn) {
			GameController.instance.isFXOn = false;
		} else {
			GameController.instance.isFXOn = true;
		}
	}

	#endregion

	public void ToggleMenu()
	{
		contentPanel.enabled = true;

		bool isHidden = contentPanel.GetBool("isHidden");
		contentPanel.SetBool("isHidden", !isHidden);
		gearImage.enabled = true;
		gearImage.SetBool("isHidden", !isHidden);

	}



}
