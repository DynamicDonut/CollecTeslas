using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerUIManager : MonoBehaviour {
    GameObject myUI, myPowerBar, myMementos, gameManager;

    float pBarValMax = 200;

    [Range(0, 200)]
    public float pBarVal = 100;

	void Awake () {
        myUI = GameObject.Find("UI");
        myPowerBar = myUI.transform.FindChild("Power Bar").gameObject;
		myMementos = myUI.transform.FindChild("Mementos (Underlay)").gameObject;
		gameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void Update () {
        myPowerBar.GetComponent<Image>().fillAmount = pBarVal / pBarValMax;
		myMementos.GetComponent<Text> ().text = "Mementos: " + this.gameObject.GetComponent<CollectingSystem> ().myMementos + "/5";
		myMementos.transform.FindChild("Mementos").GetComponent<Text>().text = "Mementos: " + this.gameObject.GetComponent<CollectingSystem> ().myMementos + "/5";

		if (this.gameObject.GetComponent<CollectingSystem> ().myElectricity == 0 && this.gameObject.GetComponent<CollectingSystem> ().myMementos == 5) {
			GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Player").GetComponent<CollectingSystem>().sfxSound[1]);
			StartCoroutine (endGame());
		}
	}

	IEnumerator endGame(){
		float fadeTime = gameManager.GetComponent<GameController> ().BeginFade (1);
		Destroy (myUI);
		gameManager.GetComponent<MusicManager> ().myMusic.clip = gameManager.GetComponent<MusicManager> ().musicLib [0];
		gameManager.GetComponent<MusicManager> ().myMusic.Play ();
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("EndScreen");
	}
}
