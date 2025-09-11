using UnityEngine;
using System.Collections;

public class ProximitySound : MonoBehaviour 
{
	private AudioSource _audio;
	private GameObject _player;
	private SphereCollider _coll;
	private Beacon _beacon;

	public PlaceActivator Activator;

	void Awake()
	{
		_audio = GetComponent<AudioSource> ();
		_coll = GetComponent<SphereCollider> ();
//		_beacon = transform.FindChild ("Beacon").GetComponent<Beacon> ();
	}


	void Start () 
	{
		_audio.enabled = false;
		_player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		float dist = Vector3.Distance (_player.transform.position, transform.position);

		if (_audio.enabled) {

			float p = dist/_coll.radius;

			_audio.volume = 1.0f - p;
			_audio.pitch = (1.8f - 0.6f) * (1-p) + 0.6f;
		}


	}


	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Player") {
			_audio.enabled = true;
			Activator.IsPlayerClose(true);
		}
	}


	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "Player") {
			_audio.enabled = false;
			Activator.IsPlayerClose(false);
		}
	}
}
