using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isDiacritic = false;
	private void Start() {
		GetKeyboardScript();
	}
    Keyboard KeyboardScript;
    private void OnMouseDown()
    {
        Color dimmed = new Color(50, 50, 50);
        GetComponent<SpriteRenderer>().color = dimmed;

        TriggerKeyboardSingleKey(this);
    }

    void GetKeyboardScript()
    {
        if (transform.parent.transform.parent.GetComponent<Keyboard>())
        {
            KeyboardScript = transform.parent.transform.parent.GetComponent<Keyboard>();
        }

    }

    void TriggerKeyboardSingleKey(Key _key)
    {
		Debug.Log("Gotcha");
		KeyboardScript.KeyClicked(_key);
    }

}
