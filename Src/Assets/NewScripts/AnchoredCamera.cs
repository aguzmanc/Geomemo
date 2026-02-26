using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class AnchoredCamera : MonoBehaviour 
{
	public static int STATE_ANCHORED = 0;
	public static int STATE_MOVING_TO_ANCHOR = 1;

	public float TransitionSeconds = 3.0f;

	public CameraHolder _currentHolder;
	private Vector3 _lastPos;
	private Vector3 _lastTargetPos;
	private int _currentState;
	private float _iniTime;

	DepthOfField _dof;

	private static AnchoredCamera _singletonInstance;
	public static AnchoredCamera Instance{
		get{return _singletonInstance;}
	}

	void Start () 
	{
		_singletonInstance = this;

		_currentState = STATE_ANCHORED;


	}
	
	void Update () 
	{
		if (_currentHolder == null) 
			return; // nothing to do

		if (_currentState == STATE_ANCHORED) {
			transform.position = _currentHolder.Holder.position;
			transform.LookAt (_currentHolder.Target.position);
		} else if (_currentState == STATE_MOVING_TO_ANCHOR) {
			float p = (Time.time - _iniTime) / TransitionSeconds;
			p = Mathf.Min (1.0f, p);

			transform.position = Vector3.Lerp(_lastPos, _currentHolder.Holder.position, p);
			transform.LookAt(Vector3.Lerp (_lastTargetPos, _currentHolder.Target.position, p));

			if(p == 1.0f){
				_currentState = STATE_ANCHORED;
				transform.position = _currentHolder.Holder.position;
				transform.LookAt (_currentHolder.Target);
			}
		}
	}


	public void AssignHolder(CameraHolder holder)
	{
		if (_currentHolder == null) {
			_lastPos = Vector3.zero;
			_lastTargetPos = Vector3.up;
		} else if (_currentState == STATE_ANCHORED) {
			_lastPos = _currentHolder.Holder.position;
			_lastTargetPos = _currentHolder.Target.position;
		} else {
			_lastPos = transform.position;
			_lastTargetPos = transform.position + transform.rotation * Vector3.forward; // a point just in front of the camera
		}


		_currentHolder = holder;
		_currentState = STATE_MOVING_TO_ANCHOR;

		_dof = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
		if (_dof != null) {
			_dof.focalTransform = _currentHolder.Target;
		}


		_iniTime = Time.time;
	}
}
