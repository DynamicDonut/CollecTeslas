﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	enum Direction {Up, Down, Left, Right};
	Direction myDir;

    public bool useDiagMovement = true;
    public bool canMove;

    public float speed;
	public int lockPos = 0;
	//bool buttonPress = false;
	private bool attacking = false;
	private bool jumping = false;
	public bool lowPower = false;

	BoxCollider2D atkRange;
	CircleCollider2D hurtBox;
    Vector3 move; Animator anim;

    private Transform myPlayer;
	public int enemyDamage = 1;

	void Awake(){
		myPlayer = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Use this for initialization
	void Start () {
		myDir = Direction.Down;
		canMove = true;

		atkRange = GetComponent<BoxCollider2D> ();
		hurtBox = GetComponent<CircleCollider2D> ();
		anim = GetComponent<Animator>();
		//anim.Play ("Player_IdleD", -1);
		atkRange.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, lockPos, lockPos);
		if (jumping) { 
			hurtBox.isTrigger = true;
		} else {
			hurtBox.isTrigger = false;
		}

		/*
		 * Temporarily commenting this out until I can figure out how to fix the animation issue.
		 * 
		if (Input.GetKeyDown (KeyCode.X)) {
			StartCoroutine ("Attack");
		}
		
		if (Input.GetKeyDown (KeyCode.Z)) {
			StartCoroutine ("Jump");
		}
		*/

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
				anim.Play ("Walk_D", -1);
			} else {
				anim.Play ("Idle_D", -1);
			}
		} else if (myDir == Direction.Up) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				if (!useDiagMovement){ move = Vector3.up; }
				anim.Play ("Walk_U", -1);
			} else {
				anim.Play ("Idle_U", -1);
			}
		} else if (myDir == Direction.Left) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				if (!useDiagMovement){ move = Vector3.left; }
				anim.Play ("Walk_L", -1);
			} else {
				anim.Play ("Idle_L", -1);
			}
		} else if (myDir == Direction.Right) {
			if (Input.GetKey (KeyCode.RightArrow)) {
				if (!useDiagMovement){ move = Vector3.right; }
				anim.Play ("Walk_R", -1);
			} else {
				anim.Play ("Idle_R", -1);
			}
		}

		if (useDiagMovement) { move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")); }
		transform.position += move * speed * Time.deltaTime;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.name == "Enter") {
			myPlayer.position = GameObject.Find ("Exit").transform.position;
		} else if (other.gameObject.tag == "Items") {
			if (attacking) {
				Destroy (other.gameObject);
			}
		}
	}
	
	IEnumerator Attack() {
		attacking = true;
		anim.Play ("Player_Attack", -1);
		yield return new WaitForSeconds (2f);
		attacking = false;
		if (myDir == Direction.Down) {
			anim.Play ("Idle_D", -1);
			//buttonPress = false;
		} else if (myDir == Direction.Right) {
			anim.Play ("Idle_R", -1);	
			//buttonPress = false;
		} else if (myDir == Direction.Left) {
			anim.Play ("Idle_L", -1);	
			//buttonPress = false;
		} else if (myDir == Direction.Up) {
			anim.Play ("Idle_U", -1);
			//buttonPress = false;
		}
	}

	IEnumerator Jump() {
		jumping = true;
		anim.Play ("Player_Jump", -1);
		//speed += 10;
		yield return new WaitForSeconds (10f);
		jumping = false;
		//speed -= 10;
		if (myDir == Direction.Down) {
			anim.Play ("Idle_D", -1);
			//buttonPress = false;
		} else if (myDir == Direction.Right) {
			anim.Play ("Idle_R", -1);	
			//buttonPress = false;
		} else if (myDir == Direction.Left) {
			anim.Play ("Idle_L", -1);	
			//buttonPress = false;
		} else if (myDir == Direction.Up) {
			anim.Play ("Idle_U", -1);
			//buttonPress = false;
		}
	}
}
