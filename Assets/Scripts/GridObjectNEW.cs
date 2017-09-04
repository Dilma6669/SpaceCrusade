using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectNEW : MonoBehaviour {

	public int gridLocZ;
	public int gridLocX;
	public int gridLocY;

	public GameObject cubeReference;
	private DefaultCubeScript cubeScript;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
