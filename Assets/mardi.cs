using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mardi : MonoBehaviour {

	private Rigidbody myRigidBody;
	public float jumpValue;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Left ();
		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Left ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Right ();
		}
	}

	public void Jump () {
		myRigidBody.AddForce (200f * Vector3.up);
	}
	public void Left () {
		myRigidBody.AddForce (30f * Vector3.left);
	}
	public void Right () {
		myRigidBody.AddForce (200f * Vector3.right);
	}

	void FixedUpdate () {

	}

	void OnTriggerEnter() {

	}
}
