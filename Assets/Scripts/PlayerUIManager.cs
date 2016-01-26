using UnityEngine;
using System.Collections;

public class PlayerUIManager : MonoBehaviour {
    GameObject myUI, myPowerBar;

    int pBarValMax = 200;
    Vector2 pBarv2;
    [Range(0, 200)]
    public int pBarVal = 100;
	void Awake () {
        myUI = GameObject.Find("UI");
        myPowerBar = myUI.transform.FindChild("Power Bar").gameObject;
	}

    void Start() {
        pBarVal = pBarValMax / 2;
        pBarv2 = myPowerBar.GetComponent<RectTransform>().sizeDelta;
    }

	// Update is called once per frame
	void Update () {
        pBarv2.x = pBarVal;
        myPowerBar.GetComponent<RectTransform>().sizeDelta = pBarv2;
	}
}
