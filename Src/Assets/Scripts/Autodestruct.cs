using UnityEngine;
using System.Collections;

public class Autodestruct : MonoBehaviour 
{
	public float SecondsToAutodestruct = 3.0f;
	private float _timeLeft;

	void Start () 
	{
		_timeLeft = SecondsToAutodestruct;
	}
	
	void Update () 
	{
		_timeLeft -= Time.deltaTime;

		if (_timeLeft <= 0.0f)
			Destroy (this.gameObject);
	}
}
