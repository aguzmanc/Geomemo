using UnityEngine;
using System.Collections;

public class FadeProxy : MonoBehaviour 
{

	public float Alpha;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		UnityEngine.UI.Image img = GetComponent<UnityEngine.UI.Image> ();
		img.color = new Color (img.color.r, img.color.g, img.color.b, Alpha);

		UnityEngine.UI.Text text = transform.FindChild ("Text").GetComponent<UnityEngine.UI.Text> ();
		text.color = new Color (text.color.r, text.color.g, text.color.b, Alpha);
	}
}
