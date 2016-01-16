using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	enum Direction {Up, Down, Left, Right};
	Direction myDir;

	public float speed;
	Vector3 move; Animator anim;
	public bool useDiagMovement = true;

	// Use this for initialization
	void Start () {
		myDir = Direction.Down;

		anim = GetComponent<Animator>();
		//anim.Play ("Player_IdleD", -1);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.UpArrow)) {
			myDir = Direction.Up;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			myDir = Direction.Down;
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			myDir = Direction.Left;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			myDir = Direction.Right;
		} else {
			if (!useDiagMovement){
				move = Vector3.zero;
			}
		}

		if (myDir == Direction.Down) {
			if (Input.GetKey (KeyCode.DownArrow)) {
				if (!useDiagMovement){ move = Vector3.down; }
				anim.Play ("Player_WalkD", -1);
			} else {
				anim.Play ("Player_IdleD", -1);
			}
		} else if (myDir == Direction.Up) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				if (!useDiagMovement){ move = Vector3.up; }
				anim.Play ("Player_WalkU", -1);
			} else {
				anim.Play ("Player_IdleU", -1);
			}
		} else if (myDir == Direction.Left) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				if (!useDiagMovement){ move = Vector3.left; }
				anim.Play ("Player_WalkL", -1);
			} else {
				anim.Play ("Player_IdleL", -1);
			}
		} else if (myDir == Direction.Right) {
			if (Input.GetKey (KeyCode.RightArrow)) {
				if (!useDiagMovement){ move = Vector3.right; }
				anim.Play ("Player_WalkR", -1);
			} else {
				anim.Play ("Player_IdleR", -1);
			}
		}

		if (useDiagMovement) { move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")); }
		transform.position += move * speed * Time.deltaTime;
	}

}
