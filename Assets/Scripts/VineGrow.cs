using UnityEngine;
using System.Collections;

public class VineGrow : MonoBehaviour {

	public GameObject vine;

	void Awake (){
		int i = Random.Range (0, 5);
		if (i == 2) {
			GameObject vineInst = (GameObject) Instantiate (vine, transform.position + Vector3.down, gameObject.transform.rotation);
			vineInst.transform.rotation = Quaternion.Euler (0, -360*Mathf.Atan2 (vineInst.transform.position.z, vineInst.transform.position.x)/(2*Mathf.PI)+90, 0);
		}
	}
}
