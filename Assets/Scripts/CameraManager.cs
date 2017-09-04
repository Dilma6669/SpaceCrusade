using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Camera highCam;
	public Camera lowCam;

	Transform managerHighCamBox;
	Transform managerLowCamBox;

	public GameObject playerHighCamBox; // dont do anything with this 
	public GameObject playerLowCamBox; // BUT this follows player around


	// Use this for initialization
	void Start () {

		managerHighCamBox = transform.Find ("Manager_HighCameraBox").transform;
		managerLowCamBox = transform.Find ("Manager_LowCameraBox").transform;
		
	}
	
	// Update is called once per frame
	void Update () {

		// Changing camera views
		if (Input.GetKeyDown(KeyCode.R)) {
			if (highCam.isActiveAndEnabled) {
				highCam.gameObject.SetActive (false);
				lowCam.gameObject.SetActive (true);
			} else if (lowCam.isActiveAndEnabled) {
				lowCam.gameObject.SetActive (false);
				highCam.gameObject.SetActive (true);
			}
		}

		managerLowCamBox.position = playerLowCamBox.transform.position;
		managerLowCamBox.rotation = playerLowCamBox.transform.rotation;

	}



	public void SetMainCamerasObjectToFollow(GameObject highCam, GameObject lowCam) {
		//Debug.Log ("Cameras set to player");
		//Debug.Log ("highCam.position:  " + highCam.position.x + " , " + highCam.position.y + " , " + highCam.position.z );
		//Debug.Log ("lowCam.position:  " + lowCam.position.x + " , " + lowCam.position.y + " , " + lowCam.position.z );
		managerHighCamBox.position = highCam.transform.position;
		managerHighCamBox.rotation = highCam.transform.rotation; 

		//managerLowCamBox.position = lowCam.transform.position;
		//managerLowCamBox.rotation = lowCam.transform.rotation;
	}
}