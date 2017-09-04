using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceScript : MonoBehaviour {

	public int[] joinableSides = new int[4];

	private int currRotation = 0;
	private int newRotation = 0;

	public bool positionFound = false;

	private bool rotatingInProcess = false;

	private float rotateTime = 20f;
	private float timeToTurnOnColliders = 0.2f;

	public bool mapNotVisible = true;

	// Use this for initialization
	void Start () {

		//Debug.Log("MapPiece Script START");

		// first random rotate
		RotateFast ();

		// turn on Colliders
		foreach (BoxCollider col in transform.GetComponentsInChildren<BoxCollider>()) {
			col.enabled = true;
		}

		// Rotate again by 2 if not in contact with other walkway collider
		StartCoroutine (placeMap (timeToTurnOnColliders));
	}



	// Update is called once per frame
	void Update () {

		if (rotatingInProcess) {
			Vector3 destination = new Vector3 (0, newRotation, 0);
			if (Vector3.Distance (transform.eulerAngles, destination) > 0.01f) {
				transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, destination, Time.deltaTime * rotateTime);
			} else {
				transform.eulerAngles = destination;
				rotatingInProcess = false;
			}
		}
	}

	public void RotateMapPieceVisually(int turns) {
		if (!positionFound) {
			for (int i = 0; i < turns; i++) {
				if (!positionFound) {
					rotatingInProcess = true;
					ChangeJoinableSides ();
				}
			}
		}
	}

	public void RotateFast() {
		int randNum = Random.Range (0, 4);
		Vector3 newVect;
		for (int i = 0; i < randNum; i++) {
			ChangeJoinableSides ();
			newVect = new Vector3 (transform.rotation.x, newRotation , transform.rotation.z);
			transform.eulerAngles = newVect;
		}
	}

	private void ChangeJoinableSides() {
		if (currRotation == 360) {
			currRotation = 0;
		}
		newRotation = currRotation + 90;
		//Debug.Log ("Rotated");
		int tempVar = joinableSides [1];
		joinableSides [1] = joinableSides [0];
		joinableSides [0] = joinableSides [3];
		joinableSides [3] = joinableSides [2];
		joinableSides [2] = tempVar;
		currRotation = newRotation;
	}
		

	IEnumerator placeMap(float secs) {

		yield return new WaitForSeconds (secs);

		if (!positionFound) {
			Debug.Log ("NO inital connection so spinnning");
			RotateMapPieceVisually (2);
		} else {
			Debug.Log ("Connection found on 1st spin");
		}

		yield return new WaitForSeconds (secs);

		// If no connections made lift board and delete
		if (!positionFound) {
			Debug.Log ("NO CONNECTIONS FOUND");

		}
	}

}
