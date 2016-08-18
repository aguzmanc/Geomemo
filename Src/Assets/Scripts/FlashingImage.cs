using UnityEngine;
using System.Collections;

public class FlashingImage : MonoBehaviour {

	public UnityEngine.UI.Image Image;
	public float Freq;
	
	private Color _color;
	private float _timeToStop;

	public float TimeToStop = 4.0f;

	void Start () 
	{
//		_color = Text.color;
		_timeToStop = TimeToStop;
		Image.enabled = true;
	}
	
	void Update () 
	{
		if (_timeToStop >= 0.0f) {
//			Text.color = new Color (_color.r, _color.g, _color.b, Mathf.Sin (Time.time * Freq) * 0.5f + 1.0f);
			Image.transform.localScale = Vector3.one * (Mathf.Sin (Time.time * Freq) * 0.5f + 1.0f);
			
			_timeToStop -= Time.deltaTime;
		} else
			Image.enabled = false;
	}
}
