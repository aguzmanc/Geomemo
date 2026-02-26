using UnityEngine;
using System.Collections;

public class SunRotation : MonoBehaviour 
{
	public Light fromLight;
	public Light toLight;
	public Light theSun;

	public float P;

	void Start () 
	{
	}
	
	void Update () 
	{
		float totalTime = GameManager.Instance.StartTime;
		float timeLeft = GameManager.Instance.TimeLeft;

//		Debug.Log (GameManager.Instance.StartTime);

		
		P = 1.0f - (timeLeft / totalTime);

		Debug.Log (P);

		theSun.transform.rotation = Quaternion.Lerp (fromLight.transform.rotation, toLight.transform.rotation, P);
		theSun.color = Color.Lerp (fromLight.color, toLight.color, P);
	}
}
