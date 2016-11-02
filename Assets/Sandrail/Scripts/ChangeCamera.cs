using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour
{
	public GameObject[] camera_;
	public GameObject[] helpText;
	//
	private bool help = true;
	private int currentCamera = 0;
	//
	
	//
	void Start ()
	{
		for (int a = 0; a < camera_.Length; a++) {
			if (a == 0) {
				camera_ [a].SetActive (true);
			} else {
				camera_ [a].SetActive (false);
			}
		}
	}
	
	//
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.C)) {
			camera_ [currentCamera].SetActive (false);
			currentCamera += 1;
			if (currentCamera >= camera_.Length) {
				currentCamera = 0;
			}
			camera_ [currentCamera].SetActive (true);
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			if (help == false) {
				for (int b = 0; b < helpText.Length; b++) {
					helpText [b].SetActive (true);
				}
				help = true;
			} else {
				for (int b = 0; b < helpText.Length; b++) {
					helpText [b].SetActive (false);
				}
				help = false;
			}
		}
	}
}
