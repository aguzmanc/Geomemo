using UnityEngine;
using System.Collections;

public class PlayIntroMovie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MovieTexture m = GetComponent<Renderer> ().material.mainTexture as MovieTexture;
		m.loop = true;
		m.Play ();
	}
	
	void Update () {
	
	}
}
