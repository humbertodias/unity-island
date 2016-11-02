using UnityEngine;
using System.Collections;

public class AntiRollBars : MonoBehaviour
{
	public WheelCollider leftWheelCollider;
	public WheelCollider rightWheelCollider;
	//
	public float antiRollPressure = 5000.0f;
	//
	private WheelHit hit;
	//
	private float leftSideTravel = 1.0f;
	private float rightSideTravel = 1.0f;
	//
	private float antiRollForce = 0.0f;
	//
	
	//
	void FixedUpdate ()
	{

		bool leftWheelGrounded = leftWheelCollider.GetGroundHit (out hit);
		
		if (leftWheelGrounded == true) {
			leftSideTravel = (-leftWheelCollider.transform.InverseTransformPoint (hit.point).y - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;
		}
		
		bool rightWheelGrounded = rightWheelCollider.GetGroundHit (out hit);
		
		if (rightWheelGrounded == true) {
			rightSideTravel = (-rightWheelCollider.transform.InverseTransformPoint (hit.point).y - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;
		}
		
		antiRollForce = (leftSideTravel - rightSideTravel) * antiRollPressure;
		
		if (leftWheelGrounded == true) {
			GetComponent<Rigidbody>().AddForceAtPosition (leftWheelCollider.transform.up * -antiRollForce, leftWheelCollider.transform.position);
		}
		
		if (rightWheelGrounded == true) {
			GetComponent<Rigidbody>().AddForceAtPosition (rightWheelCollider.transform.up * antiRollForce, rightWheelCollider.transform.position);
		}
	}
}
