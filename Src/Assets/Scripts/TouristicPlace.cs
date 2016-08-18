using UnityEngine;
using System.Collections;

public class TouristicPlace : MonoBehaviour 
{

	// OBSOLETE
//
//	public string Name;
//	public Transform MainRotationPosition;
//	public Transform CameraHolderPlace;
//	public Transform MovingTarget;
//	public Transform DepthOfFieldFocusTarget;
//	public string Key;
//
//	public bool _isInside;
//
//	private CameraFollower _cam;
//
//
//	void Awake()
//	{
//		_cam = Camera.main.GetComponentInParent<CameraFollower> ();
//		_isInside = false;
//	}
//
//
//	void Start () 
//	{
//
//	}
//
//	
//	void Update () 
//	{
//		if (_isInside) {
//			if(Input.GetKeyDown(KeyCode.Escape) && (false == UIManager.Instance.IsWaitingForTakePhotos) ){
//				_isInside = false;
//				UIManager.Instance.HidePhotos();
//
//				if(GameManager.Instance.IsGameEnded) {
//					GameManager.Instance.GameWin();
//				} else{
//					_cam.ReturnToPlayer();
//					// _cam.StartFollowing();
//					
//					UIManager.Instance.ShowClueImage (GameManager.Instance.CurrentPlaceToFind.Key);
//					GameManager.Instance.ResumeGame();
//				}
//			}
//		}
//	}
//
//
//	public void ActivatePlace ()
//	{
//		if (false==_isInside) {
//			GameManager.Instance.PlaceFound();
//
//			_cam.MoveToTouristicPlace(this);
//			_isInside = true;
//			
//			UIManager.Instance.TakeFlash(Key);
//			UIManager.Instance.HideClueImage();
//		}
//	}
}
