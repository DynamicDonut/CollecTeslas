using UnityEngine;
using System.Collections;

public class CollectingSystem : MonoBehaviour {
	public int myElectricity = 0;
	public int myRubber = 0;

	void OnCollisionEnter2D(Collision2D col2d){
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
