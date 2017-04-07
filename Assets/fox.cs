using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class fox : MonoBehaviour {
	private float speed=0;
	private Rigidbody foxRigidBody;
	private float jumpForce=1.3f;
	private float jumpForceBack = 2f;
	private float airTime;
	public float jumpCoroutineTime=0.25f;
	public float airTimeCap=0.1f;
	public Text MortTexte;
	public float FasterRun=0.01f;
	private bool FoxJump=false;
	// Use this for initialization
	void Start () {
		foxRigidBody = GetComponent<Rigidbody> ();
		InvokeRepeating ("RunFaster", FasterRun, FasterRun);
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate () {
		Left();
		Debug.Log ("FOX SPEED" + speed);
	}
	void RunFaster()
	{
		speed += 0.01f;

		Debug.Log ("run faster");

	}
	IEnumerator ShortJumpCoroutine ()
	{
		float temps = 0.30f;
		//speed = maxSpeed + 0.1f;
			foxRigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse );
		yield return new WaitForSeconds(airTime);
		while (temps > 0f) {
			temps -= Time.deltaTime;
			foxRigidBody.AddForce (-Vector3.up * jumpForceBack);
			yield return null;
		}
	}
	IEnumerator LongJumpCoroutine ()
	{
		float temps = 2f;
		jumpForce=100f;
		speed += 1;
		jumpForceBack = 50f;
		//speed = maxSpeed + 0.1f;
		foxRigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse );
		yield return new WaitForSeconds(3f);
		while (temps > 0f) {
			temps -= Time.deltaTime;
			foxRigidBody.AddForce (-Vector3.up * jumpForceBack);
			yield return null;
		}
	}
	public void Left () {
		transform.Translate (speed * Vector3.forward);	
	}
	void OnTriggerEnter(Collider collider) {

		if(collider.gameObject.tag=="jump"){
			StartCoroutine (ShortJumpCoroutine ());
			Debug.Log("JUUUUUMP");

		}
		if(collider.gameObject.tag=="longjump"){
			StartCoroutine (LongJumpCoroutine ());
			Debug.Log("LOOONGJUUUUUMP");

		}
		if (collider.gameObject.tag == "squi") {
			Debug.Log ("SQUI EST MORT");
			MortTexte.text = "NANAR A TUE SQUI";

		}

	}


}
