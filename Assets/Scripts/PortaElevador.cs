using UnityEngine;
using System.Collections;

public class PortaElevador : MonoBehaviour {

	float deslocar = 1.0f;
	int invert = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (deslocar < 3 && deslocar > -3) {
			transform.localPosition = new Vector3 ((transform.localPosition.x + deslocar * Time.deltaTime * invert), transform.localPosition.y, transform.localPosition.z);
		}
		deslocar += 0.01f;
	
	}

	public void deslocarValor(){
		deslocar = 1.0f;


	}

	public void invertesentido(){
		invert = invert * -1; 
	
	}




}
