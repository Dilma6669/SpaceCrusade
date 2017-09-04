using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighCameraController : MonoBehaviour {

	public float mouseRotationSpeed = 10f;                         // Horizontal turn speed.
	public float keysMovementSpeed = 10f;                           // Vertical turn speed.

	public float zoomMinFOV = 15f;
	public float zoomMaxFOV = 90f;
	public float zoomSensitivity = 10f;

	public float smooth = 10f;                                         // Speed of camera responsiveness.

	public float maxVerticalAngle = 0f;                               // Camera max clamp angle. 
	public float minVerticalAngle = 0f;  								 // Camera min clamp angle.

	private float h;                                
	private float v;    
	private float x;                                
	private float y;  

	private float angleH = 0;                                          // Float to store camera horizontal angle related to mouse movement.
	private float angleV = 0;                                          // Float to store camera vertical angle related to mouse movement.

	private float targetFOV;                                           // Target camera FIeld of View.
	private float targetMaxVerticalAngle;                              // Custom camera max vertical clamp angle. 

	PlayersManager playersManager;


	void Awake()
	{
		playersManager = FindObjectOfType<PlayersManager> ();

		// this has me confused but need it for camera rotation
		this.targetFOV = transform.GetComponentInChildren<Camera>().fieldOfView;

	}
		

	// Mouse rotation movement
	void LateUpdate()
	{
		if (Camera.main) {

			// Mouse rotation
			if (Input.GetMouseButton (2)) {
				// mouse look around
				x = Input.GetAxis ("Mouse X");
				y = Input.GetAxis ("Mouse Y");

				// Get mouse movement to orbit the camera.
				angleH += Mathf.Clamp (x, -1, 1) * mouseRotationSpeed;
				angleV += Mathf.Clamp (y, -1, 1) * mouseRotationSpeed;

				// Set vertical movement limit.
				angleV = Mathf.Clamp (angleV, minVerticalAngle, targetMaxVerticalAngle);

				// Set camera orientation..
				Quaternion aimRotation = Quaternion.Euler (-angleV, angleH, 0);
				transform.localRotation = aimRotation;

		
			} else {

				// mouse Zoom
			//	float FOV = Camera.main.fieldOfView;
				//FOV += Input.GetAxis ("Mouse ScrollWheel") * -zoomSensitivity;
				//FOV = Mathf.Clamp (FOV, zoomMinFOV, zoomMaxFOV);
				//Camera.main.fieldOfView = FOV;

				Vector3 tempVect = transform.localPosition;

				tempVect.y += Input.GetAxis ("Mouse ScrollWheel") * -zoomSensitivity;
				tempVect.z += Input.GetAxis ("Mouse ScrollWheel") * -zoomSensitivity;

				transform.localPosition = tempVect;

			}
		}

		SendRayToPlayers ();

	}


	// Keys hor/ver movement
	private void FixedUpdate()
	{
		// Basic Movement Player //
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		//Sets x and y basic movement
		transform.Translate (new Vector3 (keysMovementSpeed * h, 0, 0));
		transform.Translate (new Vector3 (0, 0, keysMovementSpeed * v));
	}


	private void SendRayToPlayers () {
		/*
		RaycastHit[] hits;
		Vector3 rayDirection;
		PanelPieceScript panelScript;


		foreach (Transform player in playersManager.playerObjects) {

			foreach (Transform hitBoxes in player.Find("Hit_Boxes")) {

				rayDirection = hitBoxes.position - transform.position;

				hits = Physics.RaycastAll (transform.position, rayDirection);

				for (int i = 0; i < hits.Length; i++) {

					RaycastHit hit = hits [i];

					if (hit.transform.GetComponent<PanelPieceScript> ()) {
						panelScript = hit.transform.GetComponent<PanelPieceScript> ();
						panelScript.panelGoTransparent = true;
					}
				}
			}
		}
	*/
	}
		

}
