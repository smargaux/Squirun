using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.SceneManagement;

public class player : MonoBehaviour {
	private Rigidbody myRigidBody;
	private bool isJumping = false;
	public float distance2floor;
	private float speed;
	public float mass;
	public float maxSpeed;
	private IEnumerator jumpTime;
	public int countNuts=0;
	public float jumpForce;
	public float jumpForceBack;
	private float airTime;
	public float jumpCoroutineTime;
	public float airTimeCap;
	private bool growing;
	public Text ScoreText;
	private int incrementJump = 0;
	private ParticleSystem leafs;
	public AudioSource steps;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.2f;
		leafs = GetComponentInChildren<ParticleSystem> ();
		var em = leafs.emission;
		em.enabled = true;
		myRigidBody = GetComponent<Rigidbody> ();
		speed = maxSpeed;
		AnimationSqui.isrunning = true;
	}

	IEnumerator JumpCoroutine ()
	{
		float temps = jumpCoroutineTime;
		//speed = maxSpeed + 0.1f;
		myRigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse );
		yield return new WaitForSeconds(airTime);
		while (temps > 0f) {
			temps -= Time.deltaTime;
			myRigidBody.AddForce (-Vector3.up * jumpForceBack);
			yield return null;

		}
	}

	public void Left () {
		transform.Translate (speed * Vector3.right);	
	}
	void Update ()
	{
		if ( Input.GetKeyDown(KeyCode.Space) && !isJumping && incrementJump <= 1) {

			StartCoroutine(JumpCoroutine());
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			airTime = 0;
			incrementJump += 1;
		}
		if (Input.GetKey (KeyCode.Space)) {
			if(steps.isPlaying)
				steps.Stop ();
			airTime += Time.deltaTime;
		}
		Debug.Log ("AIR TIME " + airTime);
	}
	void FixedUpdate () {
		Debug.Log ("SPEED " + speed);
		if (airTime > airTimeCap)
			airTime = airTimeCap;

		Left ();
		/*float offset = transform.position.y;

		// Verifier si Squi touche un bloc (saut loupé)
		if (growing) {
			offset += Time.fixedDeltaTime;

		} else {
			offset -= Time.fixedDeltaTime;
		}
		if(offset <= transform.position.y) growing = true;
		else if(offset > transform.position.y+1f) growing = false;

		Vector3 squiPosition = transform.position;
		Debug.DrawRay ( transform.position, Vector3.right * 4f);

		Left ();
		// Raycast qui permet de définir à quelle distance du sol se trouve le joueur
		if (Physics.Raycast (squiPosition, Vector3.right, out hit)) {
			if(hit.distance<1f){
				//speed = 0;
			}

			//Debug.Log ("COLLIDER:"+hit.collider.name);
		}*/
		RaycastHit hit;

		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			// Si la distance est inférieur à 1, on peut sauter
			if (hit.distance < 1f) {
				incrementJump = 0;
				isJumping = false;
				StopAllCoroutines ();

				//speed = maxSpeed;
			}
			Debug.Log (hit.distance);
			if (hit.distance < 0.03f) {
				if(!steps.isPlaying)
				steps.Play();
				var em = leafs.emission;
				em.enabled = true;

			} else {
				//steps.Stop();
				var em = leafs.emission;
				em.enabled = false;
			}
		}

	}
	public void Boost(){
	}
	public void DestroyNut(){
	}
	private void Slide(){
	}
	void OnCollisionEnter(Collider collider)
	{
		if (collider.gameObject.tag == "road")
		{
			Debug.Log ("ROAD");
			isJumping = false;
			incrementJump = 0;

		}
	}
	void OnTriggerEnter(Collider collider) {

		if(collider.gameObject.tag=="nut"){
			countNuts+=1;
			speed += 0.01f;
			ParticleSystem nutExplosion;
			nutExplosion = collider.gameObject.GetComponentInChildren<ParticleSystem> ();
			ScoreText.text = countNuts.ToString();
			Destroy (collider.gameObject.GetComponentInChildren<MeshFilter>().gameObject);
			nutExplosion.Play ();
			collider.enabled = false;

		}
		if (collider.gameObject.name == "sol") {
			restartCurrentScene ();
		}
	}



void restartCurrentScene()
{
	int scene = SceneManager.GetActiveScene().buildIndex;
	SceneManager.LoadScene(scene, LoadSceneMode.Single);
}
}