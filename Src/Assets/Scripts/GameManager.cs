using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private static GameManager _singletonInstance;
	public static GameManager Instance{
		get{return _singletonInstance;}
	}

	public Place[] Places;

	public float MaxTimeToMemorize;
	public float TimeBeforeShow;

	public CameraHolder GeneralPanFirstCameraHolder;
	public CameraHolder MainCharacterCameraHolder;
	public CameraHolder EndGamePanCameraHolder;

	public AnchoredCamera Cam;
	public float TimeToFindNextObjective;
	public float StartTime;

	private Place _currentPlaceToFind; // not discovered yet
	private Place _lastFoundPlace; // discovered

	private float _timeToStart;
	private bool _gameStarted;

	private int _currentPlaceIndex;
	private bool _gamePaused;
	private bool _gameEnded;
	private bool _gameLost;
	private bool _canLeaveThisLevel;
	private float _timeLeft;
	private bool _inputLocked;


	public Place CurrentPlaceToFind{get{return _currentPlaceToFind;}}
	public bool IsGameEnded {get{return _gameEnded;}}
	public bool IsGamePaused {get{return _gamePaused;}}
	public bool GameStarted {get{return _gameStarted;}}
	public float TimeLeft {get{return _timeLeft;}}
	public float TimeToStart {get{return _timeToStart;}}

	public bool ShuffleOrder = true;

	void Awake()
	{
		_singletonInstance = this;
	}

	void Start ()
	{
		_canLeaveThisLevel = false;

		StartCoroutine ("StartGame");

		// shuffle places list
		if (ShuffleOrder) {
			for (int i = 0; i < Places.Length; i++) {
				Place temp = Places[i];
				int randomIndex = Random.Range(i, Places.Length);
				Places[i] = Places[randomIndex];
				Places[randomIndex] = temp;
			}
		}

		if (Places.Length == 0) 
			throw new UnityException("GameManager must have Places");
	}
	
	void Update () 
	{
		if (_canLeaveThisLevel && Input.anyKeyDown) {
			Application.LoadLevel("MainScreen");
		}


		if (_gameStarted == false) {
			_timeToStart -= Time.deltaTime;
		}

		if (_gamePaused){

			if(false == _inputLocked &&  
			   Input.anyKeyDown
//			   Input.GetKeyDown (KeyCode.Escape)
			   ) { // exit from "show place" mode
				ResumeGame();

				AssignNextObjective();

			}
		}else {
			if(false ==_gameEnded)
				_timeLeft -= Time.deltaTime;
			
			if(_timeLeft <= 0.0f)  // gameOver
			{
				GameLost();
				Invoke("LeaveLevel", 4.0f);
			}
		}
	}


	public void AssignNextObjective()
	{
		_currentPlaceToFind.LeavePlace ();
		_lastFoundPlace = _currentPlaceToFind;

		_currentPlaceIndex ++;

		if (_currentPlaceIndex >= Places.Length) {
			GameWin ();
			Invoke("LeaveLevel", 4.0f);
		}
		else {
			_currentPlaceToFind = Places [_currentPlaceIndex];
			UIManager.Instance.ResumeGame (_currentPlaceToFind.CluePhoto);
			_currentPlaceToFind.Search();
		}
	}


	public void PlaceFound(Place place)
	{
		_inputLocked = true;
		_gamePaused = true;

//		_timeLeft = TimeToFindNextObjective;

		UIManager.Instance.ShowName (place.Nombre);
		UIManager.Instance.HideClueImage ();
		StartCoroutine ("TakePhotos");
	}

	IEnumerator StartGame()
	{
		_inputLocked = true;
		_gameStarted = false;
		_gameEnded = false;
		_gameLost = false;
		_gamePaused = true;

		UIManager.Instance.StartGameUI();
		Cam.AssignHolder (GeneralPanFirstCameraHolder);

		_timeToStart = MaxTimeToMemorize;

		yield return new WaitForSeconds (MaxTimeToMemorize - TimeBeforeShow);
		UIManager.Instance.ShowTimeToStart ();
		yield return new WaitForSeconds (TimeBeforeShow);
		UIManager.Instance.HideTimeToStart ();
		Cam.AssignHolder (MainCharacterCameraHolder);
		_timeLeft = StartTime;
		UIManager.Instance.ShowFadeOutText ("3", 1.0f, 0.5f);
		yield return new WaitForSeconds (1.0f);
		UIManager.Instance.ShowFadeOutText ("2", 1.0f, 0.8f);
		yield return new WaitForSeconds (1.0f);
		UIManager.Instance.ShowFadeOutText ("1", 1.0f, 1.0f);
		yield return new WaitForSeconds (1.0f);
		UIManager.Instance.ShowFadeOutText ("¡VAMOS!", 2.0f, 1.5f);

		yield return new WaitForSeconds (1.0f);

		_gameStarted = true;

		_currentPlaceIndex = 0;	
		_currentPlaceToFind = Places [_currentPlaceIndex]; // first one!


		UIManager.Instance.ResumeGame (_currentPlaceToFind.CluePhoto);
		_currentPlaceToFind.Search ();

		ResumeGame ();
	}

	IEnumerator TakePhotos()
	{
		_gamePaused = true;
		_inputLocked = true;

		UIManager.Instance.ShowingPlace ();
		yield return new WaitForSeconds (1.0f);
		UIManager.Instance.TakePhotoA (_currentPlaceToFind.Photo_A);
		yield return new WaitForSeconds (1.0f);
		UIManager.Instance.TakePhotoB(_currentPlaceToFind.Photo_B);
		UIManager.Instance.ShowPressKeyToContinueMessage ();
		yield return new WaitForSeconds (1.0f);

		_inputLocked = false;
	}

	public void ResumeGame()
	{
		_gamePaused = false;

		Cam.AssignHolder (MainCharacterCameraHolder);
	}

	public void GameWin()
	{
		_gameEnded = true;

		Cam.AssignHolder (EndGamePanCameraHolder);
		UIManager.Instance.ShowWinnerMessage ();
	}

	public void GameLost()
	{
		_gameLost = true;
		_gameEnded = true;
		_gamePaused = true;
		_inputLocked = true;
		Cam.AssignHolder(EndGamePanCameraHolder);
		UIManager.Instance.ShowLooserMessage();
	}

	public void LeaveLevel()
	{
		_canLeaveThisLevel = true;
		UIManager.Instance.ShowLeaveLevel ();
	}


	public void AddTimeLeft(float timeLeft)
	{
		_timeLeft += timeLeft;
	}
}
