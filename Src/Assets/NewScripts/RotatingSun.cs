using UnityEngine;
using System.Collections;

public class RotatingSun : MonoBehaviour 
{
	void Start () 
	{
	
	}
	
	void Update () 
	{
		float totalTime = GameManager.Instance.StartTime;
		float timeLeft = GameManager.Instance.TimeLeft;

		float p = (timeLeft / totalTime);

		Quaternion from = Quaternion.LookRotation (Vector3.left, Vector3.down);
		Quaternion to = Quaternion.LookRotation(Vector3.right, Vector3.up);

//		Debug.Log (from);


		transform.rotation = Quaternion.Lerp (from, to, p);
	}
}
