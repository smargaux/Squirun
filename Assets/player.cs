using UnityEngine;
using System.Collections;
public class player : MonoBehaviour {
	private Rigidbody myRigidBody;
	private bool isJumping = false;
	public float distance2floor;
	public float speed;
	public float mass;
	private IEnumerator jumpTime;
	public int countNuts=0;
	public float jumpForce;
	public float jumpForceBack;
	private float airTime;
	public float jumpCoroutineTime=0.25f;
	public float airTimeCap;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.2f;
		myRigidBody = GetComponent<Rigidbody> ();
		speed = 4f;
		AnimationSqui.isrunning = true;
	}
	public void Jump () {
		myRigidBody.AddForce (Vector3.right * 100f );
		myRigidBody.AddForce (Vector3.up * 50f );
	}
	IEnumerator JumpCoroutine ()
	{
		float temps = jumpCoroutineTime;
		speed /= 2;
		myRigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse );
		yield return new WaitForSeconds(airTime);
		while (temps > 0f) {
			temps -= Time.deltaTime;
			myRigidBody.AddForce (-Vector3.up * jumpForceBack);
			yield return null;
		}
		speed *= 2;
	}

	public void Left () {
		myRigidBody.AddForce (speed * Vector3.right);
	}
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
			StartCoroutine(JumpCoroutine());
		}
		if(Input.GetKeyUp(KeyCode.Space)) airTime = 0;
		if (Input.GetKey (KeyCode.Space)) {
			airTime += Time.deltaTime;
		}
	}
	void FixedUpdate () {
		Debug.Log (transform.position);
		Debug.DrawRay (transform.position, new Vector3(-1f, 0f, 0f) * 8f);
		if (airTime > airTimeCap)
			airTime = airTimeCap;

		Left ();
		// Raycast qui permet de définir à quelle distance du sol se trouve le joueur
		RaycastHit hit;
		Vector3 forward = new Vector3(-1f, 0.25f, -1f);
		if (Physics.Raycast (transform.position, forward, out hit)) {
			Debug.Log("Found an object - distance: " + hit.distance);
			//speed = 0;
			//Debug.Log ("COLLIDER:"+hit.collider.name);
		}
		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			// Si la distance est inférieur à 2, on peut sauter
			if (hit.distance > 2f)
				isJumping = true;
			else
				isJumping = false;
		}

	}
	public void Boost(){
	}
	public void DestroyNut(){
	}
	private void Slide(){
	}
	void OnCollisionEnter(Collider collider){
		Debug.Log (collider.gameObject.name);
		StopAllCoroutines ();
	}
	void OnTriggerEnter(Collider collider) {
		var objettouche =collider.gameObject;

		if(objettouche.tag=="nut"){
			Debug.Log("NOISETTE");
			countNuts++;
		}
	}
}