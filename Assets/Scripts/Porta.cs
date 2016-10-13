using UnityEngine;
using System.Collections;

public class Porta : MonoBehaviour {
	public bool mirror = false;
	float rotateto = 0;
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Lerp (transform.localRotation, 
			Quaternion.Euler (0, rotateto, 0), Time.deltaTime);
	}
	public void Interact(){
		if (Mathf.Abs (rotateto) < 10) {
			if (mirror) {
				rotateto = -90;
			} else {
				rotateto = 90;
			}
		} else {
			rotateto=0;
		}
	}
}
