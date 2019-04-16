using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void GetAllKeys()
	{
		foreach(GameObject _key in GetComponentsInChildren<GameObject>())
		{
			//Add key to list.
		}
	}

}
