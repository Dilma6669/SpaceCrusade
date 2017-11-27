using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBSERVER : MonoBehaviour {

	GameObject activeCube;
	GameObject playerSelected;

	public bool gridBuildingComplete = false;
	public bool gridNeighboursComplete = false;
	public bool cubeAttachingComplete = false;
	public bool cubeNeighboursComplete = false;
	public bool GAMESTART = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void UpdateActiveCube(GameObject cube) {
		if (activeCube != null) {
			activeCube.transform.GetComponent<DefaultCubeScript>().CubeNotVisible ();
		}
		activeCube = null;
		activeCube = cube;
	}

	public void UpdatePlayerSelected(GameObject player) {
		playerSelected = null;
		playerSelected = player;
	}



	public void GridBuildingComplete(bool param) {
		gridBuildingComplete = param;
	}

	public void GridNeighboursComplete(bool param) {
		gridNeighboursComplete = param;
	}

	public void CubeAttachingComplete(bool param) {
		cubeAttachingComplete = param;
	}

	public void CubeNeighboursComplete(bool param) {
		cubeNeighboursComplete = param;
	}

	public void StartGame(bool param) {
		GAMESTART = param;
	}


}
