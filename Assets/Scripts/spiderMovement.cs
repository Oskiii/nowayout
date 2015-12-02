using UnityEngine;
using System.Collections;

public class spiderMovement : MonoBehaviour {

	public Transform groundCheck1;
	public Transform groundCheck2;
	private bool facingRight = true;
	private float tubeRadius = 9.5f;
	public float spiderSpeed;

	void Start (){
		spiderSpeed = 0.001f;
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (groundCheck1.transform.position, 0.25f);
		Gizmos.DrawWireSphere (groundCheck2.transform.position, 0.25f);
	}

	void Update () {

		if (!IsGrounded (groundCheck1) && IsGrounded (groundCheck2)) {
			spiderSpeed = 0.001f;
		} else if (IsGrounded (groundCheck1) && !IsGrounded (groundCheck2)) {
			spiderSpeed = -0.001f;
		}

		if(IsGrounded(groundCheck1) != IsGrounded(groundCheck2))
			print (IsGrounded (groundCheck1) + " " + IsGrounded (groundCheck2));

	


		gameObject.transform.position = new Vector3 (tubeRadius * Mathf.Cos (Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x) + spiderSpeed), 
		                                             gameObject.transform.position.y, 
		                                             tubeRadius * Mathf.Sin (Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x) + spiderSpeed));
		
		gameObject.transform.rotation = Quaternion.Euler (0, -360 * Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x) / (2 * Mathf.PI) + 270, 0);

	}

	bool IsGrounded(Transform pos){
		RaycastHit hit;
		//Debug.DrawRay (pos.position, Vector3.down, Color.red);
		return Physics.SphereCast(pos.position, 0.55f, Vector3.down, out hit, 0.01f);
		
	}
	
}
