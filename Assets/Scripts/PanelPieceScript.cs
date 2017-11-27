using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPieceScript : MonoBehaviour {

	private Renderer rend;

	public bool panelGoTransparent = false;
	public bool transFlag = false;

	private DefaultCubeScript cubeScript;

	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer> ();
		rend.material.color = Color.black;

		cubeScript = transform.parent.GetComponentInParent<DefaultCubeScript>();
	}

	void Update () {

		if (panelGoTransparent) {
			PanelPieceGoTransparent ();
			panelGoTransparent = false;
			transFlag = true;
		} else if (transFlag) {
			PanelPieceGoNotTransparent ();
			transFlag = false;
		}

	}
	

	void OnMouseDown() {

		cubeScript.CubeSelect ("Move");
		PanelPieceChangeColor("Green");

	}


	public void PanelPieceChangeColor(string color) {

		switch (color) { 
		case "Black":
			rend.material.color = Color.black;
			break;
		case "White":
			rend.material.color = Color.white;
			break;
		case "Green":
			rend.material.color = Color.green;
			break;
		default:
			break;
		}

	}


	void OnMouseOver() {
		if (cubeScript.cubeVisible) {
			PanelPieceChangeColor ("Green");
			cubeScript.CubeHighlight ("Move");
		}
	}
	void OnMouseExit() {
		if (cubeScript.cubeVisible) {
			PanelPieceChangeColor ("White");
			cubeScript.CubeUnHighlight ("Move");
		}
	}

	public void PanelPieceGoTransparent() {

		if (rend) {
			rend.material.shader = Shader.Find ("Transparent/Diffuse");
			Color tempColor = rend.material.color;
			tempColor.a = 0.3F;
			rend.material.color = tempColor;
		}
	}

	public void PanelPieceGoNotTransparent() {

		if (rend) {
			rend.material.shader = Shader.Find ("Standard");
			Color tempColor = rend.material.color;
			tempColor.a = 1F;
			rend.material.color = tempColor;
		}
	}


}
