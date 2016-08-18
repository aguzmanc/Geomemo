using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour {

	#region PUBLIC PROPERTIES
	public LineRenderer Lightning;
	public Transform[] Points;
	#endregion

	#region PRIVATE MEMBERS
	private int randomPoint;
	private int randomPositionX;
	private float positionY;
	private float _time;
	private Vector3 standardPosition;
	private bool _activate;
	#endregion

	#region UNITY METHODS
	void Start () {
		standardPosition = Points [0].position;
	}
	

	void Update () {

		if (Input.GetKeyDown (KeyCode.L)) {
			UpdateBolt ();
			_activate = true;
		}

		if (_activate) {
			_time += Time.deltaTime;
			Lightning.SetWidth ((Time.deltaTime * 4f) + 1f, (Time.deltaTime * 4f) + 0.5f);

			if (_time < 0.5) {
				Lightning.enabled = true;
			}
			if (_time > 1.5)
				_activate = false;
		
		} else {
			Lightning.enabled = false;
			_time = 0;
		}
	
	}
	#endregion

	#region PUBLIC METHODS
	public void UpdateBolt(){
		randomPoint = Random.Range (0, 3);
		positionY = standardPosition.y;
		for(int i = 0; i < 11; i++){
			randomPositionX = Random.Range (-2, 3);
			Lightning.SetPosition (i, new Vector3 (standardPosition.x + randomPositionX,positionY,standardPosition.z));
			positionY -= 3;
		}
	}
	#endregion
}
