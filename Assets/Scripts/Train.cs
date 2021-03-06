﻿using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {
	public WheelCollider[] rodasf;
	public WheelCollider[] rodasr;


	public float torque = 0;
	public GameObject Wayfather;
	public Transform[] waypoints;

	public int indexway  = 0;
	public bool activeWayPoint = true;


	int raio = 5;


	// Use this for initialization
	void Start () {
		waypoints = Wayfather.GetComponentsInChildren <Transform> ();
	}


	void FixedUpdate(){
		float produto = 0;
		if (activeWayPoint) {
			// circular
			indexway = indexway % waypoints.Length;

			Vector3 dir = waypoints [indexway].position - transform.position;
			Debug.DrawRay (transform.position, dir);

//			Debug.Log ("index" + indexway);
//			Debug.Log ("mag" + dir.magnitude);


			if (dir.magnitude < raio) {
				indexway++;
				if (indexway >= waypoints.Length) {
					indexway = 1;
				}
			}

			produto = Vector3.Dot (dir, transform.right);

		}
//		transform.Rotate (new Vector3 (0, produto, 0));

		foreach (WheelCollider roda in rodasf) {
			roda.steerAngle = produto * raio;
			roda.motorTorque = torque;
		}

		foreach (WheelCollider roda in rodasr) {
			roda.motorTorque = torque;
			roda.steerAngle = produto * -raio;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision col){
		Debug.Log ("OnCollisionEnter:" + col.collider.gameObject.tag);
		if (col.collider.gameObject.tag.Equals ("Player")) {
			Debug.Log ("COLIDIU");
			// transform é o trem
			col.collider.gameObject.transform.parent = transform.parent;
		}
	}

	void OnCollisionExit(Collision col){
		Debug.Log ("OnCollisionExit:" + col.collider.gameObject.tag);
		if (col.collider.gameObject.tag.Equals ("Player")) {
			Debug.Log ("COLIDIU");
			col.collider.gameObject.transform.parent = null;
		}
	}
}
