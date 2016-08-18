using UnityEngine;
using System.Collections;

public class PanningLeftRight : MonoBehaviour 
{
	public float Amplitude;
	public float Frequency;

	void Start () {
	
	}
	
	void Update () 
	{
		Vector3 p = transform.position;

		transform.position = new Vector3 (p.x, p.y, Mathf.Sin(Time.time * Frequency) * Amplitude);
	}
}
