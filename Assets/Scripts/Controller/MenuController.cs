using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour //SakuraGames

{

	private const float DEFAULT_HELP_TIMER = 2F;

	public GameObject btnPanel;
	public Button btnNewGame;
	public Button btnSound;
	public Button btnQuit;

	public Text txtHighScore;
	public Text txtCoin;
	public Text txtCopyright;
	public GameObject help;
	private float helpTimer = DEFAULT_HELP_TIMER; 
	private char SoundEnabled = 'A'; // S - Sound; M - Mute; A - Sound + Music

	void Start(){
		init ();
	}

	void FixedUpdate(){
		txtCoin.text = "Coins: " +
						GameObject.Find ("GamePlayController").GetComponent<GamePlayController> ().coinsCollected;

		if (helpTimer > 0) {
			helpTimer -=Time.deltaTime;
		} else if (help.activeSelf){
			help.SetActive(false);
		}
	}

	private void init(){
		SoundFunc (); // Set sound button depending on default sound option selected
		//PlayerPrefs.SetInt ("Player Help", 0);
	}

	private bool playerHelp(){
		if (PlayerPrefs.GetInt ("Player Help") < 3) {
			int i = PlayerPrefs.GetInt ("Player Help") + 1;
			PlayerPrefs.SetInt("Player Help",i );
			helpTimer = DEFAULT_HELP_TIMER;
			return true;
		}
		return false;
	}

	public void EnableMenu(bool boolean){
		btnPanel.SetActive (boolean);
		btnNewGame.enabled = boolean;
		btnQuit.enabled = boolean;
		txtHighScore.enabled = boolean;
		txtCopyright.enabled = boolean;
		txtHighScore.text = "High Score: " + PlayerPrefs.GetInt ("High Score").ToString ();
		txtCoin.enabled = !boolean;
		if (!boolean && playerHelp()) {
			help.SetActive(true);
		} else{
			help.SetActive(false);
		}
	}

	public void NewGameFunc(){
		GameObject.Find("GamePlayController").GetComponent<GamePlayController>().gameRunning = true;
		EnableMenu (false);
	}

	public void SoundFunc(){
		Text buttonText = btnSound.transform.Find ("Text").GetComponent<Text>();

		switch (SoundEnabled) {
		
		case 'A':
			SoundEnabled = 'S';
			buttonText.text = "Sound";
			break;
		case 'S':
			SoundEnabled = 'M';
			buttonText.text = "Mute";
			break;
		case 'M':
			buttonText.text = "Music";
			SoundEnabled = 'A';
			break;
		}
	}
	public char getSoundEnabled(){
		return SoundEnabled;
	}
	
	public void QuitFunc(){
		Application.Quit();
	}



	

}
