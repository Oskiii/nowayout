using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class movement : MonoBehaviour {

	public float travelRate;
	public float tubeRadius;
	public float jumpForce;
	private bool isGrounded;
	private Rigidbody rb;
	private float distToGround;
	public bool facingRight;
	private bool running;
	private Animator anim;
	public float maxFallSpeed;
	public GameObject leo;

	public bool controllable = true;
	private bool lastMovedRight = true;

	public GameObject screenFader;
	public GameObject heart;

	public static movement current;




	void Awake (){
		current = this;
		Application.targetFrameRate = 60;

		//transform.position = GameObject.Find("bigLeo(Clone)").transform.position;
	}

	void Start (){
		rb = gameObject.GetComponent<Rigidbody> ();
		anim = gameObject.GetComponent<Animator> ();
		running = false;

		//get distance to ground at start
		distToGround = gameObject.GetComponent<BoxCollider> ().bounds.extents.y;

		controllable = true;


		facingRight = true;


	}

	void OnLevelWasLoaded(){
		screenFader.SetActive (true);
		if (HeartScore.current.CheckPrestige () < 1) {
			AudioPlayerScript.current.PlaySong (0);
		} else {
			AudioPlayerScript.current.PlaySong (2);
		}
		StartCoroutine ("FadeIn");
	}

	void FixedUpdate () {

		rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);

		//get rate of movement from input
		if (controllable) {
		
			travelRate = -Input.GetAxisRaw ("Horizontal") / 100;
			//move player along side of tube
			gameObject.transform.position = new Vector3 (tubeRadius * Mathf.Cos (Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x) + travelRate), 
		                                            gameObject.transform.position.y, 
		                                            tubeRadius * Mathf.Sin (Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x) + travelRate));

			gameObject.transform.rotation = Quaternion.Euler (0, -360 * Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x) / (2 * Mathf.PI) + 270, 0);

			//if player is grounded and presses up
			isGrounded = IsGrounded ();

			if (Input.GetAxisRaw ("Vertical") > 0.01 && isGrounded && rb.velocity.y < 0.1f) {
				isGrounded = false;
				//jump
				rb.AddForce (new Vector3 (0f, jumpForce, 0f));
			}
		}

		if (rb.velocity.y < -maxFallSpeed)
			rb.velocity = new Vector3 (rb.velocity.x, -maxFallSpeed, rb.velocity.z);


		
	}


	void Update (){
		if (isGrounded && tubeRadius != 9.5f) {
			//dude lands from falling down


			StartCoroutine("FadeOut");
		}


		if (controllable) {
			if ((Input.GetAxisRaw ("Horizontal") > 0.01f && !facingRight) || (Input.GetAxisRaw ("Horizontal") < -0.01f && facingRight)) {
				Flip ();
			
			}


			if ((Mathf.Abs (Input.GetAxisRaw ("Horizontal")) > 0.01f)) {
				running = true;
			} else {
				running = false;
			}
			anim.SetBool ("running", running);
		} else {
			anim.SetBool ("running", false);
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "healthPickup") {
			HeartScore.current.AddHealth();
			AudioPlayerScript.current.PlayEffect(0);
			Destroy(col.gameObject);
		}
	}

	void OnTriggerStay(Collider col){
		if (col.tag == "enemy") {

			HeartScore.current.takeDamage();
		}
			
	}

	//is there ground below the player?
	bool IsGrounded(){
		Debug.DrawRay (transform.position, Vector3.down, Color.red);
		return Physics.Linecast(transform.position - new Vector3(0f, 0.2f, 0f), transform.position - new Vector3(0f, distToGround + 0.0001f, 0f));

	}

	// Flips the character
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator FadeOut() {

		if (screenFader.GetComponent<Renderer> ().material.color.a != 0) {
			Color c = screenFader.GetComponent<Renderer>().material.color;
			c.a = 0;
			screenFader.GetComponent<Renderer>().material.color = c;
		}

		for (float f = 0f; f <= 1; f += 0.05f) {
			Color c = screenFader.GetComponent<Renderer>().material.color;
			c.a = f;
			screenFader.GetComponent<Renderer>().material.color = c;
			yield return null;
		}

		tubeRadius = 9.5f;
		HeartScore.current.ResetScore ();



	}

	IEnumerator FadeIn(){

		if (screenFader.GetComponent<Renderer> ().material.color.a != 1) {
			Color c = screenFader.GetComponent<Renderer>().material.color;
			c.a = 1;
			screenFader.GetComponent<Renderer>().material.color = c;
		}

		for (float f = 1f; f >= 0; f -= 0.05f) {
			Color c = screenFader.GetComponent<Renderer> ().material.color;
			c.a = f;
			screenFader.GetComponent<Renderer> ().material.color = c;
			yield return null;
		}
	}


}
