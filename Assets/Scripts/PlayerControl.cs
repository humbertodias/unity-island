using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GameObject head;
	float headup;
	Rigidbody rdb;

	// Use this for initialization
	void Start ()
	{
		rdb = gameObject.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.E)) {

			Cursor.lockState = CursorLockMode.None;

			if (Input.GetButtonDown ("Fire1")) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {
					print (hit.collider.gameObject.name);
					Rigidbody rdbout = hit.collider.gameObject.GetComponent<Rigidbody> ();
					if (rdbout != null) {
						rdbout.AddForce (ray.direction * 100);
					}
					/*
					Lamp lamp=hit.collider.gameObject.GetComponent<Lamp>();
					if (lamp != null) {
						lamp.Interact ();
					}
					*/
					hit.collider.gameObject.SendMessage ("Interact");
				}
			}
			return;
		}

		if (Input.GetButtonDown ("Jump")) {
			rdb.AddForce (Vector3.up * 200);
		}

		float th = Input.GetAxis ("Horizontal");
		float tv = Input.GetAxis ("Vertical");
		headup -= Input.GetAxis ("Mouse Y") * Time.deltaTime * 100;


		transform.Translate (new Vector3 (th, 0, tv) * Time.deltaTime * 10);
		transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * 100);

		headup = Mathf.Clamp (headup, -30, 30);
		head.transform.localRotation = Quaternion.Euler (headup, 0, 0);

	}
}
