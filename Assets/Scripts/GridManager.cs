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

	public GridObjectNEW[] objects; 

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
	

	public void BuildGridObjLookup() {

		totalObjectsX = gameManager.totalCubesInX; // 12
		totalObjectsZ = gameManager.totalCubesInZ; //12
		totalObjectsY = gameManager.totalCubesInY; //3

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

						//GameObject GridObject = Instantiate (gridObjectPrefab, spawn.transform, false);
						//GridObject.transform.SetParent (this.gameObject.transform);

						//Debug.Log ("Vector3 (spawnPosX, spawnPosY, spawnPosZ): " + spawn.transform.localPosition.x + " " +  spawn.transform.localPosition.y + " " +  spawn.transform.localPosition.z);
						// put vector location, eg, grid Location 0,0,0 and World Location 35, 0, 40 value pairs into hashmap for easy lookup
						Vector3 gridLoc = new Vector3 (objectsCountX, objectsCountZ, objectsCountY);
						Vector3 worldLoc = new Vector3 (spawn.transform.localPosition.x, spawn.transform.localPosition.y, spawn.transform.localPosition.z);
						//Debug.Log ("Vector3 (gridLoc): x: " + gridLoc.x + " z: " +  gridLoc.y + " y: " +  gridLoc.z);
						//Debug.Log ("Vector3 (worldLoc): x: " + worldLoc.x + " y: " +  worldLoc.y + " z: " +  worldLoc.z);
						//Debug.Log ("-----");
						gridObjLookup.Add (gridLoc, worldLoc);

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
	}
		

	/////////////////////////////////////////////////////////////
	// Returns a list of World locations of all node Neighbours
	// order of neighbours gets assigned like grid objects are build
	// X, z, y
	// original node is set in the middle at position 1,1,1
	// first neighbour is at 0,0,0, then 1,0,0, then 2,0,0 etc
	public List<Vector3> GetNeighbourConnections(Vector3 ownVect) {

		List<Vector3> neighbours = new List<Vector3>();

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y - 1)]); // position 000
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y - 1)]); // position 100
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y - 1)]); // position 200

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y - 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y - 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y - 1)]);

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y - 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y - 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y - 1)]);

		/////////////////////////////////

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y + 0)]); // position 001
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y + 0)]); // position 101
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y + 0)]); // position 201

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y + 0)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y + 0)]); // Oiginal Node
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y + 0)]);

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y + 0)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y + 0)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y + 0)]);

		/////////////////////////////////

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y + 1)]); // position 002
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y + 1)]); // position 102
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y + 1)]); // position 202

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y + 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y + 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y + 1)]);

		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y + 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y + 1)]);
		neighbours.Add((Vector3)gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y + 1)]);
		/////////////////////////////////

		return neighbours;
	}


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
