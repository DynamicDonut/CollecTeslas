using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	GameObject myUI, myPlayer;
	Text collText, collText_under;
	Color examColorA, examColorB;

	// Use this for initialization
	void Start () {
		myUI = GameObject.Find ("UI");
		myPlayer = GameObject.FindGameObjectWithTag ("Player");

		collText_under = myUI.transform.FindChild ("CollectText (Underlay)").GetComponent<Text> ();
		collText = collText_under.transform.FindChild ("CollectText").GetComponent<Text> ();

		examColorA = Color.white; examColorB = Color.black;
		examColorA.a = 0f; examColorB.a = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator collectMemento (string mem_Name, GameObject source){
		collText.text = "You found " + mem_Name + ".";
		collText_under.text = "You found " + mem_Name + ".";

		while (collText.color.a < 1 && collText_under.color.a < 1) {
			examColorA.a += 0.83f;
			examColorB.a += 0.83f;
			collText.color = examColorA;
			collText_under.color = examColorB;
		}

		yield return new WaitForSeconds (3);

		while (collText.color.a > 0 && collText_under.color.a > 0) {
			examColorA.a -= 0.83f;
			examColorB.a -= 0.83f;
			collText.color = examColorA;
			collText_under.color = examColorB;
		}

		Destroy (source);
	}
}
