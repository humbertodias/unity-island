using UnityEngine;
using UnityEngine.AI;

public class ZombieRag : MonoBehaviour {

	public Animator anim;
	Rigidbody[] rdbs;
	public NavMeshAgent agent;
	public GameObject target;

	// Use this for initialization
	void Start () {
		rdbs = GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = true;
		}
	}

	// evento
	public void kill(){
		print ("ZombieRag.kill");
		anim.enabled = false;
		agent.enabled = false;
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = false;
		}

	}

	// evento
	void SetTarget(GameObject intarget){
		target = intarget;
	}


	void OnTriggerEnter(Collider col){
		print ("ZombieRag.OntriggerEnter:" + col.gameObject.name);
		if (col.gameObject.name.Equals ("Player")) {
			SetTarget (col.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

		if (agent != null  && target != null) {
			print ("update follow");
			agent.SetDestination (target.transform.position);
		}
	}
}
