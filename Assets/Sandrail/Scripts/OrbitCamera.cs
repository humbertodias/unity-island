using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour
{
	public Camera orbitCamera;
	//
	private float xSpeed = 25.0f;
	private float ySpeed = 15.0f;
	//
	private float x = 0.0f;
	private float y = 0.0f;
	//
	private float yMinLimit = -45.0f;
	private float yMaxLimit = 45.0f;
	//
	private Quaternion rotation;
	private Vector3 currentPosition;
	private Vector3 position;
	//
	private float minDistance = 2.5f;
	private float maxDistance = 5.5f;
	//
	
	//
	void Start ()
	{
		position = orbitCamera.transform.localPosition;
	}
	
	//
	void LateUpdate ()
	{
		if (Input.GetMouseButton (0)) {
			x += Input.GetAxis ("Mouse X") * xSpeed;
 		       
			rotation = Quaternion.Euler (y, x, 0.0f);
        
			transform.localRotation = rotation;
		}
		
		if (Input.GetMouseButton (1)) {
			y += Input.GetAxis ("Mouse Y") * ySpeed;
			
			y = Mathf.Clamp (y, yMinLimit, yMaxLimit);
 		       
			rotation = Quaternion.Euler (y, x, 0.0f);
        
			transform.localRotation = rotation;
		}
		
		if (Input.GetKeyDown (KeyCode.PageUp)) {
			currentPosition.z = orbitCamera.transform.localPosition.z;
			if (currentPosition.z > minDistance) {
				position.z = currentPosition.z - 0.25f;
			}
			orbitCamera.transform.localPosition = position;
		}
		
		if (Input.GetKeyDown (KeyCode.PageDown)) {
			currentPosition.z = orbitCamera.transform.localPosition.z;
			if (currentPosition.z < maxDistance) {
				position.z = currentPosition.z + 0.25f;
			}
			orbitCamera.transform.localPosition = position;
		}
	}
}
