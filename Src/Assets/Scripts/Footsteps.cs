using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour 
{

	private AudioSource _audio;

	void Start () {
		_audio = GetComponent<AudioSource> ();
	
	}
	
	void Update () 
	{
		if (Input.GetAxis ("Horizontal") != 0.0f || Input.GetAxis ("Vertical") != 0.0f) {
			_audio.mute = false;
		} else
			_audio.mute = true;
	}
}
