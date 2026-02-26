using UnityEngine;
using System.Collections;

public class FadingIn : MonoBehaviour 
{
	public float Duration = 1.0f;
	public UnityEngine.UI.Image Image;
	public UnityEngine.UI.Text Text;

	private bool _isFading;
	private float _startTime;
	private Color _colorImage;
	private Color _colorText;

	void Awake()
	{
		if (Image != null)
			_colorImage = Image.color;
		
		if (Text != null)
			_colorText = Text.color;
		
		_isFading = false;
	}

	void Start () 
	{

	}
	
	void Update () 
	{
		if (false == _isFading)
			return;

		float p = (Time.time - _startTime) / Duration;
		p = Mathf.Min (p, 1.0f);

		if (Image != null)
			Image.color = Color.Lerp (
				new Color(_colorImage.r, _colorImage.g, _colorImage.b, 0),
				_colorImage, p);

		if (Text != null)
			Text.color = Color.Lerp (
				new Color(_colorText.r, _colorText.g, _colorText.b),
				_colorText, p);

		if (p == 1.0f)
			_isFading = false;
	}


	public void StartFading()
	{
		_startTime = Time.time;
		_isFading = true;
	}
}
