using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed;

	Vector3 move;
	public Animator anim;
	public bool faceUp;
	public bool faceDown;
	public bool faceLeft;
	public bool faceRight;
	bool buttonPress = false;

	bool useDiagMovement = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.Play ("Player_IdleD", -1);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.UpArrow)) {
			anim.Play ("Player_WalkU", -1);
			move = Vector3.up;
			faceUp = true;
			faceDown = false;
			faceRight = false;
			faceLeft = false;
		} else if (faceUp) {
			anim.Play ("Player_IdleU", -1);
		}
		
		if (Input.GetKey (KeyCode.DownArrow))
		{
			anim.Play ("Player_WalkD", -1);
			move = Vector3.down;
			faceUp = false;
			faceDown = true;
			faceRight = false;
			faceLeft = false;

		}else if (faceDown) {
			anim.Play ("Player_IdleD", -1);
		}
				

		if (Input.GetKey (KeyCode.LeftArrow)) {
			anim.Play ("Player_WalkL", -1);
			move = Vector3.left;
			faceUp = false;
			faceDown = false;
			faceRight = false;
			faceLeft = true;
		} else if (faceLeft) {
			anim.Play ("Player_IdleL", -1);		
		}
			
		if (Input.GetKey (KeyCode.RightArrow)) {
			anim.Play ("Player_WalkR", -1);
			move = Vector3.right;
			faceUp = false;
			faceDown = false;
			faceRight = true;
			faceLeft = false;
		} else if (faceRight) {
			anim.Play ("Player_IdleR", -1);	
		}

		move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}
}
