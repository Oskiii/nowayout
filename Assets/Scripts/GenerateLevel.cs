using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLevel : MonoBehaviour {

	public GameObject tilePrefab;
	public float jumpHeight;
	public float distance;
	public float maxMapHeight;
	private float currentHeight = 1.1f;
	private float currentAngle = -1.3f;
	public float angleBetweenTiles;
	public float chanceOfUp;
	private bool changedDirection = true;
	private int count = 0;
	private bool jumped = false;
	private int start = 0;
	private float jumpPosition = 0f;
	public GameObject leo;
	private int totalCount = 0;
	public GameObject spike;
	public GameObject torch;
	public GameObject healthPickup;
	public GameObject vine;
	public GameObject spider;

	public static GenerateLevel current;

	void Awake(){
		current = this;
	}



	void Start () {

		PopulateMap ();
	}



	void addObject (GameObject gobj, float height = 0.55f, float depth = 9.5f) {

		GameObject inst = (GameObject) Instantiate (gobj, 
		                                            new Vector3(depth*Mathf.Cos (currentAngle-angleBetweenTiles), 
		            								currentHeight+height,
		            								depth*Mathf.Sin (currentAngle-angleBetweenTiles)), Quaternion.identity);
		
		inst.transform.rotation = Quaternion.Euler (0, -360*Mathf.Atan2 (inst.transform.position.z, inst.transform.position.x)/(2*Mathf.PI)+90, 0);

	}

	void AddTile () {
		GameObject tile = (GameObject) Instantiate (tilePrefab, 
		                                            new Vector3(GetComponent<movement>().tubeRadius*Mathf.Cos (currentAngle), 
		          						  			currentHeight,
		            								GetComponent<movement>().tubeRadius*Mathf.Sin (currentAngle)), Quaternion.identity);


		tile.transform.rotation = Quaternion.Euler (0, -360*Mathf.Atan2 (tile.transform.position.z, tile.transform.position.x)/(2*Mathf.PI)+90, 0);
		currentAngle += angleBetweenTiles;

		int randomType = Random.Range (0, 100);
		if (currentHeight > 79)
			randomType = 100;
		if (randomType < 6)
			tile.GetComponent<movingTile> ().moving = true;
		else if (randomType >= 6 && randomType < 15)
			addObject (spike, 0.35f);
		else if (randomType >= 15 && randomType < 16)
			addObject (healthPickup, 1.5f);

		if (randomType >= 15 && randomType < 30)
			addObject (torch, 1.5f, 9.95f);

		if (randomType >= 40 && randomType > 70)
			addObject (vine, -.4f, 8.98f);

	}

	void PopulateMap(){
		while (currentHeight < maxMapHeight) {

			int randomNum3 = Random.Range (0, 100);
			if (randomNum3 < chanceOfUp && jumped == true) {
				distance = -distance;
				angleBetweenTiles = -angleBetweenTiles;
				currentAngle = jumpPosition+distance+angleBetweenTiles;
				changedDirection = true;
			}


			int randomNum2 = Random.Range (0, 100);
			if (( randomNum2 < chanceOfUp || count > 3 || start == 1 || changedDirection == true) && start != 0) {
				currentHeight += jumpHeight;
				count = 0;
				jumped = true;
				jumpPosition = currentAngle;
			}
			else {
				jumped = false;
			}

			int randomNum1 = Random.Range (0, 100);
			
			AddTile ();
			if (randomNum1 > 10) {
				AddTile ();
				if (randomNum1 > 20) {
					AddTile ();
					if (randomNum1 > 40) {
						AddTile ();
						if (randomNum1 > 55) {
							AddTile ();
							if (randomNum1 > 75) {
								AddTile ();
							}
						}
					}
					
				}
				
			}
			if (currentHeight > 79) addObject (leo);
			if (HeartScore.current.CheckPrestige() == -1) {
				gameObject.transform.position = new Vector3(9.5f*Mathf.Cos (currentAngle-0.7f*angleBetweenTiles), 
			                                            currentHeight+2.5f,
			                                            9.5f*Mathf.Sin (currentAngle-0.7f*angleBetweenTiles));
			
				gameObject.transform.rotation = Quaternion.Euler (0, -360*Mathf.Atan2 (gameObject.transform.position.z, gameObject.transform.position.x)/(2*Mathf.PI)+90, 0);

			}

			currentAngle += distance;
			count += 1;
			start += 1;
			changedDirection = false;
			totalCount += 1;
		}
	}
	
	
	
}