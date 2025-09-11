using UnityEngine;
using System.Collections;

public class Beacon : MonoBehaviour 
{
	private ParticleSystem _particles;

	public float DistanceFactor;

	void Start () 
	{
		_particles = GetComponent<ParticleSystem> ();
		DistanceFactor = 0.0f;
	}
	
	void Update () 
	{
//		_particles.


//		_particles.enableEmission = (DistanceFactor > 0.0f);
	}
}
