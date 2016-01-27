using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {
	GameObject myUI;
	Text collText, collText_under;
	Color examColorA, examColorB;

	public Texture2D fadeOutTexture;
	public float fadeSpd = 0.8f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1;  // -1 = Fade in, 1 = Fade Out

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}

	void Start () {
		myUI = GameObject.Find ("UI");

		collText_under = myUI.transform.FindChild ("CollectText (Underlay)").GetComponent<Text> ();
		collText = collText_under.transform.FindChild ("CollectText").GetComponent<Text> ();

		examColorA = Color.white; examColorB = Color.black;
		examColorA.a = 0f; examColorB.a = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "StartScreen") {
			if (Input.GetKeyUp (KeyCode.Space)) {
				StartCoroutine (swapScene ("MainLevel"));
			}
		} else if (SceneManager.GetActiveScene ().name == "EndScreen") {
			if (Input.GetKeyUp (KeyCode.Space)) {
				StartCoroutine (swapScene ("StartScreen"));
			}
		}
	}

	void OnGUI(){
		//fade out/in alpha using a direction, a speed, and deltaTime to convert to seconds.
		alpha += fadeDir * fadeSpd * Time.deltaTime;
		//force alpha between 0 and 1 so it works with GUI.color
		alpha = Mathf.Clamp01 (alpha);
		//set color to current color but change alpha, set draw depth to the last, and draw our texture to the entire screen area.
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade (int dir){
		fadeDir = dir;
		return (fadeSpd);  //used to easier time Application.LoadLevel();
	}

	IEnumerator swapScene (string sceneName){
		float fadeTime = BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene (sceneName);
	}

	void OnLevelWasLoaded(){
		BeginFade (-1);
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
