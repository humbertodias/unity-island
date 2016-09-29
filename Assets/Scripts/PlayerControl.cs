using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


	public GameObject head;
	float headup;

	public Rigidbody rdb;

	// Use this for initialization
	void Start () {

		rdb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Pressionando E, trava para poder clicar mexendo o mouse
		// Ao pressionar E, o jogo se torna point-click
		if (Input.GetKey (KeyCode.E)) {

			if (Input.GetButtonDown ("Fire1")) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, 100)) {
					print (hit.collider.gameObject.name);
					Rigidbody rbout = hit.collider.gameObject.GetComponent<Rigidbody> ();
					if (rbout != null) {
//						rbout.AddForce (Vector3.up * 100);
						rbout.AddForce (ray.direction * 100);
					}

					Lamp lamp = hit.collider.gameObject.GetComponent<Lamp> ();
					if (lamp != null) {
						lamp.Interact ();
					}


				}

			}

			return;
		}

		if (Input.GetButtonDown ("Jump")) {
			rdb.AddForce (Vector3.up * 300);
		}


		// TRANSLACAO

//		transform.position += new Vector3 (0, 0, 0.1f);


		// Translate é Atalho para transform.position +=
//		transform.Translate(0, 0, 0.1f);	


//		// Frente
//		if(Input.GetKey(KeyCode.W))
//			transform.Translate(new Vector3 (0, 0, 0.1f));	
//		// Tras
//		if(Input.GetKey(KeyCode.S))
//			transform.Translate(new Vector3 (0, 0, -0.1f));
//		// Esquerda		
//		if(Input.GetKey(KeyCode.A))
//			transform.Translate(new Vector3 (-0.1f, 0, 0));	
//		// Direita
//		if(Input.GetKey(KeyCode.D))
//			transform.Translate(new Vector3 (0.1f, 0, 0));	



		// OU Simplificando

		float th = Input.GetAxis("Horizontal");
		float tv = Input.GetAxis("Vertical");
		headup -= Input.GetAxis ("Mouse Y");

//		transform.Translate ( new Vector3( th, 0, tv) * 0.1f );
		transform.Translate ( new Vector3( th, 0, tv) * Time.deltaTime * 10 );



		// ROTACAO

//		// -1 a 1
//		if (Input.GetAxis ("Mouse X") > 0)
//			transform.Rotate (new Vector3 (0, -1, 0));
//
//
//		if (Input.GetAxis ("Mouse X") < 0)
//			transform.Rotate (new Vector3 (0, 1, 0));




		// ou Simplificando

		transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * 100 );


		// Rotate acumula rotacao
		// Rotation não acumula
		headup = Mathf.Clamp( headup, -30, 30);
		head.transform.localRotation = Quaternion.Euler (headup,0, 0);


	}
}
