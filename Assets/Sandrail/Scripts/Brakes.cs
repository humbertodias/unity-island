using UnityEngine;
using System.Collections;

public class Brakes : MonoBehaviour
{
	public WheelCollider frontLeft;
	public WheelCollider frontRight;
	public WheelCollider rearLeft;
	public WheelCollider rearRight;
	//
	public float brakeForce = -1000.0f;
	// IMPORTANT : only use a value between 0.0~1.0 on brakeBalance : IMPORTANT //
	public float brakeBalance = 0.5f; // Percentage : a Value of 0.75 = front(0.75) rear(0.25) ; a Value of 0.25 = front(0.25) rear (0.75)
	//
	public float noInputMultiplier = -0.35f;
	//
	private float frontBrakeBalance = 0.0f;
	private float rearBrakeBalance = 0.0f;
	//
	public float applyBrakeForceFront = 0.0f;
	public float applyBrakeForceRear = 0.0f;
	//
	public float inputValue = 0.0f;
	//
	private float relativeVelocity = 0.0f;
	//

	//
	void Start ()
	{
		frontBrakeBalance = brakeBalance;
		rearBrakeBalance = 1.0f - brakeBalance;
	}
	
	//
	void FixedUpdate ()
	{
		relativeVelocity = transform.InverseTransformDirection (GetComponent<Rigidbody>().velocity).z;
		
		inputValue = Input.GetAxis ("Vertical");
		
		if (inputValue < 0.0f && relativeVelocity > 0.5f) {
			applyBrakeForceFront = (inputValue * brakeForce) * frontBrakeBalance;
			applyBrakeForceRear = (inputValue * brakeForce) * rearBrakeBalance;
			
			frontLeft.brakeTorque = applyBrakeForceFront;
			frontRight.brakeTorque = applyBrakeForceFront;
			rearLeft.brakeTorque = applyBrakeForceRear;
			rearRight.brakeTorque = applyBrakeForceRear;
			
		} else if (inputValue < 0.0f && relativeVelocity < 0.5f) {
			frontLeft.brakeTorque = 0.0f;
			frontRight.brakeTorque = 0.0f;
			rearLeft.brakeTorque = 0.0f;
			rearRight.brakeTorque = 0.0f;
			
		} else if (inputValue == 0 && relativeVelocity < 3.0f) {
			applyBrakeForceFront = (noInputMultiplier * brakeForce) * frontBrakeBalance;
			applyBrakeForceRear = (noInputMultiplier * brakeForce) * rearBrakeBalance;
			
			frontLeft.brakeTorque = applyBrakeForceFront;
			frontRight.brakeTorque = applyBrakeForceFront;
			rearLeft.brakeTorque = applyBrakeForceRear;
			rearRight.brakeTorque = applyBrakeForceRear;
			
		} else {
			frontLeft.brakeTorque = 0.0f;
			frontRight.brakeTorque = 0.0f;
			rearLeft.brakeTorque = 0.0f;
			rearRight.brakeTorque = 0.0f;
		}
	}
}
