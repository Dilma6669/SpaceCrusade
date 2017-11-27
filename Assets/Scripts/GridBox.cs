using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox : MonoBehaviour {

	GameManager gameManager;
	GridManager gridManager;

	public int gridLocX;
	public int gridLocZ;
	public int gridLocY;

	public int cubeUniqueID;

	public List<GameObject> neighbours = new List<GameObject>();
	public List<GameObject> legalNeighbours = new List<GameObject>();
	public int[] neighbourIDs = new int[27]; // contains cubeUniqueIDS or 0 if cube doesnt exist in neighbours

	public bool linked = false;
	public bool walkable;
	public bool climbable;


	public GameObject cubeObjectRef;
	public DefaultCubeScript cubeScript;
	//public bool[] panelPiecesRef;


	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		gridManager = FindObjectOfType<GridManager> ();

		//neighbourIDs = new int[27]; // contains cubeUniqueIDS or 0 if cube doesnt exist in neighbours
	}

	void Update() {

		// Sending raycasts to neighbours
		if (linked) {
			//linked = false;
			SendRayCastToLegalNeighbours();
		}
	}


	public void SendRayCastToLegalNeighbours() {
		Vector3 rayDirection;
		foreach (GameObject child in legalNeighbours) {
			rayDirection = child.transform.position - transform.position;
			Debug.DrawRay (transform.position, rayDirection, Color.cyan);
		}
	}



	public void GetNeighbourConnectionsOnStartUp() {

		//panelPiecesRef = cubeObjectRef.GetComponent<DefaultCubeScript>().panelPieces;
		//		for (int i = 0; i < panelPiecesRef.Length; i++) {
		//			Debug.Log ("panelPieces: " + panelPiecesRef [i] + " i: " + i);
		//		}

		int neighbourCount = 0;

		CheckNeighbourConnection (new Vector3 (-1, -1, -1), neighbourCount); // bottom, back, left // Extra param is neighbour num (same order as grid)
		CheckNeighbourConnection (new Vector3 (0, -1, -1), neighbourCount++); // bottom, Back, middle
		CheckNeighbourConnection (new Vector3 (1, -1, -1), neighbourCount++); // bottom, back, right
		CheckNeighbourConnection (new Vector3 (-1, 0, -1), neighbourCount++); // bottom, Left, middle
		CheckNeighbourConnection (new Vector3 (0, 0, -1), neighbourCount++); // bottom, straight, down
		CheckNeighbourConnection (new Vector3 (1, 0, -1), neighbourCount++); // bottom, Right, middle
		CheckNeighbourConnection (new Vector3 (-1, 1, -1), neighbourCount++); // bottom, front, left
		CheckNeighbourConnection (new Vector3 (0, 1, -1), neighbourCount++); // bottom, Front, middle
		CheckNeighbourConnection (new Vector3 (1, 1, -1), neighbourCount++); // bottom, front, right

		/////////
		CheckNeighbourConnection (new Vector3 (-1, -1, 0), neighbourCount++); //Middle, back, left
		CheckNeighbourConnection (new Vector3 (0, -1, 0), neighbourCount++); //Middle, straight, back
		CheckNeighbourConnection (new Vector3 (1, -1, 0), neighbourCount++); // Middle, back, right
		CheckNeighbourConnection (new Vector3 (-1,  0, 0), neighbourCount++); // Middle, straight, left // Extra param is neighbour num (same order as grid)

		CheckNeighbourConnection (new Vector3 (0, 0, 0), neighbourCount); // Oiginal Node

		CheckNeighbourConnection (new Vector3 (1, 0, 0), neighbourCount++); // Middle, straight, Right
		CheckNeighbourConnection (new Vector3 (-1, 1, 0), neighbourCount++); // Middle, front, left
		CheckNeighbourConnection (new Vector3 (0, 1, 0), neighbourCount++); //Middle, straight, front
		CheckNeighbourConnection (new Vector3 (1, 1, 0), neighbourCount++); //Middle, front, right
		//////////

		CheckNeighbourConnection (new Vector3 (-1, -1, 1), neighbourCount++); // Top, back, left // Extra param is neighbour num (same order as grid)
		CheckNeighbourConnection (new Vector3 (0, -1, 1), neighbourCount++); // Top, Back, middle
		CheckNeighbourConnection (new Vector3 (1, -1, 1), neighbourCount++); // Top, back, right
		CheckNeighbourConnection (new Vector3 (-1, 0, 1), neighbourCount++); // Top, Left, middle
		CheckNeighbourConnection (new Vector3 (0, 0, 1), neighbourCount++); // Top, straight, Up
		CheckNeighbourConnection (new Vector3 (1, 0, 1), neighbourCount++); // Top, Right, middle
		CheckNeighbourConnection (new Vector3 (-1, 1, 1), neighbourCount++); // Top, front, left
		CheckNeighbourConnection (new Vector3 (0, 1, 1), neighbourCount++); // Top, Front, middle
		CheckNeighbourConnection (new Vector3 (1, 1, 1), neighbourCount++); // Top, front, right
	}


	private void CheckNeighbourConnection(Vector3 coords, int neighNum) {

		Vector3 ownGridLoc = new Vector3 (gridLocX, gridLocZ, gridLocY);
		Vector3 otherGridObjLoc = new Vector3 (gridLocX + coords.x, gridLocZ + coords.y, gridLocY + coords.z);
		GameObject otherGridObj;

		if (gridManager.GridLocToGridObjLookup [otherGridObjLoc] != null) {
			otherGridObj = (GameObject)gridManager.GridLocToGridObjLookup [otherGridObjLoc];
			neighbourIDs[neighNum] = otherGridObj.transform.GetComponent<GridBox> ().cubeUniqueID;
			neighbours.Add (otherGridObj);
		} else {
			neighbourIDs [neighNum] = 0;
			neighbours.Add (null);
		}
	}

	public bool CheckGridClimbable() {
		if (cubeScript.panelPieces [1]) {
			//Debug.Log ("1");
			climbable = true;
			return true;
		} else if (cubeScript.panelPieces [2]) {
			//Debug.Log ("2");
			climbable = true;
			return true;
		} else if (cubeScript.panelPieces [3]) {
			//Debug.Log ("3");
			climbable = true;
			return true;
		} else if (cubeScript.panelPieces [4]) {
			//Debug.Log ("4");
			climbable = true;
			return true;
		} else if (cubeScript.panelPieces [5]) {
			//Debug.Log ("4");
			climbable = true;
			return true;
		} else if (neighbours [12] && neighbours [12].GetComponent<GridBox> ().cubeScript.panelPieces [3]) {
			//Debug.Log ("5");
			climbable = true;
			return true;
		} else if (neighbours [16] && neighbours [16].GetComponent<GridBox> ().cubeScript.panelPieces [4]) {
			//Debug.Log ("6");
			climbable = true;
			return true;
		} else if (neighbours [14] && neighbours [14].GetComponent<GridBox> ().cubeScript.panelPieces [1]) {
			//Debug.Log ("7");
			climbable = true;
			return true;
		} else if (neighbours [10] && neighbours [10].GetComponent<GridBox> ().cubeScript.panelPieces [2]) {
			//Debug.Log ("8");
			climbable = true;
			return true;
		} else if (neighbours [22] && neighbours [22].GetComponent<GridBox> ().cubeScript.panelPieces [1]) {
			//Debug.Log ("9");
			climbable = true;
			return true;
		} else {
			//Debug.Log ("10");
			climbable = false;
			return false;
		}
		return false;
	}

	public bool CheckGridWalkable() {
		if (cubeScript.panelPieces [0]) {
			//Debug.Log ("1");
			walkable = true;
			return true;
		} else if (neighbours [4] && neighbours [4].GetComponent<GridBox> ().cubeScript.panelPieces [5]) {
			//Debug.Log ("2");
			walkable = true;
			return true;
		} else {
			//Debug.Log ("3");
			walkable = false;
			return false;
		}
		return false;
	}

	public void SetLegalNeighbours() {

		// Get legal neighbours
		int count = 0;
		foreach (GameObject neighbour in neighbours) {
			if (neighbour != null) {
				if (gameManager.player.GetComponent<TestPlayerScript> ().playerStats ["C"] == 1) {
					if (neighbour.GetComponent<GridBox>().CheckGridClimbable () && CheckObstructingPanelsInNeighbours(count)) {
						//Debug.Log ("CheckGridClimbable: " + count);
						legalNeighbours.Add (neighbour);
					} else if (neighbour.GetComponent<GridBox>().CheckGridWalkable() && CheckObstructingPanelsInNeighbours(count)) {
						//Debug.Log ("CheckGridWalkable: " + count);
						legalNeighbours.Add (neighbour);
					}
				} else {
					if (neighbour.GetComponent<GridBox>().CheckGridWalkable () && CheckObstructingPanelsInNeighbours(count)) {
						legalNeighbours.Add (neighbour);
					}
				}

//				int neighUniqueID = neighbour.GetComponent<GridBox> ().cubeUniqueID;
//				if (CheckLegalNeighbours (count)) {
//					legalNeighbours.Add (neighbour);
//				}
			}
			count++;
		}
	}


	public bool CheckObstructingPanelsInNeighbours(int neighcount) {

		//int neighUniqueID = neighbour.GetComponent<DefaultCubeScript> ().cubeUniqueID;
		int thisCubeID = cubeUniqueID;
		//int neighCubeID = neighUniqueID;

		switch (neighcount) {
		case 0:
//			if (cubeScript.panelPieces [1]) { // if left middle wall panel present cannot move down back left
//				return false;}
//			if (cubeScript.panelPieces [4]) { // if back middle wall panel present cannot move down back left
//				return false;}
//			if (neighbours [9].GetComponent<GridBox> ().cubeScript.panelPieces [0]) { // if back left middle ground panel present cannot move down back left
//				return false;}
			break;
		case 1:
//			if (cubeScript.panelPieces [4]) { 
//				return false;}
//			if (neighbours [10].GetComponent<GridBox> ().cubeScript.panelPieces [0]) {
//				return false;}
			break;
		case 2:
//			if (!cubeScript.panelPieces [0]) { 
//				return false;}
//			if (cubeScript.panelPieces [3]) { 
//				return false;}
//			if (cubeScript.panelPieces [4]) {
//				return false;}
//			if (neighbours [11].GetComponent<GridBox> ().cubeScript.panelPieces [0]) { 
//				return false;}
			break;
		case 3:
//			if (cubeScript.panelPieces [1]) { 
//				return false;}
//			if (neighbours [12].GetComponent<GridBox> ().cubeScript.panelPieces [0]) { 
//				return false;}
			break;
		case 4:
//			if (cubeScript.panelPieces [0]) { 
//				return false;}
			break;
		case 5:
//			if (!cubeScript.panelPieces [0]) { 
//				return false;}
//			if (cubeScript.panelPieces [3]) { 
//				return false;}
//			if (neighbours [14].GetComponent<GridBox> ().cubeScript.panelPieces [0]) { 
//				return false;}
			break;
		case 6:
//			if (cubeScript.panelPieces [1]) { 
//				return false;}
//			if (cubeScript.panelPieces [2]) { 
//				return false;}
//			if (neighbours [15].GetComponent<GridBox> ().cubeScript.panelPieces [0]) {
//				return false;}
			break;
		case 7:
//			if (cubeScript.panelPieces [2]) {
//				return false;}
//			if (neighbours [16].GetComponent<GridBox> ().cubeScript.panelPieces [0]) {
//				return false;}
//			if (!neighbours [7].GetComponent<GridBox> ().cubeScript.panelPieces [1]) { // if left wall in neighbour 7 missing cant go there
//				return false;}
			break;
		case 8:
//			if (!cubeScript.panelPieces [0]) { 
//				return false;}
//			if (cubeScript.panelPieces [2]) {
//				return false;}
//			if (cubeScript.panelPieces [3]) {
//				return false;}
//			if (neighbours [17].GetComponent<GridBox> ().cubeScript.panelPieces [0]) {
//				return false;}
			break;
		case 9:
//			if (neighbours [10].GetComponent<GridBox> ().cubeScript.panelPieces [1]) {
//				return false;}
			break;
		case 10:
			break;
		case 11:
			break;
		case 12:
//			if (cubeScript.panelPieces [1]) {
//				return false;}
//			if (neighbours [12] && neighbours [12].GetComponent<GridBox> ().cubeScript.panelPieces [3]) {
//				return false;}
			break;
		case 13:
			break;
		case 14:
			break;
		case 15:
			break;
		case 16:
			break;
		case 17:
			break;
		case 18:
//			return false;
//			if (neighbours [18] && neighbours [18].GetComponent<GridBox> ().cubeScript.panelPieces [3]) {
//				return false;}
//			if (neighbours [19] && neighbours [19].GetComponent<GridBox> ().cubeScript.panelPieces [1]) {
//				return false;}
//			//////
//			if (neighbours [12] && neighbours [12].GetComponent<GridBox> ().cubeScript.panelPieces [4]) {
//				return false;}
			break;
		case 19:
			//return false;
			break;
		case 20:
			//return false;
			break;
		case 21:
//			return false;
//			if (neighbours [21] && neighbours [21].GetComponent<GridBox> ().cubeScript.panelPieces [3]) {
//				return false;}
//			if (neighbours [22] && neighbours [22].GetComponent<GridBox> ().cubeScript.panelPieces [1]) {
//				return false;}
			//////
			break;
		case 22:
			break;
		case 23:
			//return false;
			break;
		case 24:
			//return false;
			break;
		case 25:
		//	return false;
			break;
		case 26:
		//	return false;
			break;
		default:
		//	return true;
			break;
		}
		return true;
	}
	//		Vector3 ownGridLoc = new Vector3(gridLocX, gridLocY, gridLocZ);
	//		Vector3 otherGridObjLoc = new Vector3 (ownGridLoc.x - coords.x, ownGridLoc.z - coords.y, ownGridLoc.y - coords.z);
	//
	//
	//		RaycastHit rayHit;
	//		Vector3 rayDirection;
	//		LayerMask layermask = 1 << 8;
	//		GameObject otherGridObj;
	//		DefaultCubeScript otherCubeScript;
	//
	//		bool playerCanFly = gameManager.player.GetComponent<TestPlayerScript> ().playerStats ["FLY"] == 1;
	//
	//		bool[] otherCubePanelPiecesRef;
	//
	//		if (gridManager.GridLocToGridObjLookup[otherGridObjLoc] != null) {
	//
	//			otherGridObj = (GameObject)gridManager.GridLocToGridObjLookup [otherGridObjLoc];
	//			otherCubeScript = otherGridObj.GetComponent<GridBox>().cubeScript;
	//			otherCubePanelPiecesRef = otherCubeScript.panelPieces;
	//
	//			// check to see if cube has no walls, or that player can fly
	//			if (otherCubeScript.cubeUniqueID != 0 || playerCanFly) {
	//
	//				if (CheckConnectionValid (cubeUniqueID, otherCubeScript.cubeUniqueID, neighNum, playerCanFly)) {
	//
	//					// Final raycast check to cover any bases missed
	//					rayDirection = otherGridObj.transform.position - transform.position;
	//					if (!(Physics.Linecast (transform.position, otherGridObj.transform.position, out rayHit, layermask))) {
	//						legalNeighbours.Add (otherGridObj);
	//					}
	//				}
	//			}
	//		}


	public List<GameObject> GetLegalNeighbours () {
		return legalNeighbours;
	}


	public void SetLinked(bool param) {

		if (param) {
			legalNeighbours.Clear ();
			SetLegalNeighbours ();
			linked = true;
		} else {
			linked = false;
		}
	}
		

//	void OnTriggerEnter(Collider other) {
//		if (other.tag == "LocSettingCollider") {
//			cubeReference = other.transform.parent.gameObject;
//			cubeScript = cubeReference.transform.GetComponent<DefaultCubeScript> ();
//			cubeScript.gridLocX = gridLocX;
//			cubeScript.gridLocZ = gridLocZ;
//			cubeScript.gridLocY = gridLocY;
//			// set cube reference in World Grid
//			gameManager.WorldGrid_CubeRef_LayersList[gridLocY][gridLocX, gridLocZ] = cubeReference;
//
//			other.gameObject.SetActive (false);
//			this.gameObject.SetActive (false);
//
//			cubeReference = null;
//		}
//	}
}
