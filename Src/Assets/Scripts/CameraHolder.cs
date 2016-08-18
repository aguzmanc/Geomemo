using UnityEngine;
using System.Collections;

public class CameraHolder : MonoBehaviour 
{
	public Transform Holder;
	public Transform Target;

	public CameraHolder NextHolder;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "anchored_camera" && NextHolder != null) {
			AnchoredCamera cam = coll.gameObject.GetComponent<AnchoredCamera>();
			cam.AssignHolder(NextHolder);
		}
	}
}
