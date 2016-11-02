using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour
{
	public WheelCollider rearLeftWheelCollider;
	public WheelCollider rearRightWheelCollider;
	//
	public bool automatic = true;
	public float[] gearbox;
	private int currentGear;
	private int appropriateGear;
	//
	public float engineTorque = 180.0f;
	public float minEngineRPM = 1100.0f;
	public float maxEngineRPM = 3500.0f;
	//
	private float engineRPM = 0.0f;
	private float applyTorque = 0.0f;
	//
	private float inputValue = 0.0f;
	//
	private float wheelRPM = 0.0f;
	//
	public float relativeVelocity = 0.0f;
	//
	
	//
	void FixedUpdate ()
	{
		relativeVelocity = transform.InverseTransformDirection (GetComponent<Rigidbody>().velocity).z;
		
		inputValue = Input.GetAxis ("Vertical");
		
		engineRPM = (rearLeftWheelCollider.rpm + rearRightWheelCollider.rpm) / 2 * gearbox [currentGear];
		Gearbox ();
			
		applyTorque = engineTorque / gearbox [currentGear] * inputValue;
			
		rearLeftWheelCollider.motorTorque = applyTorque;
		rearRightWheelCollider.motorTorque = applyTorque;
	}
	
	//
	void Gearbox ()
	{
		wheelRPM = (rearLeftWheelCollider.rpm + rearRightWheelCollider.rpm) / 2;
		
		if (engineRPM >= maxEngineRPM) {
			appropriateGear = currentGear;
			for (int h = 0; h < gearbox.Length; h++) {
				if (wheelRPM * gearbox [h] < maxEngineRPM) {
					appropriateGear = h;
					break;
				}
			}
			currentGear = appropriateGear;
		}
		
		if (engineRPM < maxEngineRPM) {
			appropriateGear = currentGear;
			for (int i = gearbox.Length - 1; i >= 0; i--) {
				if (wheelRPM * gearbox [i] < maxEngineRPM) {
					appropriateGear = i;
					break;
				}
			}
			currentGear = appropriateGear;
		}
	}
}
