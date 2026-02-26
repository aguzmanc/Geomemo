using UnityEngine;
using System.Collections;

public class Aro : MonoBehaviour 
{
	public Aro NextAro;
	public bool ForceShow = false;

	void Awake()
	{
		if (!ForceShow)
			gameObject.SetActive (false);
	}


	void Start () 
	{
//		GameObject body = transform.GetChild ("Body");
//		body.GetComponent<Animation>().Play
	}
	
	void Update () 
	{
	
	}



	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Player") {
			GetComponent<Animation>().Play();
			GetComponent<AudioSource>().Play ();

			Invoke ("RemoveMe", (45.0f/60.0f));

			if(NextAro != null)
				NextAro.gameObject.SetActive(true);
		}
	}


	void RemoveMe()
	{
		Destroy (this.gameObject);
	}
}
