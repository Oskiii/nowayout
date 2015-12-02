using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartScore : MonoBehaviour {

	public int heartAmount;
	public GameObject heart;
	public Text healthText;
	public Text prestigeText;
	public Text progressText;
	public GameObject deathPanel;
	public Text finalPrestigeText;
	public Text finalProgressText;
	public static int health = 5;
	public static int prestige = -1;
	public int progress;
	private float damageTime;
	public GameObject player;

	public static HeartScore current;

	private List<GameObject> heartList = new List<GameObject> ();

	void Awake(){
		current = this;
		DontDestroyOnLoad (gameObject);
	}

	public int CheckPrestige (){
		return prestige;
	}

	public void takeDamage() {
		if (damageTime > 2f) {
			damageTime = 0f;

			if(health != 0){
				//take damage
				health -= 1;
				AudioPlayerScript.current.PlayEffect(2);
			}

			
		}
		if (health < 1) {
			//display death panel
			movement.current.controllable = false;
			deathPanel.SetActive(true);
			finalPrestigeText.text = "Prestige: " + prestige;
			finalProgressText.text = "Progress: " + progress + "%";
			
			
		}

	}

	public void AddHealth (){
		if (health < 5) {
			health += 1;
		}
	}

	void Start (){
		heartAmount = health;


		while (heartList.Count < health) {
			AddHeart ();
		}
	





		damageTime = 3f;

		progress = 0;

		progressText.text = "Progress: " + progress + "%";
		prestigeText.text = "Prestige: " + prestige;


	}

	public void ResetScore(){
		Application.LoadLevel("testscene");
	}

	public void HardReset(){
		health = 5;
		prestige = 0;

		Application.LoadLevel("testscene");
	}

	void Update () {

		damageTime += Time.deltaTime;
		progress = (int) ((player.transform.position.y / 80) * 100);

		if (progress > 100)
			progress = 100;
		progressText.text = "Progress: " + progress + "%";

		if(heartList.Count > health){
			Destroy(heartList[0]);
			heartList.RemoveAt(0);
		}

		if (heartList.Count < health) {
			AddHeart ();
		}

	}

	public void AddPrestige(){
		prestige += 1;
		prestigeText.text = "Prestige: " + prestige;
	}

	public void AddHeart(){

		GameObject heartInst = (GameObject)Instantiate (heart);
		heartInst.transform.SetParent(transform);
		heartList.Add (heartInst);

	}
}
