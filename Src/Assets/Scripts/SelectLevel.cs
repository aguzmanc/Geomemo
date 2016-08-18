using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;



public class SelectLevel : MonoBehaviour 
{
	public Transform LevelA;
	public Transform LevelB;
	public GameObject TextA;
	public GameObject TextB;

	public float TimeToChange = 0.5f;
	public float ScaleWhenSelected = 2.0f;
	public float ScaleWhenNotSelected = 1.0f;
	public float RotationVelocity = 50.0f;

	private Transform _currentLevel;
	private Transform _otherLevel;
	private bool _changing;
	private float _startTime;
	private AudioSource _audio;
	private bool _first = true;

	void Start () 
	{
		_currentLevel = LevelA;
		_otherLevel = LevelB;
		_changing = false;
		_audio = GetComponent<AudioSource> ();
	}
	
	void Update () 
	{
		_currentLevel.Rotate (Vector3.up * RotationVelocity * Time.deltaTime);

		if (_changing) {
			float p = (Time.time - _startTime) / TimeToChange;
			p = Mathf.Min (1.0f, p);

			_currentLevel.localScale = Vector3.one * Mathf.Lerp (ScaleWhenNotSelected, ScaleWhenSelected, p);
			_otherLevel.localScale = Vector3.one * Mathf.Lerp (ScaleWhenSelected, ScaleWhenNotSelected, p);

			_changing = p < 1.0f;
		} else if (Input.GetButtonDown ("Horizontal") || _first) {
			_first = false;
			_audio.Play();
			_changing = true;
			_startTime = Time.time;

			if (_currentLevel == LevelA) {
				_currentLevel = LevelB;
				_otherLevel = LevelA;

				TextA.SetActive(true);
				TextB.SetActive(false);
			} else if (_currentLevel == LevelB) {
				_currentLevel = LevelA;
				_otherLevel = LevelB;

				TextA.SetActive(false);
				TextB.SetActive(true);
			}

			Vector3 pos = _currentLevel.localPosition;
			_currentLevel.localPosition = new Vector3(pos.x, 0.5f, pos.z);

			pos = _otherLevel.localPosition;
			_otherLevel.localPosition = new Vector3(pos.x, 0.15f, pos.z);


		} else if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
			if(_currentLevel == LevelA)
                SceneManager.LoadScene("Uyuni");
			else if(_currentLevel == LevelB)
                SceneManager.LoadScene("Cochabamba_DOS");
			
		}

		
	}
}
