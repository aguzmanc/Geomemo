using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour 
{
	private Vector3 _oldPos;

	void Start () 
	{
		_oldPos = transform.position;
	}


	void Update () 
	{
		if (GameManager.Instance.IsGameEnded == false && 
			GameManager.Instance.IsGamePaused == false) {


			if (Input.GetKeyDown(KeyCode.Space) ) {
				Rigidbody r = GetComponent<Rigidbody> ();
				r.AddTorque (Vector3.forward*500000.0f, ForceMode.Impulse);
			}
		}


		if (transform.position == _oldPos)
			return;
	}
}

