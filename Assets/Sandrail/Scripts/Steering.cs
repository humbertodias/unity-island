using UnityEngine;
using System.Collections;

public class Steering : MonoBehaviour
{
	public WheelCollider frontLeftWheel;
	public WheelCollider frontRightWheel;
	//
	public Transform steeringWheel;
	//
	public float steeringSensitivity = 10.0f;
	//
	private float inputValue = 0.0f;
	//
	private float steeringValue = 0.0f;
	//
	public float maxRelativeVelocity = 50.0f;
	public float relativeVelocity = 0.0f;
	public float velocityPercentage = 0.0f;
	//
	
	//
	void FixedUpdate ()
	{
		relativeVelocity = transform.InverseTransformDirection (GetComponent<Rigidbody>().velocity).z;
		if (relativeVelocity < 0.0f) {
			relativeVelocity *= -1.0f;
		}
		
		velocityPercentage = (maxRelativeVelocity - relativeVelocity) / maxRelativeVelocity;
		
		velocityPercentage *= velocityPercentage;
		
		inputValue = Input.GetAxis ("Horizontal");
		
		steeringValue = (inputValue * steeringSensitivity) * velocityPercentage;
		
		frontLeftWheel.steerAngle = steeringValue;
		frontRightWheel.steerAngle = steeringValue;
		
		steeringWheel.localRotation = Quaternion.Euler (0.0f, inputValue * 30.0f, 0.0f);
	}
}
