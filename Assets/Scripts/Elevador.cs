using UnityEngine;
using System.Collections;

public class Elevador : MonoBehaviour {

	float terreo = 5.0f;
	float primAndar = 11.0f;
	float segundoAndar = 17.0f;


	public float altura = 55.0f;
	public float alturatogo = 55.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		altura = Mathf.Lerp (altura, alturatogo, Time.deltaTime);
		transform.localPosition=new Vector3(transform.localPosition.x, altura, transform.localPosition.z);
	}

	public void SetAndar(int andar){
		switch (andar) {
		case 0:
			alturatogo = terreo;
		break;

		case 1:
			alturatogo = primAndar;
		break;

		case 2:
			alturatogo = segundoAndar;
		break;


		}

	}

}
