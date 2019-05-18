using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    Vector3 t_oldKeyPosition;     //Used to set the key back to the original when the keyboard is reset (i.e. when the answer is correct or incorrect)
    Vector3 t_oldKeyScale;
    Key Key_SelectedKey;
    [SerializeField] Vector3 V3_CenterPosition = new Vector3(0, -4, 0); //Where the center position is. Can be edited.
    [SerializeField] float F_NewScale = 1; //What the center scale is. Can be edited.

    bool b_NormalKeyClicked = false; //Is turned on when a normal key is clicked.
    bool b_DiacriticKeyClicked = false; //Is turned on when a diacritic key is clicked.
    List<Key> key_List_RegularKeys = new List<Key>(); //This is a list of all the keys in the keyboard that are regular. Used for on/off right now.
    List<Key> key_List_DiacriticKeys = new List<Key>(); //A list of all the keys in they keyboard that are diacritic. Used for on/off right now.

    LetterFormation LetFor_correctKey; //Pass a variable in here when activating the keyboard, clear it when closing the keyboard.
    ScriptWriter ScrWri_Level; //This will be used to instantiate the correct letter at the Hiragana location.
    string s_inputLetter; //This is the string that we'll used to check whether the input is correct or not. If the input is wrong, we'll reset the keyboard in a function.


    // Use this for initialization
    void Start()
    {
        GetAllVariables();
        GetAllKeys();
        SwitchOffKeyboard();
    }

    void GetAllVariables()
    {
        ScrWri_Level = GameObject.FindObjectOfType<ScriptWriter>();
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
        foreach (Key _key in key_List_DiacriticKeys)
        {
            _key.gameObject.SetActive(_keyActive);
        }
    }

    //If a normal key is clicked, turn off other normal keys, pull the key to the center of the screen, and surround it with diacritic keys.
    public void NormalKeyClicked(Key _keyClicked)
    {
        if (!b_NormalKeyClicked)
        {
            Key_SelectedKey = _keyClicked;
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
        b_DiacriticKeyClicked = true;
        AddToLetter(_diacriticClicked, b_DiacriticKeyClicked);
    }

    //Switch off all but one.
    void SwitchOffOthers(Key _keyToKeepOn)
    {
        foreach (Key _key in key_List_RegularKeys)
        {
            if (_key != _keyToKeepOn)
            {
                _key.gameObject.SetActive(false);
            }
        }
    }

    void SwitchOffKeyboard()
    {
        foreach (Key _key in key_List_RegularKeys)
        {
            _key.gameObject.SetActive(false);
        }
        foreach (Key _key in key_List_DiacriticKeys)
        {
            _key.gameObject.SetActive(false);
        }
    }

    public void SwitchOnRegulars()
    {
        foreach (Key _key in key_List_RegularKeys)
        {
            _key.gameObject.SetActive(true);
        }
    }

    //Store value of correct key.
    public void PassInCorrectKey(LetterFormation _correctKey)
    {
        LetFor_correctKey = _correctKey;
    }

    //Bring the key to the center based on serialised variables.
    void CenterKey(Key _keyToCenter)
    {
        t_oldKeyPosition = _keyToCenter.transform.position;
        t_oldKeyScale = _keyToCenter.transform.localScale;
        _keyToCenter.transform.position = V3_CenterPosition;
        _keyToCenter.transform.localScale = new Vector3(F_NewScale, F_NewScale, F_NewScale);

    }

    //This is still a WIP. It still needs to be tied in to the actual key that has been clicked.
    void AddToLetter(Key _key, bool _isDiacritic)
    {
        if (!_isDiacritic)
        {
            s_inputLetter = _key.gameObject.name;
        }

        else
        {
            s_inputLetter += _key.gameObject.name;
            CheckIfIsCorrectInput();
        }

        Debug.Log(s_inputLetter);
    }

    //Checks if the combined string of normal + diacritic inputs matches what the Bsae Letter of Hiragana is. If so, Instantiates a devanagari there.
    void CheckIfIsCorrectInput()
    {
        if (s_inputLetter == LetFor_correctKey.S_BaseLetter)
        {
            Debug.Log("That was the correct input!");
            SwitchOffTriggerOnKey();
            ResetAndSwitchOffKeyboard();
            ScrWri_Level.InstantiateSingleDevanagari(LetFor_correctKey);
        }

        else
        {
            {
                ReturnKey();
                b_NormalKeyClicked=false;
                b_DiacriticKeyClicked=false;
                SwitchOnOffDiacritics(b_NormalKeyClicked);
                SwitchOnRegulars();
                s_inputLetter=null;
                Debug.Log("Wrong input!" + LetFor_correctKey.S_BaseLetter + "!=" + s_inputLetter);
            }
        }
    }

    void ResetAndSwitchOffKeyboard() //This is called when players get a key correctly. The Keyboard disappears.
    {
        ReturnKey();
        b_NormalKeyClicked=false;
        b_DiacriticKeyClicked=false;
        SwitchOnOffDiacritics(false);
        SwitchOffKeyboard();
        s_inputLetter=null;

    }

    //Returns a key to the place and scale from whence it came.
    void ReturnKey()
    {
        Key_SelectedKey.transform.position = t_oldKeyPosition;
        Key_SelectedKey.transform.localScale = t_oldKeyScale;
    }

    void SwitchOffTriggerOnKey() //Currently makes sure we can't click the Hiragana again. Later can put a funciton in the hiragana that affects animation.
    {
        LetFor_correctKey.GetComponent<BoxCollider2D>().enabled=false;
    }

}
