using UnityEngine;
using System.Collections;

public class Flashing : MonoBehaviour 
{
	[Range(0.1f, 2.0f)]
	public float MinScale = 0.5f;
	[Range(0.1f, 2.0f)]
	public float MaxScale = 2.0f;

	[Range(0.0f, 1.0f)]
	public float MinOpacity = 0.5f;

	public float FreqBySecond = 2.0f;
	public float Duration = 5.0f;

	public UnityEngine.UI.Image Image;
	public UnityEngine.UI.Text Text;

	private float _timeToStop;
	private float _initTime;

	public bool DestroyOnFinish = true;

	void Start () 
	{
		_initTime = Time.time;
		_timeToStop = Duration;
	}
	
	void Update () 
	{
		_timeToStop -= Time.deltaTime;
		if (_timeToStop <= 0.0f) {
			if(DestroyOnFinish)
				Destroy(this.gameObject);
			else{
				Destroy (this);
				this.gameObject.SetActive(false);
			}

			return;
		}

		// from zero to 1
		float p = (Mathf.Sin (((Time.time - _initTime) * FreqBySecond) * 2.0f * Mathf.PI) + 1.0f) / 2.0f;

		transform.localScale = Vector3.one * (MinScale + (MaxScale - MinScale) * p);

		float opacity = MinOpacity + (1.0f - MinOpacity) * p;

		if (Image != null) 
			Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, opacity);

		if (Text != null)
			Text.color = new Color (Text.color.r, Text.color.g, Text.color.b, opacity);
	}
}
