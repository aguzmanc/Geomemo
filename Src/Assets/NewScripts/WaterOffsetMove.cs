using UnityEngine;
using System.Collections;

public class WaterOffsetMove : MonoBehaviour 
{
	public float Velocity = 0.1f;


	private Renderer _rend;
	private float _currentOffset;

	void Start () 
	{
		_rend = GetComponent<Renderer> ();
		_currentOffset = 0.0f;
	}
	

	void Update () 
	{
		_rend.material.SetTextureOffset("_MainTex", new Vector2(_currentOffset, 0));

		_currentOffset += Velocity;
	}
}
