using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

	public Light luz;
//	public ParticleEmitter particulaEmissor;
	public ParticleSystem ps;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Interact () {
		if (luz != null) {
			luz.enabled = !luz.enabled;
			print ("Luz:" + luz.enabled);
		}

		if (ps != null) {
			if (!ps.isPlaying)
				ps.Play ();
			else
				ps.Stop ();

		}
	
	}
}
