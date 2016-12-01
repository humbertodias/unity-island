using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public AudioSource fireSound;
	public AudioClip reloadSound;
	public AudioClip emptySound;
	public GameObject weaponMesh;
	public ParticleSystem bulletParticle;
	private Vector3 initialPosition;

	float cooldown = 0;
	int magazine = 12;


	Rigidbody rdbout;
	RaycastHit hit;
	Ray ray;

	// Use this for initialization
	void Start () {
		initialPosition = transform.localPosition;

		// Esconde/Bloqueia cursor
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		weaponMesh.transform.localPosition = Vector3.Lerp (weaponMesh.transform.localPosition, initialPosition
			, Time.deltaTime * 10);
		cooldown += Time.deltaTime;

		if (Input.GetButton ("Fire1") && !Input.GetKey (KeyCode.E) && cooldown > 0.1f) {

			// Esconde/Bloqueia cursor
			Cursor.lockState = CursorLockMode.Locked;

			magazine--;
			if (magazine > 0) {
				shoot ();
			} else {
				fireSound.PlayOneShot (emptySound);
			}
				
			cooldown = 0;
		} 

		if (Input.GetButtonDown ("Fire2") && magazine < 1){
		
			fireSound.PlayOneShot (reloadSound);
			magazine = 12;
			weaponMesh.transform.localPosition = new Vector3 (0, -10, -0.1f);
		}


}


	void shoot(){

		fireSound.PlayOneShot (fireSound.clip);

		weaponMesh.transform.localPosition = new Vector3 (0, 0, 0.1f);
		bulletParticle.Emit (100);

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		ray = Camera.main.ScreenPointToRay (transform.position);
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			// reposiciona emissor de particula
			bulletParticle.transform.position = hit.point;

			print ("Weapon.shot:" + hit.collider.gameObject.name);
//			Rigidbody rdbout = hit.collider.gameObject.GetComponent<Rigidbody> ();
			rdbout = hit.collider.gameObject.GetComponent<Rigidbody> ();
			Invoke("Impact", hit.distance * 0.05f);

		}
	}

	void Impact(){

		print ("Weapon.Impact");
		bulletParticle.transform.position = hit.point;
		bulletParticle.Emit (30);
		rdbout = hit.collider.gameObject.GetComponent<Rigidbody> ();
		if (rdbout != null) {
			rdbout.AddForceAtPosition (ray.direction * 200, hit.point);
		}
		print (hit.collider.name);
//		hit.collider.gameObject.SendMessage ("kill", SendMessageOptions.DontRequireReceiver);
		hit.collider.gameObject.SendMessageUpwards ("kill", SendMessageOptions.DontRequireReceiver);
	}


}