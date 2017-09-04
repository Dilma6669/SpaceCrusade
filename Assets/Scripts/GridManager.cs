using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	// THE MASTER /////////////////////
	MASTER master;
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

	public Hashtable gridObjLookup;

	// Use this for initialization
	void Start () {
		
		// THE MASTER /////////////////////
		// designed as a easy user input for designer
		master = FindObjectOfType<MASTER> ();
		////////////////////////////////////
		worldSizeX = master.worldSizeX;
		worldSizeZ = master.worldSizeZ;
		worldSizeY = master.worldSizeY;

		gameManager = FindObjectOfType<GameManager> ();

		gridObjLookup = new Hashtable ();

		// SPAWN //
		spawn = Instantiate (spawnPrefab, transform, false);
		////////////////

	}
	


	public void BuildGrid() {

		totalObjectsX = gameManager.totalCubesInX; // 12
		totalObjectsZ = gameManager.totalCubesInZ; //12
		totalObjectsY = gameManager.totalCubesInY; //3

		StartCoroutine (WaitForLayers (1.0f));
	}
	IEnumerator WaitForLayers(float secs) {

		int objectsCountX = 0;
		int objectsCountZ = 0;
		int objectsCountY = 0;

		int halfSizeOfBoard = -55;
		int squareDistance = -10;

		int layerHeight = 30;
		int cubeHeight = 10;

		int spawnPosX;
		int spawnPosZ;
		int spawnPosY;

		int startX = ((worldSizeX * halfSizeOfBoard) + (worldSizeX / 2 * squareDistance));
		int startZ = halfSizeOfBoard;
		int startY = 0;

		// Each Layer (layer contains 3 cubes in Y)
		for (int layer = 0; layer < worldSizeY; layer++) {

			spawnPosX = startX;
			spawnPosZ = startZ;
			spawnPosY = (layer * layerHeight);

			for (int cube = 0; cube < gameManager.numCubesInY; cube++) {

				// build 2d array (for each layer) to record cube references. and put 2d array in list in gameManager
				//GameObject[,] WorldGrid_LayerCubeRef = new GameObject[totalObjectsX, totalObjectsZ];
				//gameManager.WorldGrid_CubeRef_LayersList.Add (WorldGrid_LayerCubeRef);
				///////////////////////////////////////////////////////////

				spawnPosX = startX;
				spawnPosZ = startZ;
				spawnPosY = (cube * cubeHeight + (layer * layerHeight));

				for (int i = 0; i < totalObjectsZ; i++) {

					spawnPosX = startX;
					objectsCountX = 0;
					spawn.transform.localPosition = new Vector3 (spawnPosX, spawnPosY, spawnPosZ);

					for (int j = 0; j < totalObjectsX; j++) {

						GameObject gridObject = Instantiate (gridObjectPrefab, spawn.transform, false);
						gridObject.transform.SetParent (transform);
						GridBox objScript = gridObject.transform.GetComponent<GridBox> ();
						objScript.gridLocX = objectsCountX;
						objScript.gridLocZ = objectsCountZ;
						objScript.gridLocY = objectsCountY;

						// put vector location, gridObject key, value pairs into hashmap for easy lookup
						Vector3 vect = new Vector3 (objScript.gridLocX, objScript.gridLocZ, objScript.gridLocY);
						gridObjLookup.Add (vect, gridObject);

						spawnPosX += 10;
						spawn.transform.localPosition = new Vector3 (spawnPosX, spawnPosY, spawnPosZ);
						objectsCountX += 1;
					}
					spawnPosZ += 10;
					objectsCountZ += 1;
				}
				objectsCountY += 1;
			}
			// wait for each layer to load before loading next (experiment to see if loads faster)
			Debug.Log("Layer finished");
			yield return new WaitForSeconds (secs);
		}
		GetNeighbourConnections ();
	}

	public void GetNeighbourConnections() {

		// GET ALL CUBES AND ASSIGN ALL THEIR NEIGHBOURS TO THEM
		foreach (Transform child in transform) {
			if (child && child.GetComponent<GridBox> ()) {
				child.GetComponent<GridBox> ().GetNeighbourConnections ();
			}
		}
		Debug.Log ("finsihed getting neighbours");
		

	}
		

	//////////////////////////////////////////////
	public void ActivateGrid() {
		foreach (Transform child in transform) {
			if (child.GetComponent<BoxCollider> ()) {
				child.GetComponent<BoxCollider> ().enabled = true;
			}
		}
		StartCoroutine (WaitForTriggers (1.0f));
	} // Wait For Triggers To Register (this is frustrating);
	IEnumerator WaitForTriggers(float secs) {
		yield return new WaitForSeconds (secs);
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
