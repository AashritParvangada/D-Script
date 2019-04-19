using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{

	Key keyBeingUsed;
	Transform T_oldKeyPosition;
	[SerializeField] Vector3 T_CenterPosition = new Vector3 (0, -4, 0);
	[SerializeField] float TS_NewScale = 1;

	bool b_KeyActive = false;
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

	public void KeyClicked(Key _keyClicked)
	{
		if(!b_KeyActive)
		{
		SwitchOffOthers(_keyClicked);
		CenterKey(_keyClicked);
		b_KeyActive=true;
		}
	}

	void SwitchOffOthers(Key _keyToKeepOn)
	{
		foreach(Key _key in L_Keys)
		{
			if(_key != _keyToKeepOn)
			{
				_key.GetComponent<SpriteRenderer>().enabled=false;
			}
		}
	}

	void CenterKey(Key _keyToCenter)
	{
		T_oldKeyPosition = _keyToCenter.transform;
		_keyToCenter.transform.position = T_CenterPosition;
		_keyToCenter.transform.localScale = new Vector3(TS_NewScale, TS_NewScale, TS_NewScale);
	}

}
