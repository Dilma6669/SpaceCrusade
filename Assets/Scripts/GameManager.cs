using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	// THE MASTER /////////////////////
	MASTER master;
	OBSERVER observer;
	////////////////////////////////////
	private int worldSizeX;
	private int worldSizeZ;
	private int worldSizeY;

	public int numCubesInX;
	public int numCubesInZ;
	public int numCubesInY;

	public int totalCubesInX;
	public int totalCubesInZ;
	public int totalCubesInY;

	public GameObject player;

	//public bool playerSelected = false;

	private GridManager gridManager;
	//private MapManager mapManager;
	private CubeManager cubeManager;
	//private GUIController guiController;
	//private TestPlayerScript testPlayerScript;

	//public bool layMaps;

	//public bool activateGrid;
	//public bool deactivateGrid;

	//public List<GameObject[,]> WorldGrid_CubeRef_LayersList;

	//public bool GAMESTART = false;

	// Use this for initialization
	void Start () {

		// THE MASTER /////////////////////
		// designed as a easy user input for designer
		master = FindObjectOfType<MASTER> ();
		observer = FindObjectOfType<OBSERVER> ();
		////////////////////////////////////
		worldSizeX = master.worldSizeX;
		worldSizeZ = master.worldSizeZ;
		worldSizeY = master.worldSizeY;

		totalCubesInX = worldSizeX * numCubesInX;
		totalCubesInZ = worldSizeZ * numCubesInZ;
		totalCubesInY = worldSizeY * numCubesInY;


		gridManager = FindObjectOfType<GridManager> ();
		//mapManager = FindObjectOfType<MapManager> ();
		cubeManager = FindObjectOfType<CubeManager> ();
		//guiController = FindObjectOfType<GUIController> ();
		//testPlayerScript = FindObjectOfType<TestPlayerScript> ();

		//WorldGrid_CubeRef_LayersList = new List<GameObject[,]>();

		////////////////////////////

//		if (activateGrid) {
//			layMaps = false;
//			activateGrid = true;
//			GAMEMASTER_GridStart ();
//		} else if (layMaps) {
//			layMaps = true;
//			activateGrid = false;
//			GAMEMASTER_MapStart ();
//		}

		GAMEMASTER_GridStart ();
	}

	/*
	void Update() {

	}
	*/



//	public void GAMEMASTER_MapStart() {
//		Debug.Log ("MAPS_START");
//		layMaps = true;
//		activateGrid = false;
//		if (mapManager != null) {
//			mapManager.CreateMaps ();
//		}
//	}
//

	public void GAMEMASTER_GridStart() {
		Debug.Log ("GRID_START");
		//layMaps = false;
		//activateGrid = true;
		if (gridManager != null) {
			gridManager.BuildGridObjLookup ();
			cubeManager.AttachCubeToLoc ();
			GAMEMASTER_StartGame ();
			//gridManager.ActivateGrid ();
		}
	}


	public void GAMEMASTER_GridFinish() {
		//layMaps = false;
		//activateGrid = false;
		if (gridManager != null) {
			//if (deactivateGrid) {
				Debug.Log ("GRID_DEACTIVATE");
				gridManager.DeactivateGrid ();
			//} else {
			//	GAMEMASTER_GUI ();
			//}
		}
	}


	public void GAMEMASTER_GUI() {
		Debug.Log ("GUI_ARRANGING");
		//guiController.ChangeDisplayLayer ();
		//GAMEMASTER_StartGame ();
	}


	public void GAMEMASTER_StartGame() {
		Debug.Log ("GAME_START!!!!");
		observer.StartGame(true);
		TestPlayerScript playerScript = FindObjectOfType<TestPlayerScript> ();
		playerScript.PutPlayerIntoStartPosition ();
	}

}
