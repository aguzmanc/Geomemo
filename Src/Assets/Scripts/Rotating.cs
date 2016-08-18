using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour {

	public float Velocity;


	void Start () 
	{
	
	}
	
	void Update () 
	{
		transform.Rotate (Vector3.up, Velocity);
	}
}
