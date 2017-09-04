using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCubeScript : MonoBehaviour {

	private TestPlayerScript playerScript;

	public int gridLocZ = -1;
	public int gridLocX = -1;
	public int gridLocY = -1;

	private GameObject cubeSelectTrans;

	private bool cubeWalkable = false;
	public bool cubeVisible = false;

	// Use this for initialization
	void Start () {

		playerScript = FindObjectOfType<TestPlayerScript> ();


		if (transform.Find ("CubeSelect")) {
			cubeSelectTrans = transform.Find ("CubeSelect").gameObject;
		}

		// this will need to change for the alien to walk up walls
		// and if marine is to climb up walls (ladders)
		if (transform.Find ("Ground")) {
			if (transform.Find ("Ground").gameObject.activeSelf) {
				cubeWalkable = true;
			} else {
				cubeWalkable = false;
			}
		}
		////////////////////////////////////////////////

	}


	public void CubeSelect(string selectType) {
		if (cubeWalkable && cubeVisible) {
			if (transform.Find ("CubeSelect")) {
				switch (selectType) { 
				case "Move":
					playerScript.MovePlayer (this.gameObject);
					break;
				default:
					break;
				}
			}
		}
	}

	////////////////////////////////////////////////
	// Special green box highlighting cube 
	public void CubeHighlight(string selectType) {
		if (cubeWalkable && cubeVisible) {
			if (transform.Find ("CubeSelect")) {
				switch (selectType) { 
				case "Move":
					cubeSelectTrans.SetActive (true);
					break;
				default:
					break;
				}
			}
		}
	}
	public void CubeUnHighlight(string selectType) {
		if (transform.Find ("CubeSelect")) {
			switch (selectType) { 
			case "Move":
				cubeSelectTrans.SetActive (false);
				break;
			default:
				break;
			}
		}
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
