using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed;
	Vector3 move;
	public Animator get;
	public bool faceUp;
	public bool faceDown;
	public bool faceLeft;
	public bool faceRight;
	bool buttonPress = false;
	public int lockPos = 0;

	private bool attacking = false;
	private bool jumping = false;


	private Transform player;
	public int enemyDamage = 1;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	void Start ()
	{
		get = GetComponent<Animator> ();
		get.Play ("Player_IdleD", -1);
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, lockPos, lockPos);
		if (jumping)
			(GameObject.FindGameObjectWithTag ("Player").GetComponent(typeof(CircleCollider2D)) as CircleCollider2D).isTrigger = true;
		else {
			(GameObject.FindGameObjectWithTag ("Player").GetComponent(typeof(CircleCollider2D)) as CircleCollider2D).isTrigger = false;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine (Attack());
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			StartCoroutine (Jump());
		}

		if (Input.GetKey (KeyCode.UpArrow) && !buttonPress) 
		{
			get.Play ("Player_WalkU", -1);
			move = Vector3.up;
			faceUp = true;
			faceDown = false;
			faceRight = false;
			faceLeft = false;
			buttonPress = true;
		} else if (faceUp && Input.GetKeyUp (KeyCode.UpArrow)) {
			get.Play ("Player_IdleU", -1);
			buttonPress = false;
		}
		
		if (Input.GetKey (KeyCode.DownArrow) && !buttonPress) {
			get.Play ("Player_WalkD", -1);
			move = Vector3.down;
			faceUp = false;
			faceDown = true;
			faceRight = false;
			faceLeft = false;
			buttonPress = true;

		} else if (faceDown && Input.GetKeyUp (KeyCode.DownArrow)) {
			get.Play ("Player_IdleD", -1);
			buttonPress = false;
		}
				

		if (Input.GetKey (KeyCode.LeftArrow) && !buttonPress) {
			get.Play ("Player_WalkL", -1);
			move = Vector3.left;
			faceUp = false;
			faceDown = false;
			faceRight = false;
			faceLeft = true;
			buttonPress = true;

		} else if (faceLeft && Input.GetKeyUp (KeyCode.LeftArrow)) {
			get.Play ("Player_IdleL", -1);	
			buttonPress = false;
		}
			
		if (Input.GetKey (KeyCode.RightArrow) && !buttonPress) {
			get.Play ("Player_WalkR", -1);
			move = Vector3.right;
			faceUp = false;
			faceDown = false;
			faceRight = true;
			faceLeft = false;
			buttonPress = true;
		} else if (faceRight && Input.GetKeyUp (KeyCode.RightArrow)) {
			get.Play ("Player_IdleR", -1);	
			buttonPress = false;
		}


		move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}
		

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name == "Pad1") 
		{
			player.position = GameObject.FindGameObjectWithTag ("Pad2").transform.position;

		}
		if (other.gameObject.tag == "Items") {
			if (attacking)
				Destroy (other.gameObject);
		}

	}

	IEnumerator Attack()
	{
		attacking = true;
		get.Play ("Player_Attack", -1);
		yield return new WaitForSeconds (0.5f);
		attacking = false;
		if (faceDown) {
			get.Play ("Player_IdleD", -1);
			buttonPress = false;
		} 
		else if (faceRight) 
		{
			get.Play ("Player_IdleR", -1);	
			buttonPress = false;
		}
		else if (faceLeft) 
		{
				get.Play ("Player_IdleL", -1);	
				buttonPress = false;
		}

		else if (faceUp) 
		{
				get.Play ("Player_IdleU", -1);
				buttonPress = false;
		}


	}


	IEnumerator Jump()
	{
		jumping = true;
		get.Play ("Player_Jump", -1);
		speed = speed + 10;
		yield return new WaitForSeconds (0.5f);
		jumping = false;
		speed = speed - 10;
		if (faceDown) {
			get.Play ("Player_IdleD", -1);
			buttonPress = false;
		} else if (faceRight) {
			get.Play ("Player_IdleR", -1);	
			buttonPress = false;
		} else if (faceLeft) {
			get.Play ("Player_IdleL", -1);	
			buttonPress = false;
		} else if (faceUp) {
			get.Play ("Player_IdleU", -1);
			buttonPress = false;
		}
	}
}
