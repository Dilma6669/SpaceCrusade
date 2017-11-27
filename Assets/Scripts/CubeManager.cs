using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour {

	// THE MASTER /////////////////////
	MASTER master;
	OBSERVER Observer;
	////////////////////////////////////
	private int worldSizeX;
	private int worldSizeZ;
	private int worldSizeY;


	GameManager gameManager;
	GridManager gridManager;

	public List<GameObject> cubeBlocksList = new List<GameObject>();

	public Hashtable GridLocToCubeObjLookup;

	public List<int[,]> floors = new List<int[,]>();
	public int[,] floorArray00;
	public int[,] floorArray01;

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
		gridManager = FindObjectOfType<GridManager> ();

		GridLocToCubeObjLookup = new Hashtable ();

		//		case 0: Empty
		//		case 1: Floor
		//		case 2: Left
		//		case 3: Front
		//		case 4: Right
		//		case 5: Back
		//		case 6: Ceiling
		//		case 7: Floor Left Back
		//		case 8: Floor Left
		//		case 9: Floor Left Front
		//		case 10: Floor front
		//		case 11: Floor Front Right
		//		case 12: Floor Right 
		//		case 13: Floor Right Back
		//		case 14: Floor Back
		//		case 15: Left Back
		//		case 16: Left Front
		//		case 17: Front Right
		//		case 18: Right Back


		// LOOK AT THESE AS UNDERNEATH MAPS
		// First Floor
		floors.Add(new int[,] {
//			{ 07, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 13}, 
//			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 12, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 12, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 12, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 12, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
//			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
//			{ 09, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11}
			{ 07, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 13}, 
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 08, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 12},
			{ 09, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11}
		});
		// second Floor
		floors.Add(new int[,] {
//			{ 15, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 18}, 
//			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 04, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 04, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 04, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 04, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
//			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
//			{ 16, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 17},
			{ 15, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 18}, 
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 16, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 17},
		});
		// third Floor
		floors.Add(new int[,] {
			{ 15, 05, 05, 05, 05, 05, 05, 05, 05, 05, 05, 18}, 
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 02, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 04},
			{ 16, 03, 03, 03, 03, 03, 03, 03, 03, 03, 03, 17},
		});
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//
	public void AttachCubeToLoc() {

		int totalObjectsX = gameManager.totalCubesInX; // 12
		int totalObjectsZ = gameManager.totalCubesInZ; //12
		int totalObjectsY = gameManager.totalCubesInY; //3

		int objectsCountX = 0;
		int objectsCountZ = 0;
		int objectsCountY = 0;

		Vector3 loc;
		Vector3 GridLoc;
		GameObject gridObject;
		DefaultCubeScript cubeScript;
		GridBox gridScript;

		int[,] floor;

		// Each Layer (layer contains 3 cubes in Y)
		for (int layer = 0; layer < worldSizeY; layer++) {

			for (int cube = 0; cube < gameManager.numCubesInY; cube++) {
				objectsCountX = 0;
				objectsCountZ = 0;

				floor = floors[objectsCountY];

				for (int i = 0; i < totalObjectsZ; i++) {
					objectsCountX = 0;
					for (int j = 0; j < totalObjectsX; j++) {

						int cubeType = floor [objectsCountZ, objectsCountX];

						GridLoc = new Vector3 (objectsCountX, objectsCountZ, objectsCountY);
						loc = (Vector3)gridManager.GridLocToWorldLocLookup [GridLoc];
						gridObject = (GameObject)gridManager.GridLocToGridObjLookup [GridLoc];
						//////////
						GameObject cubeObject = Instantiate (cubeBlocksList[cubeType], this.transform, false);
						cubeObject.transform.position = loc;
						cubeObject.transform.SetParent (this.gameObject.transform);
						GridLocToCubeObjLookup.Add (GridLoc, cubeObject);
						//////////

						cubeScript = cubeObject.GetComponent<DefaultCubeScript> ();
						gridScript = gridObject.GetComponent<GridBox> ();

						cubeScript.gridObjectRef = gridObject;
						cubeScript.gridLocX = gridScript.gridLocX;
						cubeScript.gridLocZ = gridScript.gridLocZ;
						cubeScript.gridLocY = gridScript.gridLocY;

						gridScript.cubeObjectRef = cubeObject;
						gridScript.cubeScript = cubeScript;

						// dont like this here but cant think of better way now
//						if (cubeType == 0) {
//							gridObject.GetComponent<GridBox> ().walkable = false;
//						} else {
//							gridObject.GetComponent<GridBox> ().walkable = true;
//						}

						///////////////
						objectsCountX += 1;
					}
					objectsCountZ += 1;
				}
				objectsCountY += 1;
			}

		}
		Observer.CubeAttachingComplete (true);
		Invoke ("SetAllCubesNeighbours", 1.0f);
	}


	public void SetAllCubesNeighbours() {

		for (int y = 0; y < gameManager.totalCubesInY; y++) {
			for (int z = 0; z < gameManager.totalCubesInZ; z++) {
				for (int x = 0; x < gameManager.totalCubesInX; x++) {
					//Debug.Log ("x, z, y: " + x + " " + z + " " + y);
					GameObject cubeObject = (GameObject)GridLocToCubeObjLookup [new Vector3 (x, z, y)];
					cubeObject.GetComponent<DefaultCubeScript> ().CheckPanelsToSetUniqueIds ();
				}
			}
		}
		Observer.CubeNeighboursComplete (true);
	}
}
