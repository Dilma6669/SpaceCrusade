using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	// THE MASTER /////////////////////
	MASTER master;
	OBSERVER Observer;
	////////////////////////////////////
	private int worldSizeX;
	private int worldSizeZ;
	private int worldSizeY;

	int totalObjectsX;
	int totalObjectsZ;
	int totalObjectsY;

	private GameManager gameManager;

	public GameObject gridObjectPrefab;
	public GameObject spawnPrefab;
	private GameObject spawn;

	// making a lookUp table with objects and there Vector3 locations

	public Hashtable GridLocToWorldLocLookup;
	public Hashtable GridLocToGridObjLookup;

	public Hashtable pathfindingCubeList;
	public int[,] pathFindingGrid;

	// Use this for initialization
	void Start () {
		
		// THE MASTER /////////////////////
		// designed as a easy user input for designer
		master = FindObjectOfType<MASTER> ();
		Observer = FindObjectOfType<OBSERVER> ();
		////////////////////////////////////
		worldSizeX = master.worldSizeX;
		worldSizeZ = master.worldSizeZ;
		worldSizeY = master.worldSizeY;

		gameManager = FindObjectOfType<GameManager> ();

		GridLocToWorldLocLookup = new Hashtable ();
		GridLocToGridObjLookup = new Hashtable ();

		pathfindingCubeList = new Hashtable ();

		// SPAWN //
		spawn = Instantiate (spawnPrefab, transform, false);
		////////////////

	}

//	void Update() {
//
//		//GetNeighbourConnections ();
//
//	}
	

	public void BuildGridObjLookup() {

		totalObjectsX = gameManager.totalCubesInX; // 12
		totalObjectsZ = gameManager.totalCubesInZ; //12
		totalObjectsY = gameManager.totalCubesInY; //3

		int objectsCountX = 0;
		int objectsCountZ = 0;
		int objectsCountY = 0;

		int sizeOfBoard = -110;
		int halfSizeOfBoard = -55;
		int squareDistance = -10;

		int layerHeight = 30;
		int cubeHeight = 10;

		int spawnPosX;
		int spawnPosZ;
		int spawnPosY;


		int baseEquation = (sizeOfBoard*worldSizeX)/2 + (squareDistance/2)*(worldSizeX-1);

		int startX = baseEquation;
		int startZ = halfSizeOfBoard;
		int startY = 0;

		// Each Layer (layer contains 3 cubes in Y)
		for (int layer = 0; layer < worldSizeY; layer++) {

			spawnPosX = startX;
			spawnPosZ = startZ;
			spawnPosY = (layer * layerHeight);

			for (int cube = 0; cube < gameManager.numCubesInY; cube++) {

				spawnPosX = startX;
				spawnPosZ = startZ;
				spawnPosY = (cube * cubeHeight + (layer * layerHeight));

				objectsCountX = 0;
				objectsCountZ = 0;

				for (int i = 0; i < totalObjectsZ; i++) {

					spawnPosX = startX;
					spawn.transform.localPosition = new Vector3 (spawnPosX, spawnPosY, spawnPosZ);

					objectsCountX = 0;

					for (int j = 0; j < totalObjectsX; j++) {

						///////////////////////////////
						// put vector location, eg, grid Location 0,0,0 and World Location 35, 0, 40 value pairs into hashmap for easy lookup
						Vector3 gridLoc = new Vector3 (objectsCountX, objectsCountZ, objectsCountY);
						Vector3 worldLoc = new Vector3 (spawn.transform.localPosition.x, spawn.transform.localPosition.y, spawn.transform.localPosition.z);
						//Debug.Log ("Vector3 (gridLoc): x: " + gridLoc.x + " z: " +  gridLoc.z + " y: " +  gridLoc.y);
						//Debug.Log ("Vector3 (worldLoc): x: " + worldLoc.x + " y: " +  worldLoc.y + " z: " +  worldLoc.z);
						GridLocToWorldLocLookup.Add (gridLoc, worldLoc);
						/////////////////////////////////


						////////////////////////////////////////
						GameObject GridObject = Instantiate (gridObjectPrefab, spawn.transform, false);
						GridObject.transform.SetParent (this.gameObject.transform);
						GridBox objScript = GridObject.GetComponent<GridBox> ();
						objScript.gridLocX = objectsCountX;
						objScript.gridLocZ = objectsCountZ;
						objScript.gridLocY = objectsCountY;
						//Debug.Log ("GridLocToGridObjLookup.Add to: " + gridLoc.x + " " + gridLoc.y + " " + gridLoc.z);
						GridLocToGridObjLookup.Add (gridLoc, GridObject);
						//////////////////////////////////////////

						spawnPosX += 10;
						spawn.transform.localPosition = new Vector3 (spawnPosX, spawnPosY, spawnPosZ);
						objectsCountX += 1;
					}
					spawnPosZ += 10;
					objectsCountZ += 1;
				}
				objectsCountY += 1;
			}
		}
		Observer.GridBuildingComplete (true);
		Invoke ("SetAllGridsNeighbours", 2.0f);
	}

	public void SetAllGridsNeighbours() {
		// x, z, y
		// grid location
		for (int y = 0; y < gameManager.totalCubesInY; y++) {
			for (int z = 0; z < gameManager.totalCubesInZ; z++) {
				for (int x = 0; x < gameManager.totalCubesInX; x++) {
					GameObject GridObject = (GameObject)GridLocToGridObjLookup[new Vector3 (x, z, y)];
					GridObject.GetComponent<GridBox> ().GetNeighbourConnectionsOnStartUp();
				}
			}
		}
		Observer.GridNeighboursComplete (true);
	}


	public void ClearConnectionsForObjects() {
		foreach (GameObject cube in pathfindingCubeList) {
			cube.GetComponent<GridBox> ().linked = false;
		}
		pathfindingCubeList.Clear();
		// x, z, y
		// grid location
//		for (int y = 0; y < gameManager.totalCubesInY; y++) {
//			for (int z = 0; z < gameManager.totalCubesInZ; z++) {
//				for (int x = 0; x < gameManager.totalCubesInX; x++) {
//					GameObject GridObject = (GameObject)GridLocToGridObjLookup[new Vector3 (x, z, y)];
//					GridObject.GetComponent<GridBox> ().linked = false;
//					GridObject.GetComponent<GridBox> ().cubeRecorded = false;
//				}
//			}
//		}
	}

	public void RecursiveCall(GameObject originNode, int spread) {
		if (spread <= 0) {
			return;
		}
		GridBox originNodeScript = originNode.GetComponent<GridBox> ();
		originNodeScript.SetLinked (true);
		Vector3 originNodeLoc = new Vector3(originNodeScript.gridLocX, originNodeScript.gridLocZ, originNodeScript.gridLocY);
		AddCubeToPathFindingList (originNodeLoc, originNode);

		List<GameObject> legalNeighboursRef = originNodeScript.GetLegalNeighbours ();
		//Debug.Log ("originNode x z y " + originNode.GetComponent<GridBox> ().gridLocX + " " + originNode.GetComponent<GridBox> ().gridLocZ + " " + originNode.GetComponent<GridBox> ().gridLocY + " Spread: " + spread);
		for (int i = 0; i < legalNeighboursRef.Count; i++) {
			GridBox neighNodeScript = legalNeighboursRef [i].GetComponent<GridBox> ();
			Vector3 neighgridLoc = new Vector3(neighNodeScript.gridLocX, neighNodeScript.gridLocZ, neighNodeScript.gridLocY);
			if (pathfindingCubeList [neighgridLoc] == null) {
				RecursiveCall (legalNeighboursRef [i], spread - 1);
			}
		}
		return;
	}


	public void SendOutRays(bool activate, Vector3 ownGridLoc, int playerMove) {
		Debug.Log ("SendOutRays");

		int spread = playerMove;

		pathFindingGrid = new int[spread, spread];
		GameObject gridObject = (GameObject)GridLocToGridObjLookup [ownGridLoc];

		if (activate) {
			RecursiveCall (gridObject, spread);
		} else {
			gridObject.GetComponent<GridBox> ().SetLinked(false);
		}
	}



	public void AddCubeToPathFindingList(Vector3 gridLoc, GameObject cube) {
		pathfindingCubeList.Add (gridLoc, cube);
	}
//	public List<GameObject> GetPathFindingCubeList() {
//		return pathfindingCubeList;
//	}
//


	//////////////////////////////////////////////
	public void ActivateGrid() {
		foreach (Transform child in transform) {
			if (child.GetComponent<BoxCollider> ()) {
				child.GetComponent<BoxCollider> ().enabled = true;
			}
		}
		Invoke ("WaitForTriggers", 1.0f);
	}
	public void WaitForTriggers() {
		gameManager.GAMEMASTER_GridFinish ();
	}
	/////////////////////////////////////////////


	public void DeactivateGrid() {
		foreach (Transform child in transform) {
			//child.gameObject.SetActive (false);
			Destroy (child.gameObject);
		}
		gameManager.GAMEMASTER_GUI();
	}

		
}
