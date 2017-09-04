using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

	private List<GameObject> openNodesList; // the set of nodes to be evaluated
	private GameObject startNode;
	private GameObject finishNode;
	private GameObject[,] OpenSetGrid;

	private List<GameObject> closedNodesList; // The set of nodes already evaluated

	private int openSetSizeSmallZ;
	private int openSetSizeSmallX;
	private int openSetSizeLargeZ;
	private int openSetSizeLargeX;

	private int openSetSizeFinalZ;
	private int openSetSizeFinalX;

	private GameManager gameManagerScript;
	private GridManager gridManagerScript;
	private DefaultCubeScript cubeScript;

	// Use this for initialization
	void Start () {

		gameManagerScript = FindObjectOfType<GameManager> ();
		gridManagerScript = FindObjectOfType<GridManager> ();

	}
		
	public void FindPath(List<GameObject> _openNodes, GameObject _startNode, GameObject _finishNode) {
		openNodesList  = _openNodes;
		startNode  = _startNode;
		finishNode = _finishNode;

		SetOpenSetSize ();
	}



	void SetOpenSetSize() {

		openSetSizeSmallZ = gameManagerScript.MapSizeGetter("Z") * gameManagerScript.numCubesInZ;
		openSetSizeSmallX = gameManagerScript.MapSizeGetter("X") * gameManagerScript.numCubesInX;
		openSetSizeLargeZ = -1;
		openSetSizeLargeX = -1;
		OpenSetGrid = null;

		int tempSmallZ = openSetSizeSmallZ;
		int tempSmallX = openSetSizeSmallX;
		int tempLargeZ = openSetSizeLargeZ;
		int tempLargeX = openSetSizeLargeX;

		foreach (GameObject node in openNodesList) {
			cubeScript = node.GetComponent<DefaultCubeScript> ();

			//Debug.Log ("nodeZX: " + cubeScript.gridLocZ + "," + cubeScript.gridLocX);

			if (cubeScript.gridLocZ >= 0 && cubeScript.gridLocX >= 0) {
				//find smallest Z
				if (cubeScript.gridLocZ < tempSmallZ) {
					tempSmallZ = cubeScript.gridLocZ;
				}
				//openSetSizeSmallZ = tempSmallZ;

				//find smallest X
				if (cubeScript.gridLocX < tempSmallX) {
					tempSmallX = cubeScript.gridLocX;
				}
				//openSetSizeSmallX = tempSmallX;

				//find Largest Z
				if (cubeScript.gridLocZ > tempLargeZ) {
					tempLargeZ = cubeScript.gridLocZ;
				}
				//openSetSizeLargeZ = tempLargeZ;

				//find Largest X
				if (cubeScript.gridLocX > tempLargeX) {
					tempLargeX = cubeScript.gridLocX;
				}
				//openSetSizeLargeX = tempLargeX;
			}
		}

		openSetSizeSmallZ = tempSmallZ;
		openSetSizeSmallX = tempSmallX;
		openSetSizeLargeZ = tempLargeZ;
		openSetSizeLargeX = tempLargeX;
		//Debug.Log ("SetOpenSetSizeZX: " + openSetSizeSmallZ + "," + openSetSizeSmallX + " -- " + openSetSizeLargeZ + "," + openSetSizeLargeX);

		openSetSizeFinalZ = openSetSizeLargeZ - openSetSizeSmallZ + 1;
		openSetSizeFinalX = openSetSizeLargeX - openSetSizeSmallX + 1;


		//BuildOpenSetGrid ();

	}



	void BuildOpenSetGrid() {

		OpenSetGrid = new GameObject[openSetSizeFinalZ, openSetSizeFinalX];
		//Debug.Log ("BuildOpenSetGridSize: " + openSetSizeFinalZ + " -- " + openSetSizeFinalX);

		int lookUpZ = openSetSizeSmallZ;
		int lookUpX = openSetSizeSmallX;

		foreach (GameObject node in openNodesList) {
			cubeScript = node.GetComponent<DefaultCubeScript> ();

			//Debug.Log ("nodeZX: " + cubeScript.gridLocZ + "," + cubeScript.gridLocX);
			//Debug.Log ("putting in OpenSetGrid at ZX: " + (cubeScript.gridLocZ - openSetSizeSmallZ) + "," + (cubeScript.gridLocX - openSetSizeSmallX));
			if (cubeScript.gridLocZ >= 0 && cubeScript.gridLocX >= 0) {
				OpenSetGrid [cubeScript.gridLocZ - openSetSizeSmallZ, cubeScript.gridLocX - openSetSizeSmallX] = node;
			}
		}

		/*
		for (int i = 0; i < openSetSizeFinalZ; i++) {

			Debug.Log ("NEW ROW --------------------------------");

			for (int j = 0; j < openSetSizeFinalX; j++) {

				Debug.Log ("Z: " + i + ":" + "X: " + j + " == " + OpenSetGrid[i,j]);

			}
		}
		*/




	}


}
