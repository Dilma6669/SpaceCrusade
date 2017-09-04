using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour {

	public List<Transform> playerObjects;

	// Use this for initialization
	void Start () {

		foreach (Transform child in transform) {
			playerObjects.Add (child);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
