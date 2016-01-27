using UnityEngine;
using System.Collections;

public class TrashScript : MonoBehaviour {
	string[] scrapItems = { "a TV", "a pocket watch", "a guitar", "Sonic's career", "a stop sign", "a teddy bear", "a wedding ring", "a cell phone", "a fancy pen" };
	GameObject myPlayer, gameManager;
	float playerDist;
	
	// Update is called once per frame
	void Update () {
		myPlayer = GameObject.FindGameObjectWithTag ("Player");
		gameManager = GameObject.Find ("GameManager");
		playerDist = Vector3.Distance (myPlayer.transform.position, transform.position);
		if (playerDist <= 95f) {
			if (Input.GetKeyUp(KeyCode.Space)){
				myPlayer.GetComponent<CollectingSystem> ().myElectricity -= 5;
				myPlayer.GetComponent<CollectingSystem> ().myMementos++;
				StartCoroutine(gameManager.GetComponent<GameController> ().collectMemento (scrapItems[Random.Range(0,scrapItems.Length)], this.gameObject));
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col2d){
		if (col2d.gameObject.tag == "Player") {
			transform.FindChild ("Interact Text").gameObject.SetActive (true);
		}
	}

	void OnCollisionExit2D(Collision2D col2d){
		if (col2d.gameObject.tag == "Player") {
			transform.FindChild ("Interact Text").gameObject.SetActive (false);
		}
	}
}
