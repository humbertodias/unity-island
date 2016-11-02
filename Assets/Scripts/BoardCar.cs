using UnityEngine;
using System.Collections;

public class BoardCar : MonoBehaviour {

	public MonoBehaviour carscripts;

	public GameObject playerInside;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Jump")) {
			if(playerInside)
			ExitCar ();
		}
	
	}

	void OnCollisionEnter(Collision col){
		Debug.Log ("ONCOLLISIONENTER CAR");
		if (col.collider.gameObject.tag.Equals ("Player")) {
			col.collider.gameObject.transform.parent = transform;

			carscripts.enabled = true;

			playerInside = col.collider.gameObject;

			col.collider.gameObject.transform.localPosition = new Vector3 (0, 0.8f, 0f);
			col.collider.gameObject.GetComponent<PlayerControl> ().enabled = false;
			col.collider.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			col.collider.gameObject.GetComponent<Collider> ().enabled = false;
			col.collider.gameObject.transform.localRotation = Quaternion.identity;
		}
	}


	void ExitCar(){
		Debug.Log ("EXITCAR");
	
		playerInside.transform.parent = null;	

		carscripts.enabled = false;

		playerInside.transform.position = transform.position - transform.right * 4 + transform.up;
		playerInside.GetComponent<PlayerControl> ().enabled = true;
		playerInside.GetComponent<Rigidbody> ().isKinematic = false;
		playerInside.GetComponent<Collider> ().enabled = true;
		playerInside.transform.localRotation = Quaternion.identity;


		playerInside = null;
	}

}
