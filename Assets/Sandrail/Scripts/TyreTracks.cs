using UnityEngine;
using System.Collections;

public class TyreTracks : MonoBehaviour
{
	public WheelCollider correspondingWheelCollider;
	//
	public GameObject correspondingTrail;
	//
	private TrailRenderer trail;
	public float trailTime = 2.5f;
	private float lifeTime = 0.0f;
	//

	//
	void Start ()
	{
		trail = correspondingTrail.GetComponent<TrailRenderer> ();
	}
	
	//
	void FixedUpdate ()
	{
		if (correspondingWheelCollider.isGrounded == false) {
			lifeTime = 0.0f;
		} else {
			lifeTime = lifeTime + 1.0f * Time.deltaTime;
		}
		
		if (lifeTime >= trailTime) {
			lifeTime = trailTime;
		}
		
		trail.time = lifeTime;
		
		trail.material.mainTextureScale = new Vector2 (1.0f * lifeTime, 1.0f);
	}
}