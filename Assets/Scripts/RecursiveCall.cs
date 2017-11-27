using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveCall : MonoBehaviour {

	public void Recursive(GameObject originNode, int spread) {
		if (spread <= 0) {
			return;
		}
		RecursiveCall recursiveScript = FindObjectOfType<RecursiveCall> ();
		int newSpread = spread--;

		originNode.GetComponent<GridBox> ().SetLinked (true);
		List<GameObject> legalNeighboursRef = originNode.GetComponent<GridBox> ().GetLegalNeighbours ();
		foreach (GameObject neighbour in legalNeighboursRef) {
			recursiveScript.Recursive (neighbour, newSpread);
		}
	}

}
