using UnityEngine;
using System.Collections;

public class FrontAxle : MonoBehaviour
{
	public WheelCollider frontLeftWheel;
	public WheelCollider frontRightWheel;
	private Vector3 centerOfLeftWheel;
	private Vector3 centerOfRightWheel;
	//
	public GameObject frontLeftMesh;
	public GameObject frontRightMesh;
	//
	public Transform axleJoint_L_000;
	public Transform axleJoint_L_001;
	public Transform axleJoint_L_002;
	//
	public Transform axleJoint_R_000;
	public Transform axleJoint_R_001;
	public Transform axleJoint_R_002;
	//
	public Transform steeringArm_L;
	public Transform steeringArm_R;
	//
	public Transform shockLower_L;
	public Transform shockLower_R;
	public Transform shockUpper_L;
	public Transform shockUpper_R;
	//
	private Vector3 transform_L;
	private Vector3 transform_R;
	//
	private RaycastHit hit_000;
	private RaycastHit hit_001;
	//
	private float steeringAngle_L_000 = 0.0f;
	private float steeringAngle_L_001 = 0.0f;
	private float steeringAngle_R_000 = 0.0f;
	private float steeringAngle_R_001 = 0.0f;
	//
	private float suspensionCompressionLeft = 0.0f;
	private float suspensionCompressionRight = 0.0f;
	private float suspensionAngle_L = 0.0f;
	private float suspensionAngle_R = 0.0f;
	//
	private float axlePivotAngle_L = 0.0f;
	private float axlePivotAngle_R = 0.0f;
	private float rotationValue_L = 0.0f;
	private float rotationValue_R = 0.0f;
	//
	private Vector3 localRotation_L_001;
	private Vector3 localRotation_R_001;
	//
	
	//
	void Update ()
	{
		centerOfLeftWheel = frontLeftWheel.transform.TransformPoint (frontLeftWheel.center);
		
		if (Physics.Raycast (centerOfLeftWheel, -frontLeftWheel.transform.up, out hit_000, frontLeftWheel.suspensionDistance + frontLeftWheel.radius)) {
			transform_L = hit_000.point + (frontLeftWheel.transform.up * frontLeftWheel.radius);
		} else {
			transform_L = centerOfLeftWheel - (frontLeftWheel.transform.up * frontLeftWheel.suspensionDistance);
		}
		
		centerOfRightWheel = frontRightWheel.transform.TransformPoint (frontRightWheel.center);
		
		if (Physics.Raycast (centerOfRightWheel, -frontRightWheel.transform.up, out hit_001, frontRightWheel.suspensionDistance + frontRightWheel.radius)) {
			transform_R = hit_001.point + (frontRightWheel.transform.up * frontRightWheel.radius);
		} else {
			transform_R = centerOfRightWheel - (frontRightWheel.transform.up * frontRightWheel.suspensionDistance);
		}
		
		suspensionCompressionLeft = transform_L.y - centerOfLeftWheel.y;
		suspensionCompressionRight = transform_R.y - centerOfRightWheel.y;
		
		if (suspensionCompressionLeft >= -0.000001f) {
			suspensionCompressionLeft = -0.000001f;
		}
		if (suspensionCompressionRight >= -0.000001f) {
			suspensionCompressionRight = -0.000001f;
		}
		
		axlePivotAngle_L = (Mathf.Atan2 (suspensionCompressionLeft * -1.0f, 0.7732195f)) * Mathf.Rad2Deg;
		axlePivotAngle_R = (Mathf.Atan2 (suspensionCompressionRight * 1.0f, 0.7732195f)) * Mathf.Rad2Deg;
		
		axleJoint_L_000.localRotation = frontLeftWheel.transform.localRotation * Quaternion.Euler (0.0f, frontLeftWheel.steerAngle, axlePivotAngle_L * -1.0f);
		axleJoint_L_001.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_L);
		axleJoint_L_002.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_L);
		
		axleJoint_R_000.localRotation = frontRightWheel.transform.localRotation * Quaternion.Euler (0.0f, frontRightWheel.steerAngle, axlePivotAngle_R * -1.0f);
		axleJoint_R_001.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_R);
		axleJoint_R_002.localRotation = Quaternion.Euler (0.0f, 0.0f, axlePivotAngle_R);
		
		frontLeftMesh.transform.localRotation = Quaternion.Euler (rotationValue_L, 0.0f, 0.0f);
		rotationValue_L += frontLeftWheel.rpm * (360 / 60) * Time.deltaTime;
		
		frontRightMesh.transform.localRotation = Quaternion.Euler (rotationValue_R, 0.0f, 0.0f);
		rotationValue_R += frontRightWheel.rpm * (360 / 60) * Time.deltaTime;
		
		steeringAngle_L_000 = frontLeftWheel.steerAngle / 355.0f;
		steeringAngle_R_000 = frontRightWheel.steerAngle / 355.0f;
		if (steeringAngle_L_000 < 0.0f) {
			steeringAngle_L_001 = steeringAngle_L_000 * -0.25f;
		} else {
			steeringAngle_L_001 = steeringAngle_L_000 * 0.05f;
		}
		if (steeringAngle_R_000 < 0.0f) {
			steeringAngle_R_001 = steeringAngle_R_000 * 0.05f;
		} else {
			steeringAngle_R_001 = steeringAngle_R_000 * 0.25f;
		}
		
		steeringArm_L.localPosition = new Vector3 (0.05803502f - steeringAngle_L_000 + (axlePivotAngle_L / 360.0f), 0.1501183f, -0.04844904f + steeringAngle_L_001);
		steeringArm_R.localPosition = new Vector3 (-0.05803502f - steeringAngle_R_000 + (axlePivotAngle_R / 360.0f), 0.1501183f, -0.04844904f + steeringAngle_R_001);
		
		suspensionAngle_L = (Mathf.Atan2 ((suspensionCompressionLeft + -0.6262f) * -1.0f, 0.0727f)) * Mathf.Rad2Deg;
		suspensionAngle_R = (Mathf.Atan2 ((suspensionCompressionRight + -0.6262f) * 1.0f, 0.0727f)) * Mathf.Rad2Deg;
		
		shockLower_L.localRotation = Quaternion.Euler (0.0f, 0.0f, (axlePivotAngle_L * -0.875f) + suspensionAngle_L);
		shockLower_R.localRotation = Quaternion.Euler (0.0f, 0.0f, (axlePivotAngle_R * -0.875f) + suspensionAngle_R);
		shockUpper_L.localRotation = Quaternion.Euler (0.0f, 0.0f, (axlePivotAngle_L * 0.1f) + suspensionAngle_L);
		shockUpper_R.localRotation = Quaternion.Euler (0.0f, 0.0f, (axlePivotAngle_R * 0.1f) + suspensionAngle_R);
	}
}