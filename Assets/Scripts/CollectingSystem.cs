using UnityEngine;
using System.Collections;

public class CollectingSystem : MonoBehaviour {
	public int myElectricity = 0;
	public int myMementos = 0;
	GameObject gameManager, trashCollision;

	void Start () {
		gameManager	= GameObject.Find ("GameManager");
		trashCollision = GameObject.Find ("Trash Collision");
	}

    void Update() {
        if (myElectricity >= 20) {
            myElectricity = 20;
		} else if (myElectricity > 0) {
			if (this.gameObject.GetComponent<PlayerMovement> ().lowPower == true) {
				gameManager.GetComponent<MusicManager> ().swapMusic = true;
			}				
			this.gameObject.GetComponent<PlayerMovement> ().lowPower = false;
		} else if (myElectricity <= 0) {
            myElectricity = 0;
			if (this.gameObject.GetComponent<PlayerMovement> ().lowPower == false) {
				gameManager.GetComponent<MusicManager> ().swapMusic = true;
			}	
			this.gameObject.GetComponent<PlayerMovement> ().lowPower = true;
        }

        this.gameObject.GetComponent<PlayerUIManager>().pBarVal = myElectricity * 10.0f;
    }

	void OnCollisionEnter2D(Collision2D col2d){
		if (col2d.gameObject.tag == "Collectable") {
			if(col2d.gameObject.GetComponent<CollectableObject>().myColType == CollectableObject.CollectableType.Electricity){
				myElectricity += col2d.gameObject.GetComponent<CollectableObject>().val;
			}

			Destroy(col2d.gameObject);
		} 
	}
}
