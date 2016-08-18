using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour 
{
	public UnityEngine.UI.Text Text;
	public UnityEngine.UI.Image Image;
	public float Duration = 3.0f;
	public float StartScale = 1.0f;

	private Color _colorText;
	private Color _colorImage;
	private float _startTime;

	void Start () 
	{
		if (Text != null)
			_colorText = Text.color;

		if (Image != null)
			_colorImage = Image.color;

		_startTime = Time.time;
	}
	
	void Update () 
	{
		float p = (Time.time - _startTime) / Duration;

		transform.localScale = Vector3.one * (StartScale * (1-p));

		if (Text != null)
			Text.color = Color.Lerp (_colorText, new Color (_colorText.r, _colorText.g, _colorText.b, 1-p), p);

		if (Image != null)
			Image.color = Color.Lerp (_colorImage, new Color(_colorImage.r, _colorImage.g, _colorImage.b, 1-p), p);

		if (p >= 1.0f)
			Destroy (this.gameObject);
	}
}
