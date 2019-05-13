using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //Tells the game if this key is a diacritic so that we can do certain functions depending on whether it is or isn't.
    public bool isDiacritic = false;

    //Getting the Keyboard script, which will check if the input is the right letter in the end.
    private void Start()
    {
        GetKeyboardScript();
    }

    //Var of Keyboard Script.
    Keyboard KeyboardScript;

    //When this key is tapped-
    private void OnMouseDown()
    {
        KeyClicked(this);
    }

    void GetKeyboardScript()
    {
        //Pretty complicated way to check if parent of parent is keyboard. Might change to a Find later.
        if (transform.parent.transform.parent.GetComponent<Keyboard>())
        {
            KeyboardScript = transform.parent.transform.parent.GetComponent<Keyboard>();
        }

    }

    //This is currently triggered when the key is clicked. It calls a function from the Keyboard script that centers the key, enlarges it, and removes all other keys. It also pops up the diacritic keys.
    void KeyClicked(Key _key)
    {
        if (!isDiacritic)
        {
            KeyboardScript.NormalKeyClicked(_key);
        }

        else
        {
            KeyboardScript.DiacriticKeyClicked(this);
        }
    }

}
