using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour {
	public Light luz;
	// Use this for initialization
	void Start () {
	
	}
	
	public void Interact(){
		luz.enabled = !luz.enabled;

	}
}
