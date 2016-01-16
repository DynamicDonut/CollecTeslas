using UnityEngine;
using System.Collections;

public class CollectingSystem : MonoBehaviour {

	public int myElectricity = 0;
	public int myRubber = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollision2DEnter(Collision2D col2d){
		Debug.Log (col2d.gameObject);
		if (col2d.gameObject.tag == "Collectable") {
			if(col2d.gameObject.GetComponent<CollectableObject>().myColType == CollectableObject.CollectableType.Electricity){
				myElectricity += col2d.gameObject.GetComponent<CollectableObject>().val;
			} else if(col2d.gameObject.GetComponent<CollectableObject>().myColType == CollectableObject.CollectableType.Rubber){
				myRubber += col2d.gameObject.GetComponent<CollectableObject>().val;
			}

			Destroy(col2d.gameObject);
		}
	}
}
