using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour {

	public List<GameObject> mapsPlacedRef;
	public List<GameObject> pathfindingCubeList;
	private Transform cubesGround;

	private Vector3 startingPos;

	public bool playerActivated;

	private Renderer rend;
	private GameObject sensor;

	private GameManager gameManagerScript;
	private MapManager mapManagerScript;
	private PathFinding pathFindingScript;

	private CameraManager cameraManagerScript;

	public GameObject currentStandingCube;
	public GameObject targetCubeObject;
	public Vector3 targetCubePos;
	public bool playerMoving = false;

	private int moveSpeed = 30;
	private int rotateDamping = 6;

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


	// Use this for initialization
	void Start () {

		rend = transform.GetComponentInChildren<Renderer> ();
		sensor = transform.Find ("Sensor").gameObject;

		transform.position = Vector3.zero;
		transform.position = currentStandingCube.transform.position;

		gameManagerScript = FindObjectOfType<GameManager> ();
		mapManagerScript = FindObjectOfType<MapManager> ();
		cameraManagerScript = FindObjectOfType<CameraManager> ();
		pathFindingScript = FindObjectOfType<PathFinding> ();

		mapsPlacedRef = new List<GameObject> ();
		pathfindingCubeList = new List<GameObject> ();

		SetManagerCameras ();

		leftArmAnimator = leftArmJoint.transform.GetComponent<Animator>();
		rightArmAnimator = RightArmJoint.transform.GetComponent<Animator>();
		leftLegAnimator = leftLegJoint.transform.GetComponent<Animator>();
		rightLegAnimator = RightLegJoint.transform.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (playerMoving) {
			if (transform.position == targetCubePos) {
				playerMoving = false;
				PlayerAnimator ();
				currentStandingCube = targetCubeObject;
				targetCubeObject = null;
				TestRayCast ();
			} else if (transform.position != targetCubePos) {
				var lookPos = targetCubePos - transform.position;
				var rotation = Quaternion.LookRotation (lookPos);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotateDamping);
				this.gameObject.transform.position = Vector3.MoveTowards (transform.position, targetCubePos, (moveSpeed * Time.deltaTime));
			}
		}
	}
		


	public void PlayerOnGameStart() {
		mapsPlacedRef = mapManagerScript.mapsPlaced;
	}


		
	void OnMouseDown() {
		if (gameManagerScript.GAMESTART) {
			PlayerActivated ();
		}
	}


	void PlayerActivated() {
		Debug.Log ("PlayerActivated");
		playerActivated = true;
		rend.material.color = Color.green;
		TestRayCast ();
		gameManagerScript.playerSelected = true;
	}


	public void MovePlayer(GameObject _targetCube) {

		if (playerActivated) {
			targetCubeObject = _targetCube;
			targetCubePos = _targetCube.transform.position;
			pathFindingScript.FindPath (pathfindingCubeList, currentStandingCube, _targetCube);
			playerMoving = true;
			PlayerAnimator ();
		}
	}


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
		


	void TestRayCast() {
		pathfindingCubeList.Clear ();
		bool cubeVisible = false;

		RaycastHit rayHit;
		Vector3 rayDirection;

		for (int LAYER = 0; LAYER <= gameManagerScript.MapSizeGetter("Y"); LAYER++) {
			//ground Level
			foreach (GameObject plane in mapsPlacedRef) {
				if (plane.transform.Find ("CubesGround_" + LAYER)) {
					cubesGround = plane.transform.Find ("CubesGround_" + LAYER).transform;
					foreach (Transform row in cubesGround) {
						foreach (Transform DefaultCube in row) {
							cubeVisible = false;

							rayDirection = DefaultCube.position - sensor.transform.position;

							if (Physics.Linecast (sensor.transform.position, DefaultCube.position, out rayHit)) {
							
								foreach (Transform panel in DefaultCube) {
									if (cubeVisible == false) {
										if (panel.transform.tag != "CubeSelect") {
											rayDirection = panel.position - sensor.transform.position;
											if (Physics.Linecast (transform.position, panel.position, out rayHit)) {
												//Debug.Log ("BLocked");
												Debug.DrawRay (sensor.transform.position, rayDirection, Color.cyan);
											} else {
												//Debug.Log ("Hit");
												cubeVisible = true;
												Debug.DrawRay (sensor.transform.position, rayDirection, Color.cyan);
											}
										}
									}
								}

							} else {
								Debug.DrawRay (transform.position, rayDirection, Color.cyan);
								cubeVisible = true;
							}

							if (cubeVisible) {
								DefaultCube.GetComponent<DefaultCubeScript> ().CubeVisible ();
								pathfindingCubeList.Add (DefaultCube.gameObject);
							} else {
								DefaultCube.GetComponent<DefaultCubeScript> ().CubeNotVisible ();
							}
						}
					}
				}
			}
		}

	}


	public void SetManagerCameras() {

		GameObject highcam = transform.Find ("Player_HighCameraBox").gameObject;
		GameObject lowcam = transform.Find ("Player_LowCameraBox").gameObject;

		cameraManagerScript.SetMainCamerasObjectToFollow (highcam, lowcam);

	}

}
