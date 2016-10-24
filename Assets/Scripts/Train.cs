using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {
	public WheelCollider[] rodasf;
	public WheelCollider[] rodasr;


	public float torque = 0;
	public GameObject Wayfather;
	public Transform[] waypoints;

	public int indexway  = 0;


	// Use this for initialization
	void Start () {
		waypoints = Wayfather.GetComponentsInChildren <Transform> ();
	}


	void FixedUpdate(){
		float produto = 0;
			// circular
			indexway = indexway % waypoints.Length;

			Vector3 dir = waypoints [indexway].position - transform.position;
			Debug.DrawRay (transform.position, dir);

			Debug.Log ("index" +  indexway);
			Debug.Log ("mag" +  dir.magnitude);


			if (dir.magnitude < 5) {
				indexway++;
			}

		 produto = Vector3.Dot (dir, transform.right);
		transform.Rotate (new Vector3 (0, produto, 0));

		foreach (WheelCollider roda in rodasf) {
//			roda.steerAngle = produto * 5;
			roda.motorTorque = torque;
		}

		foreach (WheelCollider roda in rodasr) {
			roda.motorTorque = torque;
//			roda.steerAngle = -produto;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
