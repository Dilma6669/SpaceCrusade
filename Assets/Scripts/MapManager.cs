using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	// THE MASTER /////////////////////
	MASTER master;
	////////////////////////////////////
	private int worldSizeX;
	private int worldSizeZ;
	private int worldSizeY;
	private int totalMapCountPerLayer;

	private GameManager gameManagerScript;

	private int[,][] AvailSidesGrid;
	private int[,][] SpawnPosXYZGrid;

	public List<GameObject> mapPieces = new List<GameObject>();
	public GameObject spawnPrefab;
	private GameObject spawn;

	public GameObject layerPrefab;
	private GameObject layerContainer;

	private int[] currGridPos;
	private int[,] walkedPathList;
	private int walkedPathIndex = 0;

	public List<GameObject> mapsPlaced = new List<GameObject>();

	// LastMove Map placed
	private string lastMove = "U";

	// when maps have finished loading
	private bool mapsFinished = false;
	//public bool[,] basicMapGrid;
	//////////////////////////////


	// Use this for initialization
	void Start () {

		// THE MASTER /////////////////////
		// designed as a easy user input for designer
		master = FindObjectOfType<MASTER> ();
		////////////////////////////////////
		worldSizeX = master.worldSizeX;
		worldSizeZ = master.worldSizeZ;
		worldSizeY = master.worldSizeY;
		totalMapCountPerLayer = (worldSizeX*worldSizeZ);

		gameManagerScript = FindObjectOfType<GameManager> ();

		// Where maps are placed
		//basicMapGrid = new bool[worldSizeZ, worldSizeX];

		// SPAWN //
		spawn = Instantiate (spawnPrefab, transform, false);
		////////////////

		ResetComponents ();

	}

	public void CreateMaps() {
		StartCoroutine (CreateMap (0.5f));
	}


	public void ResetComponents() {

		AvailSidesGrid = null;
		AvailSidesGrid = new int[worldSizeZ, worldSizeX][];
		SpawnPosXYZGrid = null;
		SpawnPosXYZGrid = new int[worldSizeZ, worldSizeX][];
		currGridPos = null;
		currGridPos = new int[2] {(int)Mathf.Floor ((float)worldSizeZ / 2), 0};
		walkedPathList = null;
		walkedPathList = new int[totalMapCountPerLayer,2];
		walkedPathIndex = 0;
		lastMove = "U";

	}

	void BuildSpawnPosXYZGrid(int _y) {
		int x = -120;
		int y = _y;
		int z = 0;
		for (int i = 0; i < worldSizeX; i++) {
			for (int j = 0; j < worldSizeZ; j++) {
				SpawnPosXYZGrid [j, i] = new int[3] {x, y, z};
				x += 120;
			}
			x = -120;
			z += 120;
		}
	}

	// Build a layer Container for each level of map (worldSizeY)
	// Build layer of maps inside each Layer Container
	IEnumerator CreateMap(float secs) {

		//yield return new WaitForSeconds (secs);

		for (int y = 0; y < worldSizeY; y++) {

			ResetComponents ();

			BuildSpawnPosXYZGrid ((y*30));
			spawn.transform.position = new Vector3 (0, (y * 30), 0);


			// Build the layer Container
			layerContainer = Instantiate (layerPrefab, this.gameObject.transform, true);
			layerContainer.transform.SetParent (this.gameObject.transform);



			for (int i = 0; i < totalMapCountPerLayer; i++) {

				/*
				Debug.Log ("-------------------------------------- Placeing tile: " + (i + 1));
				Debug.Log ("------------------------------------ at GRIDPos X: " + currGridPos [0] + " , Y: " + currGridPos [1]);
				Debug.Log ("-------------------------------------- spawnPos: "
				+ spawn.transform.position.x + ","
				+ spawn.transform.position.y + ","
				+ spawn.transform.position.z + "");
				*/

				//////////////////////////////////////////////////////////////////
				/// Update walkedPathList with currentGrid Location
				walkedPathList [walkedPathIndex, 0] = currGridPos [0];
				walkedPathList [walkedPathIndex, 1] = currGridPos [1];
				walkedPathIndex++;
				//////////////////////////////////////////////////////////////////////

				//////////////////////////////////////////////////////////////////////
				///  Pick and make random map and build and get script reference
				int randNum = Random.Range (0, 4);
				GameObject MapPiece = Instantiate (mapPieces [randNum], spawn.transform, false);
				MapPiece.transform.SetParent (layerContainer.transform);
				mapsPlaced.Add (MapPiece); // for player
				MapPieceScript mapScript = MapPiece.transform.GetComponent<MapPieceScript> ();
				//////////////////////////////////////////////////////////////////////

				//////////////////////////////////////////
				/// wait for next tile
				if (!mapScript.positionFound) {
					yield return new WaitForSeconds (secs);
				}
				//////////////////////////////////////////

				////////////////////////////////////////////////////////////////////////
				/// Create new copy of joinable sides from map piece into AvailSidesGrid
				AvailSidesGrid [currGridPos [0], currGridPos [1]] = new int[4] { 0, 0, 0, 0 };
				System.Array.Copy (mapScript.joinableSides, AvailSidesGrid [currGridPos [0], currGridPos [1]], 4);
				////////////////////////////////////////////////////////////////////////

				//////////////////////////////////////////////////////////////////////
				/// figure out last move and change AvailSidesArray accordingly
				switch (lastMove) { 
				case "L":
					AvailSidesGrid [currGridPos [0], currGridPos [1]] [2] = 0;
					break;
				case "U":
					AvailSidesGrid [currGridPos [0], currGridPos [1]] [3] = 0;
					break;
				case "R":
					AvailSidesGrid [currGridPos [0], currGridPos [1]] [0] = 0;
					break;
				case "D":
					AvailSidesGrid [currGridPos [0], currGridPos [1]] [1] = 0;
					break;
				default:
					break;
				}
				//////////////////////////////////////////////////////////////////////

				/*
				for (int j = 0; j < walkedPathIndex; j++) {
					Debug.Log ("walkedPathList [" + j + "] = " + walkedPathList [j, 0] + " : " + walkedPathList [j, 1]);
					Debug.Log ("AvailSidesGrid: " + AvailSidesGrid [walkedPathList [j, 0], walkedPathList [j, 1]] [0]
					+ " " + AvailSidesGrid [walkedPathList [j, 0], walkedPathList [j, 1]] [1]
					+ " " + AvailSidesGrid [walkedPathList [j, 0], walkedPathList [j, 1]] [2]
					+ " " + AvailSidesGrid [walkedPathList [j, 0], walkedPathList [j, 1]] [3]);
				}
				Debug.Log ("---------------------------- Finsihed Placing tile --------");
				Debug.Log ("-----------------------------------------------------------");
				Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				Debug.Log ("");
				Debug.Log ("---------Searching for next place to lay --------");
				*/
				int locX = currGridPos [0];
				int locZ = currGridPos [1];

				////////////////////////////////////////////////////////////////
				/// change AvailSidesArray if next to bounds
				if (!(locX - 1 >= 0)) {
					AvailSidesGrid [locX, locZ] [0] = 0;
				}
				if (!(locZ + 1 < worldSizeZ)) {
					AvailSidesGrid [locX, locZ] [1] = 0;
				}
				if (!(locX + 1 < worldSizeX)) {
					AvailSidesGrid [locX, locZ] [2] = 0;
				}
				if (!(locZ - 1 >= 0)) {
					AvailSidesGrid [locX, locZ] [3] = 0;
				}
				///////////////////////////////////////////////////////////

				//Debug.Log ("shallowRefSides: " + AvailSidesGrid [locX, locZ] [0] + " " + AvailSidesGrid [locX, locZ] [1] + " " + AvailSidesGrid [locX, locZ] [2] + " " + AvailSidesGrid [locX, locZ] [3]);

				// Set new Spawn point
				spawn.transform.localPosition = GetNextSpot (locX, locZ);

				// Back Track if needed
				if (spawn.transform.localPosition == Vector3.zero) {
					//Debug.Log ("SHIT NO SPOTS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

					// back up through previous locations
					// its -2 because were already looking ahead for a spot
					int size = walkedPathIndex - 2;
					for (int j = size; j >= 0; j--) {

						int newlocX = walkedPathList [j, 0];
						int newlocZ = walkedPathList [j, 1];

						/*
						Debug.Log ("Looking at Grid [X]: " + newlocX + " [Z]: " + newlocZ);
						Debug.Log ("shallowRefSides2: " + AvailSidesGrid [newlocX, newlocZ] [0]
						+ " " + AvailSidesGrid [newlocX, newlocZ] [1]
						+ " " + AvailSidesGrid [newlocX, newlocZ] [2]
						+ " " + AvailSidesGrid [newlocX, newlocZ] [3]);
						*/

						// Set new Spawn point
						spawn.transform.localPosition = GetNextSpot (newlocX, newlocZ);

						// check if spot found
						if (spawn.transform.localPosition != Vector3.zero) {
							//Debug.Log ("Found and broken: with J: " + j);
							walkedPathIndex = j;
							break;
						}
						////////////////////////

						// FINAL NO MORE POSSIBILITIES
						if (j == 0) {
							i = totalMapCountPerLayer - 1;
							break;
						}
					}
				}

				// FINAL NO MORE POSSIBILITIES
				if (i == totalMapCountPerLayer - 1 || spawn.transform.localPosition == Vector3.zero) {
					Debug.Log ("Finished Map layer");
					break;
				}
			}
		}

		MapsFinishedLoading ();
		Destroy (spawn);
	}


	private void MapsFinishedLoading() {
		Debug.Log ("MapsFinishedLoading!!!!!!!!");
		mapsFinished = true;
		//BuildBasicGrid ();
		gameManagerScript.GAMEMASTER_GridStart ();
	}


	private Vector3 GetNextSpot(int locX, int locZ) {

		Vector3 newSpawnVect = new Vector3 (spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z);
		int[] tempArr = new int[3];


		if ((AvailSidesGrid [locX, locZ] [0] == 1) && (AvailSidesGrid [locX - 1, locZ] == null)) {
			//Debug.Log ("GOOD LEFT: ");
			lastMove = "L";
			AvailSidesGrid [locX, locZ] [0] = 0;
			tempArr = SpawnPosXYZGrid [locX - 1, locZ];
			newSpawnVect = new Vector3 (tempArr [0], tempArr [1], tempArr [2]);
			currGridPos [0] = locX - 1;
			currGridPos [1] = locZ;

		} else if ((AvailSidesGrid [locX, locZ] [1] == 1) && (AvailSidesGrid [locX, locZ + 1] == null)) {
			//Debug.Log ("GOOD UP: ");
			lastMove = "U";
			AvailSidesGrid [locX, locZ] [1] = 0;
			tempArr = SpawnPosXYZGrid [locX, locZ + 1];
			newSpawnVect = new Vector3 (tempArr [0], tempArr [1], tempArr [2]);
			currGridPos [0] = locX;
			currGridPos [1] = locZ + 1;

		} else if ((AvailSidesGrid [locX, locZ] [2] == 1) && (AvailSidesGrid [locX + 1, locZ] == null)) {
			//Debug.Log ("GOOD RIGHT: ");
			lastMove = "R";
			AvailSidesGrid [locX, locZ] [2] = 0;
			tempArr = SpawnPosXYZGrid [locX + 1, locZ];
			newSpawnVect = new Vector3 (tempArr [0], tempArr [1], tempArr [2]);
			currGridPos [0] = locX + 1;
			currGridPos [1] = locZ;

		} else if ((AvailSidesGrid [locX, locZ] [3] == 1) && (AvailSidesGrid [locX, locZ - 1] == null)) {
			//Debug.Log ("GOOD DOWN: ");
			lastMove = "D";
			AvailSidesGrid [locX, locZ] [3] = 0;
			tempArr = SpawnPosXYZGrid [locX, locZ - 1];
			newSpawnVect = new Vector3 (tempArr [0], tempArr [1], tempArr [2]);
			currGridPos [0] = locX;
			currGridPos [1] = locZ - 1;

		} else {

			newSpawnVect = Vector3.zero;
		}

		return newSpawnVect;
	}


	// Where maps are placed recorded in a 2Dgrid
	/*
	void BuildBasicGrid() {
		if (!mapsFinished) {
			mapsFinished = true;
			for (int i = 0; i < worldSizeZ; i++) {
				for (int j = 0; j < worldSizeX; j++) {
					if( AvailSidesGrid[i,j] != null) {
						basicMapGrid [i, j] = true;
						//Debug.Log ("basicMapGrid [ " + i + " , " + j + " ] = true ");
					}
				}
			}
		}
	}
	*/


}

