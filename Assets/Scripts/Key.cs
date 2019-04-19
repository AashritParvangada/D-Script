using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	private void OnMouseDown() {
		Debug.Log("Gotcha");
		Color dimmed = new Color(50,50,50);
		GetComponent<SpriteRenderer>().color=dimmed;
	}

}
