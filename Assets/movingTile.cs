using UnityEngine;
using System.Collections;

public class movingTile : MonoBehaviour {

	// Use this for initialization
	public bool moving = false;
	

	// Update is called once per frame
	void FixedUpdate () {
		if (moving == true) {
			gameObject.transform.position = new Vector3((9.5f+0.4f+0.4f*Mathf.Sin (2*Time.time))*Mathf.Cos (Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x)), 
			                                            gameObject.transform.position.y,
			                                            (9.5f+0.4f+0.4f*Mathf.Sin (2*Time.time))*Mathf.Sin (Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x)));
		}
	}
}
