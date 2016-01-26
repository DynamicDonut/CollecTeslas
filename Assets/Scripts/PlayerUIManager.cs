using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIManager : MonoBehaviour {
    GameObject myUI, myPowerBar;

    float pBarValMax = 200;

    [Range(0, 200)]
    public float pBarVal = 100;

	void Awake () {
        myUI = GameObject.Find("UI");
        myPowerBar = myUI.transform.FindChild("Power Bar").gameObject;
	}

	// Update is called once per frame
	void Update () {
        myPowerBar.GetComponent<Image>().fillAmount = pBarVal / pBarValMax;
	}
}
