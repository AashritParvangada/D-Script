using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    Transform t_oldKeyPosition;     //Used to set the key back to the original when the keyboard is reset (i.e. when the answer is correct or incorrect)

    [SerializeField] Vector3 V3_CenterPosition = new Vector3(0, -4, 0); //Where the center position is. Can be edited.
    [SerializeField] float F_NewScale = 1; //What the center scale is. Can be edited.

    bool b_NormalKeyClicked = false; //Is turned on when a normal key is clicked.
    bool b_DiacriticKeyClicked = false; //Is turned on when a diacritic key is clicked.
    List<Key> key_List_RegularKeys = new List<Key>(); //This is a list of all the keys in the keyboard that are regular. Used for on/off right now.
    List<Key> key_List_DiacriticKeys = new List<Key>(); //A list of all the keys in they keyboard that are diacritic. Used for on/off right now.

    string s_Letter; //This is the string that we'll used to check whether the input is correct or not. If the input is wrong, we'll reset the keyboard in a function.


    // Use this for initialization
    void Start()
    {
        GetAllKeys();
		SwitchOnOffDiacritics(b_NormalKeyClicked);
    }

    void GetAllKeys()
    {
        foreach (Key _key in GetComponentsInChildren<Key>())
        {
            if (_key.isDiacritic)
            {
				key_List_DiacriticKeys.Add(_key);
            }
            else
            {
                key_List_RegularKeys.Add(_key);
            }
        }

    }

    //Can turn off or on Diacritic Keys. Used in both normal key clicked and diacritic key clicked.
	void SwitchOnOffDiacritics(bool _keyActive)
	{
		foreach(Key _key in key_List_DiacriticKeys)
		{
			_key.gameObject.SetActive(_keyActive);
		}
	}

    //If a normal key is clicked, turn off other normal keys, pull the key to the center of the screen, and surround it with diacritic keys.
    public void NormalKeyClicked(Key _keyClicked)
    {
        if (!b_NormalKeyClicked)
        {
            SwitchOffOthers(_keyClicked);
            CenterKey(_keyClicked);
            b_NormalKeyClicked = true;
        }
        AddToLetter(_keyClicked, b_DiacriticKeyClicked);
		SwitchOnOffDiacritics(b_NormalKeyClicked);
    }

    //Still WIP. Add to letter, but nothing else currently happens.
    public void DiacriticKeyClicked(Key _diacriticClicked)
    {
        b_DiacriticKeyClicked=true;
        AddToLetter(_diacriticClicked, b_DiacriticKeyClicked);
    }

    //Switch off all but one.
    void SwitchOffOthers(Key _keyToKeepOn)
    {
        foreach (Key _key in key_List_RegularKeys)
        {
            if (_key != _keyToKeepOn)
            {
                _key.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    //Bring the key to the center based on serialised variables.
    void CenterKey(Key _keyToCenter)
    {
        t_oldKeyPosition = _keyToCenter.transform;
        _keyToCenter.transform.position = V3_CenterPosition;
        _keyToCenter.transform.localScale = new Vector3(F_NewScale, F_NewScale, F_NewScale);
    }

    //This is still a WIP. It still needs to be tied in to the actual key that has been clicked.
    void AddToLetter(Key _key, bool _isDiacritic)
    {
        if(!_isDiacritic)
        {
            s_Letter=_key.gameObject.name;
        }

        else
        {
            s_Letter+=_key.gameObject.name;
        }
        
        Debug.Log(s_Letter);
    }

}
