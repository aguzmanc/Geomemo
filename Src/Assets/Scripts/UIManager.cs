using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour 
{
	private static UIManager _singletonInstance;

	public static UIManager Instance {
		get{return _singletonInstance;}
	}

	public GameObject PhotoAPrototype;
	public GameObject PhotoBPrototype;
	public GameObject MemorizaPrototype;
	public GameObject FadeOutTextPrototype;
	public GameObject TicTacSound;

	public GameObject LabelTiempoRestante;
	public GameObject LabelTimeLeft;
	public GameObject LabelPlaceName;
	public GameObject PhotoClue;
	public GameObject LabelPista;
	public GameObject LabelPlus20Sec;
	public GameObject PressKeyToContinue;
	public GameObject LeaveLevelMessage;

	public UnityEngine.UI.Image _flash;
	public GameObject WinnerMessage;
	public GameObject LooserMessage;

	private string _currentKey;
	private float _flashLevel;

	void Awake()
	{
		_singletonInstance = this;
	}


	void Start () 
	{
		_flashLevel = 0.0f;


		//		Cursor.visible = false;
		//		Cursor.lockState = CursorLockMode.Locked;

	}
	
	void Update () 
	{
		float timeLeft = 0.0f;
		if (GameManager.Instance.GameStarted) 
			timeLeft = GameManager.Instance.TimeLeft;
		else
			timeLeft = GameManager.Instance.TimeToStart;

		if (LabelTimeLeft.activeSelf) {
			if(timeLeft >= 0.0f) {
				int plus = (int)(timeLeft * 100);

				UnityEngine.UI.Text text = 
					LabelTimeLeft.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();

				text.text = string.Format("{0:00}", (int)(plus / 100)) + "." + 
							string.Format("{0:00}", (int)(plus % 100)) + " seg.";

				if(timeLeft <= 5.0f)
					text.color = Color.red;
				else
					text.color = Color.green;
			}
		}

		_flash.color = new Color (255, 255, 255, _flashLevel);

		_flashLevel -= Time.deltaTime/2.0f;
		_flashLevel = Mathf.Max (0.0f, _flashLevel);
	}


	public void StartGameUI()
	{
		GameObject memoriza = (GameObject)Instantiate (MemorizaPrototype);
		memoriza.transform.SetParent (gameObject.transform, false);
		LabelTiempoRestante.SetActive (false);
		LabelTimeLeft.SetActive (false);
		PhotoClue.SetActive (false);
	}


	public void ShowTimeToStart()
	{
		LabelTiempoRestante.SetActive (true);
		LabelTimeLeft.SetActive (true);

		Instantiate (TicTacSound);
	}

	public void ShowFadeOutText(string text, float duration, float pitchLevel)
	{
		GameObject fadeOutText = Instantiate (FadeOutTextPrototype);
		fadeOutText.GetComponent<UnityEngine.UI.Text> ().text = text;
		fadeOutText.GetComponent<FadeOut> ().Duration = duration;
		fadeOutText.GetComponent<AudioSource> ().pitch = pitchLevel;
		fadeOutText.transform.SetParent (transform, false);
	}


	public void HideTimeToStart()
	{
		LabelTiempoRestante.SetActive (false);
		LabelTimeLeft.SetActive (false);
	}

	public void ResumeGame(Sprite clueImage)
	{
		LabelPlaceName.SetActive (false);

		PhotoClue.SetActive (true);
		PhotoClue.transform.FindChild ("Image").GetComponent<UnityEngine.UI.Image> ().sprite = clueImage;

		// remove photos
		GameObject [] photos = GameObject.FindGameObjectsWithTag ("photo");
		foreach (GameObject photo in photos)
			Destroy (photo);

		LabelTimeLeft.SetActive (true);

		PressKeyToContinue.SetActive (false);

		LabelPista.SetActive (true);
		LabelPista.AddComponent<Flashing> ();
		Flashing f = LabelPista.GetComponent<Flashing> ();
		f.MinScale = 0.6f;
		f.MaxScale = 1.7f;
		f.MinOpacity = 0.5f;
		f.FreqBySecond = 1.3f;
		f.Duration = 5.0f;
		f.DestroyOnFinish = false;
	}




	public void HideClueImage()
	{
		PhotoClue.SetActive (false);
		PhotoClue.transform.FindChild ("Image").GetComponent<UnityEngine.UI.Image> ().sprite = null;
	}

	public void ShowWinnerMessage()
	{
		WinnerMessage.SetActive (true);

		LabelPlaceName.SetActive (false);
		
		PhotoClue.SetActive (false);
		// remove photos
		GameObject [] photos = GameObject.FindGameObjectsWithTag ("photo");
		foreach (GameObject photo in photos)
			Destroy (photo);
		
		LabelTimeLeft.SetActive (false);
		PressKeyToContinue.SetActive (false);
		LabelPista.SetActive (false);
	}

	public void ShowLooserMessage() 
	{
		LooserMessage.SetActive (true);

		LabelPlaceName.SetActive (false);
		
		PhotoClue.SetActive (false);
		// remove photos
		GameObject [] photos = GameObject.FindGameObjectsWithTag ("photo");
		foreach (GameObject photo in photos)
			Destroy (photo);
		
		LabelTimeLeft.SetActive (false);
		PressKeyToContinue.SetActive (false);
		LabelPista.SetActive (false);
	}

	public void ShowName(string placeName)
	{
		LabelPlaceName.SetActive (true);
		LabelPlaceName.transform.Find ("Text").GetComponent<UnityEngine.UI.Text> ().text = placeName;
		LabelPlaceName.GetComponent<FadingIn> ().StartFading ();
	}

	public void ShowLeaveLevel()
	{
		LeaveLevelMessage.SetActive (true);
	}


	public void ShowingPlace()
	{
		LabelTimeLeft.SetActive (false);
	}


	public void TakePhotoA(Sprite sprite)
	{
		_flashLevel = 1.0f;
		GameObject photo = (GameObject)Instantiate (PhotoAPrototype);
		photo.transform.SetParent (gameObject.transform, false);
		photo.transform.FindChild ("Image").gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = sprite;
	}


	public void TakePhotoB(Sprite sprite)
	{
		_flashLevel = 1.0f;
		GameObject photo = (GameObject)Instantiate (PhotoBPrototype);
		photo.transform.SetParent (gameObject.transform, false);
		photo.transform.FindChild ("Image").gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = sprite;
	}

	public void ShowPressKeyToContinueMessage ()
	{
		PressKeyToContinue.SetActive (true);
	}

	public void Plus20Sec()
	{
		LabelPlus20Sec.SetActive (true);
		LabelPlus20Sec.GetComponent<Animation> ().Play ();
		LabelTimeLeft.GetComponent<Animation> ().enabled = true;
		LabelTimeLeft.GetComponent<Animation> ().Play ();
	}



}
