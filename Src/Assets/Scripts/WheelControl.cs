using UnityEngine;
using System.Collections;

public class WheelControl : MonoBehaviour {

	public float Torque = 2500.0f;
	private WheelCollider _coll;

	public bool IsFrontWheel = false;
	public GameObject Body;
	private float _rot = 0;

	private AudioSource _audio;
	private float _pitch;

	void Awake()
	{
		_coll = GetComponent<WheelCollider> ();
		_audio = GetComponent<AudioSource> ();
		_pitch = 1.0f;
	}

	void Start () 
	{

	}
	
	void Update () 
	{

	}

	void FixedUpdate()
	{
//		float inp = Input.GetAxis ("Vertical");
//		if (inp > 0) {
//			_coll.motorTorque = inp * Torque;
//			_coll.brakeTorque = 0;
//		} else {
//			_coll.brakeTorque = (-inp) * Torque*3;
//			_coll.motorTorque = 0;
//		}

		if (false == IsFrontWheel) {
			if (GameManager.Instance.IsGamePaused) {
				_coll.brakeTorque = 2500.0f;
			} else  {
				if (Input.GetKey (KeyCode.Delete) || Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.B))
					_coll.brakeTorque = 5000.0f;
				else
					_coll.brakeTorque = 0.0f;
			}
		} else
			_coll.brakeTorque = 0.0f;

		Debug.Log (_coll.brakeTorque);


		_coll.motorTorque = Torque/2;//Input.GetAxis ("Vertical") * Torque;


		if(IsFrontWheel)
			_coll.steerAngle = Input.GetAxis ("Horizontal") * 30;

		if (Body != null) {
			Body.transform.rotation = _coll.transform.rotation * Quaternion.Euler (_rot, _coll.steerAngle, 0);

			RaycastHit hit;
			Vector3 collCenter = _coll.transform.TransformPoint (_coll.center);
			if ( Physics.Raycast(collCenter, - _coll.transform.up, out hit, _coll.suspensionDistance + _coll.radius ) )
				Body.transform.position = hit.point + (_coll.transform.up * _coll.radius);
			else
				Body.transform.position = collCenter - (_coll.transform.up * _coll.suspensionDistance);
		}

		_rot += _coll.rpm * 6 * Time.deltaTime;

		if (_audio != null) {
			if(Input.GetAxis("Vertical") > 0.0f){
				_pitch += 0.01f;
			} else
				_pitch -= 0.01f;

			_pitch = Mathf.Max (1.0f, Mathf.Min (3f, _pitch));

			_audio.pitch = _pitch;
		}
	}
}
