using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputController : MonoBehaviour {

	private GameManager gameManager;

	private RaycastHit rayHit;
	private Ray ray;
	public Camera viewCamera;

	public GameObject mouseFollPrefab;
	private GameObject mouseFollower;
	public GameObject playerArrowPrefab;
	private GameObject playerArrow;

	public bool setArrowPos;

	public Vector3 mousePosition;
	public Vector3 mousePosDiff;
	private int mouseFollYOffset;


	public Vector3 mousePosHELD;
	public Vector3 mouseRotHELD;

	// Use this for initialization
	void Start () {

		gameManager = FindObjectOfType<GameManager> ();

		if (mouseFollPrefab != null) {
			mouseFollower = Instantiate (mouseFollPrefab, transform, false);
			playerArrow = Instantiate (playerArrowPrefab, transform, false);
		}

		mousePosition = new Vector3 (0, 0, 0);
		mouseFollYOffset = 5;

	}
	
	// Update is called once per frame
//	void Update () {
//		
//		if (mouseFollPrefab != null) {
//			mouseFollower.transform.position = mousePosition;
//	
//
//			if (gameManager.playerSelected) {
//				ray = viewCamera.ScreenPointToRay (Input.mousePosition); //Gives screen position (mouse position), return a ray from the camera to that position to infinity.
//				if (Physics.Raycast (ray, out rayHit)) { //Gives OUT raydistance then assigns it a variable. Will return true if the ray itersects with a collider and will also know length.
//					Debug.DrawLine (ray.origin, rayHit.point, Color.red);
//					mousePosition = new Vector3 (rayHit.point.x, (rayHit.point.y + mouseFollYOffset), rayHit.point.z);
//				}
//			}
//
//
//			if (Input.GetMouseButtonUp (2)) {
//				setArrowPos = false;
//			}
//			if (Input.GetMouseButtonDown (2) && setArrowPos == false) {
//				setArrowPos = true;
//				mousePosHELD = mousePosition;
//				playerArrow.transform.position = mousePosHELD;
//			}
//			if (setArrowPos == false) {
//				playerArrow.transform.position = mousePosition;
//			}
//		}
//
//
//
//	}
}
