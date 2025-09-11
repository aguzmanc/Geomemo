using UnityEngine;
using System.Collections;

public class DelayedFollower : MonoBehaviour 
{
	public Transform Target;
	public Transform Follower;
	public bool FixedPosition = true;
	public float SecondsToReach = 2.0f;

	private Vector3 _offset;

	void Start () 
	{
		_offset = Follower.position - Target.position; 
	}
	
	void Update () 
	{
		Vector3 to = Target.position + _offset;

		if (FixedPosition) {
			Follower.position = to;
		} else {
			float d = Vector3.Distance(Follower.position, to);
			float vel = d / SecondsToReach;  // distance to reach

//			if(d < 0.5)
//				Follower.position = to;
//			else
				Follower.position = Follower.position + (to - Follower.position).normalized * Time.deltaTime * vel ;

			Debug.DrawRay(Follower.position, (to - Follower.position).normalized);
		}
	}
}
