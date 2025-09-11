using UnityEngine;
using System.Collections;

public class Place : MonoBehaviour 
{
	public PlaceActivator[] Activators; 
	public Sprite Photo_A;
	public Sprite Photo_B;
	public Sprite CluePhoto;
	public AudioSource FoundPlaceAudio;
	public string Nombre;

	private bool _found;
	private bool _isThisPlaceSearcheable;

	public bool HasBeenFound{get{return _found;}}
	
	void Start () 
	{
		_found = false;
		_isThisPlaceSearcheable = false;

		// make sure all activators are not enable at beginning of game
		foreach (PlaceActivator activator in Activators)
			activator.gameObject.SetActive (false);

		FoundPlaceAudio.enabled = false;
	}


	void Update () 
	{
	
	}


	public void Found()
	{
		if (_found && false==_isThisPlaceSearcheable) 
			return;

		_found = true;

		CameraHolder holder = GetComponent<CameraHolder> ();

		AnchoredCamera.Instance.AssignHolder (holder);

		foreach (PlaceActivator activator in Activators)
			activator.gameObject.SetActive (false);

		GameManager.Instance.PlaceFound(this);

		FoundPlaceAudio.enabled = true;
	}


	public void LeavePlace()
	{
		FoundPlaceAudio.enabled = false;
	}



	public void Search()
	{
		_isThisPlaceSearcheable = true;

		foreach (PlaceActivator activator in Activators)
			activator.gameObject.SetActive (true);
	}
}
