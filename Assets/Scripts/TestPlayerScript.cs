using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour {

	//public List<GameObject> mapsPlacedRef;
	//public List<GameObject> pathfindingCubeList;
	//private Transform cubesGround;

	//private Vector3 startingPos;

	public bool playerActivated;

	public Dictionary<string, int> playerStats;

	private Renderer rend;
	private GameObject sensor;

	OBSERVER Observer;

//	private GameManager gameManagerScript;
//	private MapManager mapManagerScript;
	GridManager gridManager;
//	private PathFinding pathFindingScript;

//	private CameraManager cameraManagerScript;

	public Vector3 currentGridPos;
	public Vector3 currentPosVect;
	public Vector3 targetGridPos;
	public Vector3 targetPosVect;
	public bool playerMoving = false;

	private int moveSpeed = 30;
	private int rotateDamping = 6;

	public int playerRotation;

	//// Animation stuff /////
	public GameObject leftArmJoint;
	public GameObject RightArmJoint;
	public GameObject leftLegJoint;
	public GameObject RightLegJoint;

	Animator leftArmAnimator;
	Animator rightArmAnimator;
	Animator leftLegAnimator;
	Animator rightLegAnimator;
	///////////////////////////

	public int forcePlayerX;
	public int forcePlayerZ;	
	public int forcePlayerY;


	// Use this for initialization
	void Start () {

		rend = transform.GetComponentInChildren<Renderer> ();
		sensor = transform.Find ("Sensor").gameObject;

		playerStats = new Dictionary<string, int> {{"M", 4},{"WS", 4}, {"BS", 4},{"S", 4}, {"T", 4},{"W", 4},{"I", 4},{"A", 4}, {"LD", 4},{"C", 1}, {"F", 0}};

		//transform.position = Vector3.zero;
		//transform.position = currentStandingCube.transform.position;

		Observer = FindObjectOfType<OBSERVER> ();

//		gameManagerScript = FindObjectOfType<GameManager> ();
//		mapManagerScript = FindObjectOfType<MapManager> ();
		gridManager = FindObjectOfType<GridManager> ();
//		cameraManagerScript = FindObjectOfType<CameraManager> ();
//		pathFindingScript = FindObjectOfType<PathFinding> ();

		//mapsPlacedRef = new List<GameObject> ();
		//pathfindingCubeList = new List<GameObject> ();

		SetManagerCameras ();

		leftArmAnimator = leftArmJoint.transform.GetComponent<Animator>();
		rightArmAnimator = RightArmJoint.transform.GetComponent<Animator>();
		leftLegAnimator = leftLegJoint.transform.GetComponent<Animator>();
		rightLegAnimator = RightLegJoint.transform.GetComponent<Animator>();

		Quaternion target = Quaternion.Euler(0, 0, playerRotation);
		this.transform.rotation = target;

		//Invoke ("ForcePlayerMoveAtStart", 1.0f);
		
	}

	public void PutPlayerIntoStartPosition() {
		//ForceMovePlayer (new Vector3 (forcePlayerX, forcePlayerZ, forcePlayerY));
		transform.position = (Vector3)gridManager.GridLocToWorldLocLookup [new Vector3 (forcePlayerX, forcePlayerZ, forcePlayerY)];
		currentGridPos = new Vector3 (forcePlayerX, forcePlayerZ, forcePlayerY);
		currentPosVect = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (playerMoving) {
			if (transform.position == targetPosVect) {
				MovePlayerFinish ();
			} else if (transform.position != targetPosVect) {
				var lookPos = targetPosVect - transform.position;
				var rotation = Quaternion.LookRotation (lookPos);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotateDamping);
				this.gameObject.transform.position = Vector3.MoveTowards (transform.position, targetPosVect, (moveSpeed * Time.deltaTime));
			}
		}
	}
		


//	public void PlayerOnGameStart() {
//		mapsPlacedRef = mapManagerScript.mapsPlaced;
//	}


		
	void OnMouseDown() {
		//if (gameManagerScript.GAMESTART) {
			PlayerActivated ();
		//}
	}


	void PlayerActivated() {
		Debug.Log ("PlayerActivated");
		playerActivated = true;
		rend.material.color = Color.green;
		Observer.UpdatePlayerSelected(this.gameObject);
		gridManager.SendOutRays(true, currentGridPos, playerStats["M"]);
		//GameObject obj = (GameObject)gridManagerScript.GridLocToGridObjLookup[new Vector3 (currentCubePos.x, currentCubePos.z, currentCubePos.y)];
		//obj.GetComponent<GridBox> ().linked = true;
		//MovePlayerStart ();
	}



//	public void MovePlayerStart() {
//		GameObject obj = (GameObject)gridManager.GridLocToGridObjLookup[new Vector3 (currentGridPos.x, currentGridPos.z, currentGridPos.y)];
//		obj.GetComponent<GridBox> ().linked = false;
//		gridManager.ClearConnectionsForObjects ();
//		ForceMovePlayer(new Vector3 (forcePlayerX, forcePlayerZ, forcePlayerY));
//		//pathFindingScript.FindPath (transform.position, targetCubePos);
//	}

	public void ForceMovePlayer(Vector3 posToMoveTo) {
		targetGridPos = posToMoveTo;
		targetPosVect = (Vector3)gridManager.GridLocToWorldLocLookup[targetGridPos];

		playerMoving = true;
		PlayerAnimator ();
	}

	public void MovePlayerFinish() {
		Debug.Log ("Player finish move");
		playerMoving = false;
		PlayerAnimator ();
		currentGridPos = targetGridPos;
		currentPosVect = transform.position;
		gridManager.SendOutRays(true, currentGridPos, playerStats["M"]);
		//targetPosVect = (Vector3)new Vector3 (-1, -1, -1);
		//targetGridPos = (Vector3)new Vector3 (-1, -1, -1);
		//gridManager.SendOutRays(false, currentGridPos);
		//CheckPlayerCubeConnections();
	}


//	public void MovePlayer(Vector3 _targetCube) {
//		Debug.Log ("MovePlayer");
//		if (playerActivated) {
//			//targetCubeObject = _targetCube;
//			targetGridPos = _targetCube;
//			//pathFindingScript.FindPath (pathfindingCubeList, transform.position, _targetCube);
//			playerMoving = true;
//			PlayerAnimator ();
//		}
//	}


	public void PlayerAnimator() {
		if (playerMoving) {
			leftArmAnimator.enabled = true;
			rightArmAnimator.enabled = true;
			leftLegAnimator.enabled = true;
			rightLegAnimator.enabled = true;
		} else {
			leftArmAnimator.enabled = false;
			rightArmAnimator.enabled = false;
			leftLegAnimator.enabled = false;
			rightLegAnimator.enabled = false;
		}
	}
		

//	void CheckPlayerCubeConnections() {
//		Debug.Log ("CheckPlayerCubeConnections");
//
//	}


//	void TestRayCast() {
//		Debug.Log ("TestRayCast");
//		pathfindingCubeList.Clear ();
//		bool cubeVisible = false;
//
//		RaycastHit rayHit;
//		Vector3 rayDirection;
//
//		for (int LAYER = 0; LAYER <= gameManagerScript.MapSizeGetter("Y"); LAYER++) {
//			//ground Level
//			foreach (GameObject plane in mapsPlacedRef) {
//				if (plane.transform.Find ("CubesGround_" + LAYER)) {
//					cubesGround = plane.transform.Find ("CubesGround_" + LAYER).transform;
//					foreach (Transform row in cubesGround) {
//						foreach (Transform DefaultCube in row) {
//							cubeVisible = false;
//
//							rayDirection = DefaultCube.position - sensor.transform.position;
//
//							if (Physics.Linecast (sensor.transform.position, DefaultCube.position, out rayHit)) {
//							
//								foreach (Transform panel in DefaultCube) {
//									if (cubeVisible == false) {
//										if (panel.transform.tag != "CubeSelect") {
//											rayDirection = panel.position - sensor.transform.position;
//											if (Physics.Linecast (transform.position, panel.position, out rayHit)) {
//												//Debug.Log ("BLocked");
//												Debug.DrawRay (sensor.transform.position, rayDirection, Color.cyan);
//											} else {
//												//Debug.Log ("Hit");
//												cubeVisible = true;
//												Debug.DrawRay (sensor.transform.position, rayDirection, Color.cyan);
//											}
//										}
//									}
//								}
//
//							} else {
//								Debug.DrawRay (transform.position, rayDirection, Color.cyan);
//								cubeVisible = true;
//							}
//
//							if (cubeVisible) {
//								DefaultCube.GetComponent<DefaultCubeScript> ().CubeVisible ();
//								pathfindingCubeList.Add (DefaultCube.gameObject);
//							} else {
//								DefaultCube.GetComponent<DefaultCubeScript> ().CubeNotVisible ();
//							}
//						}
//					}
//				}
//			}
//		}
//
//	}


	public void SetManagerCameras() {

		GameObject highcam = transform.Find ("Player_HighCameraBox").gameObject;
		GameObject lowcam = transform.Find ("Player_LowCameraBox").gameObject;

		//cameraManagerScript.SetMainCamerasObjectToFollow (highcam, lowcam);

	}

}
