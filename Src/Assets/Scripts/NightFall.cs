using UnityEngine;
using System.Collections;

/**
 * THIS CLASS IS THE REASON FOR THE NAME OF THE GAME
 * */

public class NightFall : MonoBehaviour 
{
	public bool StartFalling;


	private static NightFall _singletonInstance;
	public static NightFall Instance{get{return _singletonInstance;}}

	public float P;

	public float TimeDuration;
	public Color InitColor;
	public Color EndColor;

	public float RotationVelocity;

	public float _timeElapsed;
	private bool _hasStarted;
	private Light _light;

	private Quaternion _initRotation;
	public Transform EndRotationTransform;

	void Awake()
	{
		_singletonInstance = this;

		_light = GetComponent<Light> ();
	}

	void Start () 
	{
		_hasStarted = false;
		_timeElapsed = 0.0f;

		_initRotation = transform.rotation;
		StartFalling = false;
	}


	void Update () 
	{
		if (_hasStarted == false) {
			if(GameManager.Instance.IsGamePaused == false){
				_hasStarted = true;
			}
			return;
		}

		// game started, so time is counting until nightFall
		if (false == GameManager.Instance.IsGamePaused && _hasStarted && StartFalling) {
//			transform.Rotate (new Vector3 (RotationVelocity, 0, 0));

			_timeElapsed += Time.deltaTime;

			_timeElapsed = Mathf.Min (TimeDuration, _timeElapsed); // max bound

			float p = (_timeElapsed/TimeDuration);
			this.P = p;

			_light.color = Color.Lerp(InitColor, EndColor, p); 

			transform.rotation = Quaternion.Lerp(_initRotation, EndRotationTransform.rotation, p);

			_light.intensity = 1.0f + p;
		}
	}
}
