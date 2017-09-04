using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollidersScript : MonoBehaviour {

	MapPieceScript mapPieceScript;

	// Use this for initialization
	void Start () {
		mapPieceScript = GetComponentInParent<MapPieceScript> ();
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "MapEdgeCollider") {
			Debug.Log ("WE have found a spot" + this.gameObject.name);
			if (this.gameObject.transform.parent.transform.parent.gameObject.tag != "Entrance") {
				mapPieceScript.positionFound = true;
			}
			this.gameObject.SetActive (false);
		}
	}

}
