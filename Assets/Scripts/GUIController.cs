using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {

	/*
	public bool layer03 = false;
	public bool layer02 = false;
	public bool layer01 = false;
	public bool layer00 = false;
	*/

	private GameObject mapManagerObject;

	private bool layerchange = false;

	public int currentLayer;


	// Use this for initialization
	void Start () {
		if (FindObjectOfType<MapManager> ()) {
			mapManagerObject = FindObjectOfType<MapManager> ().gameObject;
		}

	}

	// Update is called once per frame
	void Update () {

	}

	public void UpDownGUIButton(string upORdown) {

		if (upORdown == "UP") {
			currentLayer++;
			if (currentLayer >= mapManagerObject.transform.childCount-1) {
				currentLayer = mapManagerObject.transform.childCount-1;
			}
		}
		if (upORdown == "DOWN") {
			currentLayer--;
			if (currentLayer <= 1) {
				currentLayer = 1;
			}
		}
		ChangeDisplayLayer ();
	}



	// NEEDS SERIOUS WORK, NO MAGIC NUMMBERS!!
	public void ChangeDisplayLayer() {

		// reset all map layers, not entrance layer
		for (int i = 1; i < mapManagerObject.transform.childCount; i++) {
			if (mapManagerObject.transform.GetChild (i).gameObject) {
				mapManagerObject.transform.GetChild (i).gameObject.transform.localScale = new Vector3 (0, 0, 0);
			}
		}
		////////////////

		// 0 is the entrance object, map layers are from 1 onwards
		for (int i = 0; i <= currentLayer; i++) {
			if (mapManagerObject.transform.GetChild (i)) {
				mapManagerObject.transform.GetChild (i).gameObject.transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}

}
