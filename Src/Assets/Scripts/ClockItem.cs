using UnityEngine;
using System.Collections;

public class ClockItem : MonoBehaviour 
{
	private ParticleSystem _particles;
	public ClockItem NextClock;

	void Awake()
	{
		_particles = GetComponent<ParticleSystem> ();
		_particles.Play ();
	}

	void Start () 
	{

	}
	
	void Update () 
	{
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Player") {
			_particles.Stop();
			_particles.Play ();
			GameObject body = transform.FindChild("Body").gameObject;
			body.GetComponent<Animation>().Play();
			GetComponent<AudioSource>().Play ();

			UIManager.Instance.Plus20Sec();

			Invoke ("Add20Seconds", (45.0f/60.0f));
			Invoke ("RemoveBody", (45.0f/60.0f));
			Invoke ("RemoveMe", 1.5f);

			if(NextClock != null)
				NextClock.gameObject.SetActive(true);
		}
	}

	void Add20Seconds()
	{
		GameManager.Instance.AddTimeLeft (20);
	}

	void RemoveBody()
	{
		Destroy (transform.Find ("Body").gameObject);
	}

	void RemoveMe()
	{
		Destroy (this.gameObject);
	}

}
