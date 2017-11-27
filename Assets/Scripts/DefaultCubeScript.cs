using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCubeScript : MonoBehaviour {

	OBSERVER Observer;
	TestPlayerScript playerScript;

	public int gridLocZ = -1;
	public int gridLocX = -1;
	public int gridLocY = -1;

	public int cubeUniqueID;

	public GameObject gridObjectRef;
	private GameObject cubeSelectTrans;
 
	private bool panelGround = false;
	private bool panelLeft = false;
	private bool panelFront = false;
	private bool panelRight = false;
	private bool panelBack = false;
	private bool panelCeiling = false;
	// Angles // HAVE TO REMEMEBR TO ''''TAG''' the Angle cube objects !!!!
//	private bool panelGroundLeft = false;
//	private bool panelGroundFront = false;
//	private bool panelGroundRight = false;
//	private bool panelGroundBack = false;
//	private bool panelCeilingLeft = false;
//	private bool panelCeilingFront = false;
//	private bool panelCeilingRight = false;
//	private bool panelCeilingBack = false;


	public bool[] panelPieces = new bool[14];

	public bool cubeVisible = false;
	public bool cubSelected = false;

	// Use this for initialization
	void Start () {

		Observer = FindObjectOfType<OBSERVER> ();
		playerScript = FindObjectOfType<TestPlayerScript> ();

		panelPieces[0] = panelGround;
		panelPieces[1] = panelLeft;
		panelPieces[2] = panelFront;
		panelPieces[3] = panelRight;
		panelPieces[4] = panelBack;
		panelPieces[5] = panelCeiling;
		// Angles
//		panelPieces[6] = panelGroundLeft;
//		panelPieces[7] = panelGroundFront;
//		panelPieces[8] = panelGroundRight;
//		panelPieces[9] = panelGroundBack;
//		panelPieces[10] = panelCeilingLeft;
//		panelPieces[11] = panelCeilingFront;
//		panelPieces[12] = panelCeilingRight;
//		panelPieces[13] = panelCeilingBack;
//
//		if (transform.Find ("CubeSelect")) {
//			cubeSelectTrans = transform.Find ("CubeSelect").gameObject;
//		}

		// this will need to change for the alien to walk up walls
		// and if marine is to climb up walls (ladders)
//		if (transform.Find ("Ground")) {
//			if (transform.Find ("Ground").gameObject.activeSelf) {
//				cubeWalkable = true;
//			} else {
//				cubeWalkable = false;
//			}
//		}
		////////////////////////////////////////////////

	}
		
	// Angles not sorted yet!!!!!!!!
	public void CheckPanelsToSetUniqueIds() {
		for(int i = 0; i < panelPieces.Length; i++) {
			panelPieces[i] = false;
		}

		// refer to spreadsheet to undersatnd these numbers
		int panelCode = 0; // empty
		int panelMultiplier = 0;
		int overAllMultiplier = 9;

		cubeUniqueID = 0;
		string panelTag = "";
		foreach (Transform child in transform) {
			if (child != null && child.tag != null) {
				panelTag = child.tag;
				switch (panelTag) {
				case "CubeGround":
					panelPieces [0] = true;
					panelCode = 1;
					panelMultiplier = 1;
					cubeUniqueID += (panelCode*panelMultiplier);
					break;
				case "CubeLeft":
					panelPieces [1] = true;
					panelCode = 2;
					panelMultiplier = 2;
					cubeUniqueID += (panelCode*panelMultiplier);
					break;
				case "CubeFront":
					panelPieces [2] = true;
					panelCode = 3;
					panelMultiplier = 3;
					cubeUniqueID += (panelCode*panelMultiplier);
					break;
				case "CubeRight":
					panelPieces [3] = true;
					panelCode = 4;
					panelMultiplier = 4;
					cubeUniqueID += (panelCode*panelMultiplier);
					break;
				case "CubeBack":
					panelPieces [4] = true;
					panelCode = 5;
					panelMultiplier = 5;
					cubeUniqueID += (panelCode*panelMultiplier);
					break;
				case "CubeCeiling":
					panelPieces [5] = true;
					panelCode = 6;
					panelMultiplier = 6;
					cubeUniqueID += (panelCode*panelMultiplier);
					break;
				default:
				//	Debug.Log ("DEFAULT");
					cubeUniqueID += 0;
					break;
					// Angles
//				case "CubeGroundLeft":
//					panelPieces [6] = true;
//					cubeUniqueID += 100;
//					break;
//				case "CubeGroundFront":
//					panelPieces [7] = true;
//					cubeUniqueID += 100;
//					break;
//				case "CubeGroundRight":
//					panelPieces [8] = true;
//					cubeUniqueID += 100;
//					break;
//				case "CubeGroundBack":
//					panelPieces [9] = true;
//					cubeUniqueID += 100;
//					break;
//				case "CubeCeilingLeft":
//					panelPieces [10] = true;
//					cubeUniqueID += 100;
//					break;
//				case "CubeCeilingFront":
//					panelPieces [11] = true;
//					cubeUniqueID += 100;
//					break;
//				case "CubeCeilingRight":
//					panelPieces [12] = true;
//					cubeUniqueID += 100;
//					break;
//				case "CubeCeilingBack":
//					panelPieces [13] = true;
//					cubeUniqueID += 100;
//					break;
				}
			}
		}

		// Set both cube AND Grid uniqueIDS
		cubeUniqueID += (transform.childCount-1)*overAllMultiplier; // -1 becuse of 'select' child object
		gridObjectRef.GetComponent<GridBox>().cubeUniqueID = cubeUniqueID;
	}


	public void CubeSelect(string selectType) {
		cubSelected = true;
		Observer.UpdateActiveCube (this.gameObject);

		//if (cubeWalkable && cubeVisible) {
			//if (transform.Find ("CubeSelect")) {
				switch (selectType) { 
				case "Move":
					Debug.Log ("PLAYER MOve");
					//playerScript.MovePlayer (this.gameObject.transform.position);
					break;
				default:
					break;
			//	}
			//}
		}
	}

	////////////////////////////////////////////////
	// Special green box highlighting cube 
	public void CubeHighlight(string selectType) {
	//	if (cubeWalkable && cubeVisible) {
			if (transform.Find ("CubeSelect")) {
				switch (selectType) { 
				case "Move":
					cubeSelectTrans.SetActive (true);
					break;
				default:
					break;
				}
			}
	//	}
	}
	public void CubeUnHighlight(string selectType) {
	//	if (transform.Find ("CubeSelect")) {
			switch (selectType) { 
			case "Move":
				cubeSelectTrans.SetActive (false);
				break;
			default:
				break;
			}
		//}
	}
	////////////////////////////////////////////////



	////////////////////////////////////////////////
	// If player canNOT see this cube
	public void CubeNotVisible() {
		cubeVisible = false;
		foreach (Transform child in transform) {
			if (child.gameObject.activeSelf && child.GetComponent<PanelPieceScript> ()) {
				child.GetComponent<PanelPieceScript> ().PanelPieceChangeColor ("Black");
			}
		}
	}
	// If player can see this cube
	public void CubeVisible() {
		cubeVisible = true;
		foreach (Transform child in transform) {
			if (child.gameObject.activeSelf && child.GetComponent<PanelPieceScript> ()) {
				child.GetComponent<PanelPieceScript> ().PanelPieceChangeColor ("White");
			}
		}
	}
	////////////////////////////////////////////////
}
