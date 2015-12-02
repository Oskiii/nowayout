using UnityEngine;
using System.Collections;

public class LeoKick : MonoBehaviour {

	private Animator leoAnim;
	public bool kicking;
	public GameObject player;

	void Start (){
		leoAnim = GetComponentInParent<Animator> ();
	}


	void OnTriggerEnter(Collider col){
		leoAnim.SetBool("kicking", true);
		leoAnim.SetTrigger ("kick");
		movement.current.controllable = false;
		AudioPlayerScript.current.audioSource1.Stop ();
		AudioPlayerScript.current.audioSource2.Stop ();
		StartCoroutine("kick");
	}

	void Update(){
		
		//make leo face jaba
		transform.localScale = movement.current.gameObject.transform.localScale;


	}

	IEnumerator kick(){

		yield return new WaitForSeconds (leoAnim.GetCurrentAnimatorClipInfo (0).GetLength (0));


		player.transform.position = new Vector3 (8.8f * Mathf.Cos (Mathf.Atan2 (player.transform.position.z, player.transform.position.x)), 
		                                             gameObject.transform.position.y, 
		                                         8.8f * Mathf.Sin (Mathf.Atan2 (player.transform.position.z, player.transform.position.x)));

		//get kicked
		movement.current.tubeRadius = 8.8f;
		movement.current.controllable = true;
		movement.current.travelRate = 0.05f * transform.localScale.x;
		HeartScore.current.AddPrestige ();



		leoAnim.SetBool ("kicking", false);
		AudioPlayerScript.current.PlaySong (1);
		AudioPlayerScript.current.PlayEffect (1);

	}
}
