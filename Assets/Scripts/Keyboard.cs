using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{

    List<Key> L_Keys = new List<Key>();

    // Use this for initialization
    void Start()
    {
        GetAllKeys();
    }

    void GetAllKeys()
    {
        foreach (Key _key in GetComponentsInChildren<Key>())
        {
            L_Keys.Add(_key);
        }

		Debug.Log(L_Keys.Count);
    }

	public void SwitchOffOthers(Key _keyToKeepOn)
	{
		foreach(Key _key in L_Keys)
		{
			if(_key != _keyToKeepOn)
			{
				_key.GetComponent<SpriteRenderer>().enabled=false;
			}
		}
	}

}
