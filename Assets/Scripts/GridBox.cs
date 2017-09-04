using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox : MonoBehaviour {

	public GridManager gridManager;
	public int gridLocZ;
	public int gridLocX;
	public int gridLocY;

	public List<GameObject> neighbours = new List<GameObject>();

	/////////////////////////////////
	// neighbur connections go from bottom corner like way grid is built
	// 000 = X, Z, Y
	public GameObject neighbour000;
	public GameObject neighbour100;
	public GameObject neighbour200;

	public GameObject neighbour010;
	public GameObject neighbour110;
	public GameObject neighbour210;

	public GameObject neighbour020;
	public GameObject neighbour120;
	public GameObject neighbour220;
	/////////////////////////////////
	public GameObject neighbour001;
	public GameObject neighbour101;
	public GameObject neighbour201;

	public GameObject neighbour011;
	//public GameObject neighbour111; //THIS IS THE OBJECT//
	public GameObject neighbour211;

	public GameObject neighbour021;
	public GameObject neighbour121;
	public GameObject neighbour221;
	/////////////////////////////////
	public GameObject neighbour002;
	public GameObject neighbour102;
	public GameObject neighbour202;

	public GameObject neighbour012;
	public GameObject neighbour112;
	public GameObject neighbour212;

	public GameObject neighbour022;
	public GameObject neighbour122;
	public GameObject neighbour222;
	/////////////////////////////////


	public GameObject cubeReference;
	private DefaultCubeScript cubeScript;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		gridManager = FindObjectOfType<GridManager> ();
	}

	public void GetNeighbourConnections() {

		Vector3 ownVect = new Vector3(gridLocX, gridLocZ, gridLocY);

		neighbours.Add (neighbour000 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y - 1)]);
		neighbours.Add (neighbour100 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y - 1)]);
		neighbours.Add (neighbour200 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y - 1)]);

		neighbours.Add (neighbour010 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y - 1)]);
		neighbours.Add (neighbour110 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y - 1)]);
		neighbours.Add (neighbour210 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y - 1)]);

		neighbours.Add (neighbour020 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y - 1)]);
		neighbours.Add (neighbour120 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y - 1)]);
		neighbours.Add (neighbour220 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y - 1)]);

		/////////////////////////////////
		neighbours.Add (neighbour001 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y + 0)]);
		neighbours.Add (neighbour101 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y + 0)]);
		neighbours.Add (neighbour221 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y + 0)]);

		neighbours.Add (neighbour011 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y + 0)]);
		//neighbours.Add (neighbour111 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y + 0)]);
		neighbours.Add (neighbour211 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y + 0)]);

		neighbours.Add (neighbour021 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y + 0)]);
		neighbours.Add (neighbour121 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y + 0)]);
		neighbours.Add (neighbour212 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y + 0)]);
		/////////////////////////////////

		neighbours.Add (neighbour002 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y + 1)]);
		neighbours.Add (neighbour102 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y + 1)]);
		neighbours.Add (neighbour202 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y + 1)]);

		neighbours.Add (neighbour012 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y + 1)]);
		neighbours.Add (neighbour112 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y + 1)]);
		neighbours.Add (neighbour212 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y + 1)]);

		neighbours.Add (neighbour022 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y + 1)]);
		neighbours.Add (neighbour122 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y + 1)]);
		neighbours.Add (neighbour222 = (GameObject)gridManager.gridObjLookup[new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y + 1)]);
		/////////////////////////////////

			
	}

	void SendRayCastToNeighbour() {

		RaycastHit rayHit;
		Vector3 rayDirection;

		foreach (GameObject child in neighbours) {

			rayDirection = child.transform.position - transform.position;

			if (Physics.Linecast (transform.position, child.transform.position, out rayHit)) {
				Debug.Log ("BLocked");
				Debug.DrawRay (transform.position, rayDirection, Color.cyan);
			} else {
				Debug.Log ("Hit");
				Debug.DrawRay (transform.position, rayDirection, Color.cyan);
			}
		}


	}

//	void OnTriggerEnter(Collider other) {
//		if (other.tag == "LocSettingCollider") {
//			cubeReference = other.transform.parent.gameObject;
//			cubeScript = cubeReference.transform.GetComponent<DefaultCubeScript> ();
//			cubeScript.gridLocX = gridLocX;
//			cubeScript.gridLocZ = gridLocZ;
//			cubeScript.gridLocY = gridLocY;
//			// set cube reference in World Grid
//			gameManager.WorldGrid_CubeRef_LayersList[gridLocY][gridLocX, gridLocZ] = cubeReference;
//
//			other.gameObject.SetActive (false);
//			this.gameObject.SetActive (false);
//
//			cubeReference = null;
//		}
//	}
}
