using UnityEngine;
using System.Collections;

public class CreditosScript : MonoBehaviour 
{
	public GameObject Creditos;

	void Start () 
	{
		Creditos.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ShowCreditos()
	{
		Creditos.SetActive (true);
	}

	public void HideCreditos()
	{
		Creditos.SetActive (false);
	}


}
