using UnityEngine;
using System.Collections;

public class DayLight : MonoBehaviour {
	public float horas = 0;
	public float escalaTempo = 1;
	public Light luz;
	float intensidade = 0;
	public Color corDiurna;
	// Use this for initialization
	void Start () {
		if (luz == null) {
			luz = gameObject.GetComponent<Light> ();
			//luz = (Light) gameObject.GetComponent<"Light">;
		}
	}
	
	// Update is called once per frame
	void Update () {
		horas += Time.deltaTime * escalaTempo;

		transform.rotation = Quaternion.Euler (((horas * 360) / 24) - 90, 0, 0);


		if (horas > 17 || horas < 05) {
			intensidade = 0;
		} else {
			intensidade = 1;
		}
		luz.intensity = Mathf.Lerp (luz.intensity, intensidade, Time.deltaTime);
		RenderSettings.fogColor = Color.Lerp (Color.black, corDiurna, luz.intensity);

		if (horas >= 24){
			horas = horas - 24;
		} 
	}
}
