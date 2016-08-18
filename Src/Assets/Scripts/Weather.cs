using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weather : MonoBehaviour {

	#region PUBLIC PROPERTIES
	public GameObject Rain;
	public float FadeTime;
	public Text[] TimeMarker;
	#endregion

	#region PRIVATE MEMBERS
	private TOD_Sky sky;
	private GameObject WeatherObj;

	private float cloudDensity;
	private float cloudSharpness;
	private float cloudBrightness;
	private float startBrightness;
	private float atmosphereFog;

	private float cloudDensityDefault;
	private float cloudSharpnessDefault;
	private float cloudBrightnessDefault;
	private float startBrightnessDefault;
	private float atmosphereFogDefault;

	private bool lightningAct;
	private float timeLightning;
	private float maxLightning = 15f;
	#endregion

	#region UNITY METHODS
	void Start () {
		WeatherObj = GameObject.Find ("Sky Dome");
		sky = WeatherObj.GetComponent<TOD_Sky> ();

		cloudDensity = cloudDensityDefault = sky.Clouds.Density;
		cloudSharpness = cloudSharpnessDefault = sky.Clouds.Sharpness;
		cloudBrightness = cloudBrightnessDefault = sky.Clouds.Brightness;
		startBrightness = startBrightnessDefault = 25f;
		atmosphereFog = atmosphereFogDefault = sky.Atmosphere.Fogginess;
	}
	
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Clear ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Clouds ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Cloudy ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			Raining ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			Fog ();
		}
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			Storm();
		}

		if(Input.GetKeyDown(KeyCode.O)){
			Day ();
		}
		if(Input.GetKeyDown(KeyCode.P)){
			Night ();
		}

		float t = Time.deltaTime / FadeTime;

		sky.Clouds.Brightness    = Mathf.Lerp(sky.Clouds.Brightness,    cloudBrightness, t);
		sky.Clouds.Density       = Mathf.Lerp(sky.Clouds.Density,       cloudDensity,    t);
		sky.Clouds.Sharpness     = Mathf.Lerp(sky.Clouds.Sharpness,     cloudSharpness,  t);
		sky.Stars.Brightness     = Mathf.Lerp(sky.Stars.Brightness,     startBrightness,  t);
		sky.Atmosphere.Fogginess = Mathf.Lerp(sky.Atmosphere.Fogginess, atmosphereFog,  t);

		int hora1 = (int) sky.Cycle.Hour;
		TimeMarker [0].text = hora1.ToString("00");
		int minuto1 = (int) ((sky.Cycle.Hour - (int) sky.Cycle.Hour)*100/1.67);
		TimeMarker [1].text = minuto1.ToString("00");

		if (lightningAct) {
			timeLightning += Time.deltaTime;
			if (timeLightning > maxLightning) {
				timeLightning = 0;
				maxLightning = Random.Range (10,21);
				Debug.Log ("Rayo");
			}
		}

	}
	#endregion

	#region PUBLIC METHODS
	public void Cloudy(){
	
		cloudDensity = cloudDensityDefault;
		cloudSharpness = 0.2f;
		cloudBrightness = 0.3f;
		startBrightness = 10f;
		atmosphereFog = atmosphereFogDefault;
		Rain.SetActive (false);
		/*cloudDensity = 1f;
		cloudSharpness = 0.1f;
		cloudBrightness = 0.1f;
		startBrightness = 10f;*/
	}

	public void Raining (){

		cloudDensity = cloudDensityDefault;
		cloudSharpness = 0.2f;
		cloudBrightness = 0.1f;
		startBrightness = startBrightnessDefault;
		atmosphereFog = 1f;
		Rain.SetActive (true);
		lightningAct = true;

	}

	public void Clear(){
	
		cloudDensity = 0f;
		cloudSharpness = 1f;
		cloudBrightness = cloudBrightnessDefault;
		startBrightness = startBrightnessDefault;
		atmosphereFog = atmosphereFogDefault;
		Rain.SetActive (false);
		lightningAct = false;

	}

	public void Clouds(){
		
		cloudDensity = cloudDensityDefault;
		cloudSharpness = cloudSharpnessDefault;
		cloudBrightness = cloudBrightnessDefault;
		startBrightness = startBrightnessDefault;
		atmosphereFog = atmosphereFogDefault;
		Rain.SetActive (false);
		lightningAct = false;

	}

	public void Fog (){

		cloudDensity = cloudDensityDefault;
		cloudSharpness = cloudSharpnessDefault;
		cloudBrightness = cloudBrightnessDefault;
		startBrightness = startBrightnessDefault;
		atmosphereFog = 1f;
		Rain.SetActive (false);
		lightningAct = true;

	}

	public void Storm (){

		cloudDensity = cloudDensityDefault;
		cloudSharpness = cloudSharpnessDefault;
		cloudBrightness = cloudBrightnessDefault;
		startBrightness = startBrightnessDefault;
		atmosphereFog = 0.5f;
		Rain.SetActive (false);
		lightningAct = false;

	}

	public void DeterTiempo(){

	}

	public void SeguirTiempo(){
	
	}

	public void Day(){
		WeatherObj.GetComponent<TOD_Sky> ().Cycle.Hour = 12f;
	}

	public void Night(){
		WeatherObj.GetComponent<TOD_Sky> ().Cycle.Hour = 0f;
	}
	#endregion
}
