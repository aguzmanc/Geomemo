using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraFollower : MonoBehaviour 
{
	// OBSOLETE
//
//	public static int STATE_FOLLOWING_TARGET 				= 0;
//	public static int STATE_MOVING_TO_ROTATIONAL_TARGET		= 1;
//	public static int STATE_RETURNING_TO_PLAYER 			= 2;
//	public static int STATE_PANING_SCENARIO 				= 3;
//	public static int STATE_PANING_SCENARIO_GAME_ENDED 		= 4;
//
//	private Vector3 _offset;
//	private int _currentState;
//	private Vector3 _fromTargetPosition;
//	private Vector3 _toTargetPosition;
//	private float _startMovementTime;
//	private TouristicPlace _currentTouristicPlace;
//	private Vector3 _fromCameraPosition;
//	private Vector3 _toCameraPosition;
//
//	public GameObject Target;
//	public float MovementVelocity;
//	public GameObject MainCamera;
//	public float PanTimeSequence;
//	public Transform EndGameLookTo;
//	public Transform EndGameCameraHolder;
//
//	public Transform[] PanSequence;
//	private int _panIndex;
//	private int _nextPanIndex;
//	
//	
//	void Start () 
//	{
//		_offset = transform.position - Target.transform.position;
////		_currentState = STATE_PANING_SCENARIO;
//		_currentState = STATE_FOLLOWING_TARGET;
////		_currentState = STATE_PANING_SCENARIO_GAME_ENDED;
//
//		DepthOfField depthOfField = MainCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>();
//		depthOfField.enabled = false;
//		depthOfField.focalTransform = Target.transform;
//
//		_panIndex = 0;
//		_nextPanIndex = 1;
//		_startMovementTime = Time.time;
//	}
//	
//	void Update () 
//	{
//		if (_currentState == STATE_PANING_SCENARIO) {
//			Transform seq = PanSequence [_panIndex];
//			Transform cam = seq.transform.FindChild ("Cam");
//			Transform target = seq.transform.FindChild ("Target");
//
//			Transform nextSeq = PanSequence [_nextPanIndex];
//			Transform nextCam = nextSeq.transform.FindChild ("Cam");
//			Transform nextTarget = nextSeq.transform.FindChild ("Target");
//
//			float p = (Time.time - _startMovementTime) / PanTimeSequence;
//
//			if (p <= 1.0f) {
//				transform.position = Vector3.Lerp (cam.transform.position, nextCam.transform.position, p);
//
//				MainCamera.transform.LookAt (Vector3.Lerp (target.position, nextTarget.position, p));
//			} else {
//				_panIndex = (_panIndex + 1) % PanSequence.Length;
//				_nextPanIndex = (_nextPanIndex + 1) % PanSequence.Length;
//				_startMovementTime = Time.time;
//			}
//
//		} else if (_currentState == STATE_FOLLOWING_TARGET) {
//			Vector3 to = Target.transform.position + _offset;
//		
//			transform.position = Vector3.Lerp (transform.position, to, Time.deltaTime * 3.0f);
//		} else if (_currentState == STATE_MOVING_TO_ROTATIONAL_TARGET) {
//
//			float p = (Time.time - _startMovementTime) / MovementVelocity;
//
//			if (p <= 1.0f) {
//				Vector3 lookPos = Vector3.Lerp (_fromTargetPosition, _toTargetPosition, p);
//				MainCamera.transform.LookAt (lookPos);	
//				MainCamera.transform.position = Vector3.Lerp (_fromCameraPosition, _currentTouristicPlace.CameraHolderPlace.transform.position, p);
//			} else {
//				MainCamera.transform.position = _currentTouristicPlace.CameraHolderPlace.transform.position;
//				DepthOfField depthOfField = MainCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
//				depthOfField.focalTransform = _currentTouristicPlace.DepthOfFieldFocusTarget;
//
//				if (_currentTouristicPlace.MovingTarget != null) {
//					MainCamera.transform.LookAt (_currentTouristicPlace.MovingTarget);
//				} else {
//					MainCamera.transform.LookAt (_toTargetPosition);
//				}
//			}
//		} else if (_currentState == STATE_RETURNING_TO_PLAYER) {
//			float p = (Time.time - _startMovementTime) / MovementVelocity;
//
//			if (p <= 1.0f) {
//				Vector3 lookPos = Vector3.Lerp (_fromTargetPosition, _toTargetPosition, p);
//				MainCamera.transform.LookAt (lookPos);	
//				MainCamera.transform.position = Vector3.Lerp (_fromCameraPosition, _toCameraPosition, p);
//			} else {
//				_currentState = STATE_FOLLOWING_TARGET;
//
//				DepthOfField depthOfField = MainCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
//				depthOfField.focalTransform = Target.transform;
//			}
//		} else if (_currentState == STATE_PANING_SCENARIO_GAME_ENDED) {
//			// just pan scenario until eternity ^_^ , game ended!
//			transform.position = EndGameCameraHolder.transform.position;
//			MainCamera.transform.LookAt (EndGameLookTo);
//
//			DepthOfField depthOfField = MainCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
//			depthOfField.focalTransform = EndGameLookTo.transform;
//			depthOfField.focalSize = 0.2f;
//		}
//	}
//
//
//	public void MoveToTouristicPlace(TouristicPlace place)
//	{
//		_currentState = STATE_MOVING_TO_ROTATIONAL_TARGET;
//		_fromTargetPosition = Target.transform.position;
//		_toTargetPosition = place.MainRotationPosition.position;
//		_startMovementTime = Time.time;
//		_currentTouristicPlace = place;
//
//		_fromCameraPosition = transform.position;
//	}
//
//
//	public void ReturnToPlayer()
//	{
//		_currentState = STATE_RETURNING_TO_PLAYER;
//		_fromTargetPosition = _currentTouristicPlace.transform.position;
//		_toTargetPosition = Target.transform.position;
//
//		_startMovementTime = Time.time;
//
//		_currentTouristicPlace = null;
//
//		_fromCameraPosition = transform.position;
//		_toCameraPosition = Target.transform.position + _offset;
//	}
//
//	public void StartFollowing()
//	{
//		_currentState = STATE_FOLLOWING_TARGET;
//
//		DepthOfField depthOfField = MainCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>();
//		depthOfField.enabled = true;
//
//		transform.position = Target.transform.position + _offset;
//		MainCamera.transform.LookAt (Target.transform.position);
//	}
//
//	public void DoScenarioPan() 
//	{
//		_currentState = STATE_PANING_SCENARIO_GAME_ENDED;
//
//		transform.position = EndGameCameraHolder.transform.position;
//		MainCamera.transform.LookAt (EndGameLookTo);
//	}
}
