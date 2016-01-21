using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {
	public enum CollectableType { Electricity, Rubber, Memento };
	public CollectableType myColType = CollectableType.Electricity;

	[Range(1f, 10f)]
	public int val = 1;
	public Sprite mySprite;
	//public Sprite[] defaultSprites;

	void Awake(){
		if (mySprite != null) {
			GetComponent<SpriteRenderer> ().sprite = mySprite;
		}
	}

	void Start () {
		SpriteRenderer myRend = GetComponent<SpriteRenderer> ();
		if (mySprite == null) {
			if (myColType == CollectableType.Rubber) {
				myRend.color = Color.red;
			} else if (myColType == CollectableType.Memento) {
				myRend.color = Color.blue;
			} else {
				myRend.color = Color.white;
			}
		}
	}
}
