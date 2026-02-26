using UnityEngine;
using System.Collections;

public class PlaceActivator : MonoBehaviour 
{
	private ParticleSystem _particles;

	public Place Place;
	public GameObject ActivateSoundPrototype;

	void Awake()
	{
		_particles = GetComponent<ParticleSystem> ();
		_particles.enableEmission = false;

	}

	void Start () 
	{
	}
	
	void Update () 
	{
	}


	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Player") {
			Place.Found();
			Instantiate(ActivateSoundPrototype);
		}
	}


	public void IsPlayerClose(bool isClose)
	{
		_particles.enableEmission = isClose;
	}
}
