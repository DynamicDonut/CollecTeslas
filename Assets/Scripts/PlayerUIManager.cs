using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerUIManager : MonoBehaviour {
    GameObject myUI, myPowerBar, gameManager;

    float pBarValMax = 200;

    [Range(0, 200)]
    public float pBarVal = 100;

	void Awake () {
        myUI = GameObject.Find("UI");
        myPowerBar = myUI.transform.FindChild("Power Bar").gameObject;
		gameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void Update () {
        myPowerBar.GetComponent<Image>().fillAmount = pBarVal / pBarValMax;

		if (this.gameObject.GetComponent<CollectingSystem> ().myElectricity == 0 && this.gameObject.GetComponent<CollectingSystem> ().myMementos == 5) {
			StartCoroutine (endGame());
		}
	}

	IEnumerator endGame(){
		float fadeTime = gameManager.GetComponent<GameController> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("EndScreen");
	}
}
