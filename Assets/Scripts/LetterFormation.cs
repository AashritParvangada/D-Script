using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterFormation : MonoBehaviour
{

    #region Letter Populating Variables

    public string S_BaseLetter;
    public string S_Diacritic;

    //This finds the sprite renderer in the current game object so that we can edit the base letter / diacritic.
    SpriteRenderer SprRnd_BaseLetter;
    SpriteRenderer SprRnd_Diacritic;
    LetterContainer LetCon_LetterContainer;

    #endregion

    #region Diacritic Alignment Variables
    GameObject GO_Diacritic_Object;


    #endregion

    #region Letter Populating Functions



    void GetLetterContainer()
    {
        LetCon_LetterContainer = GameObject.FindObjectOfType<LetterContainer>();
    }
    void GetSpriteRenderers()
    {
        SprRnd_BaseLetter = gameObject.GetComponent<SpriteRenderer>();

        SprRnd_Diacritic = GO_Diacritic_Object.GetComponent<SpriteRenderer>();

    }

    void GetChildObject()
    {
        GO_Diacritic_Object = transform.GetChild(0).gameObject;
    }

    //If the dictionaries contain these letters, populate them.
    void PopulateDevanagariSprites()
    {
        if (LetterContainer.SToSpr_Dic_DevanagariLettersToSprites.ContainsKey(S_BaseLetter))
            SprRnd_BaseLetter.sprite = LetterContainer.SToSpr_Dic_DevanagariLettersToSprites[S_BaseLetter];
        else
        {
            Debug.Log("Ain't nothing but a G thang.");
        }

        if (LetterContainer.SToSpr_Dic_DevanagariLettersToSprites.ContainsKey(S_Diacritic))
            SprRnd_Diacritic.sprite = LetterContainer.SToSpr_Dic_DevanagariLettersToSprites[S_Diacritic];
        else
        {
            Debug.Log("Ain't nothing but a G thang.");
        }
    }

    void PopulateHiraganaSprites()
    {
        //If the dictionary contains this letter, then populate it.
        if (LetterContainer.SToSpr_Dic_HiraganaLettersToSprites.ContainsKey(S_BaseLetter))
            SprRnd_BaseLetter.sprite = LetterContainer.SToSpr_Dic_HiraganaLettersToSprites[S_BaseLetter];

        else
        {
            Debug.Log("Dochidemo nai iru, yakuza no koto dake");
        }
    }

    //Find script, find the diacritic object, get their sprite renderers.
    public void AllPopulatingFunctions(bool _isHiragana)
    {
        GetLetterContainer();
        GetChildObject();
        GetSpriteRenderers();

        //Currently called from Script Writer. Adds a diacritic offset if the letter is devanagari.
        if (!_isHiragana)
        {
            PopulateDevanagariSprites();
            OffsetDiacritic();
        }
        //If is hiragana, populate as hiragana.
        else
        { PopulateHiraganaSprites(); }

        //These are used for answering Hiragana.
        B_IsHiragana = _isHiragana;
        AddTriggerBox();
        FindKeyboard();
    }
    #endregion

    #region Diacritic Alignment Functions


    //Offsets Diacritic on X Axis if List Above contains the letter.
    void OffsetDiacritic()
    {
        if (LetterContainer.S_List_RightAligned.Contains(S_BaseLetter))
        {
            GO_Diacritic_Object.transform.localPosition = new Vector2(LetCon_LetterContainer.f_RightAligned, 0);
        }

        else if (LetterContainer.S_List_FarRightAligned.Contains(S_BaseLetter))
        {
            GO_Diacritic_Object.transform.localPosition = new Vector2(LetCon_LetterContainer.f_FarRightAligned, 0);
        }

        else if (LetterContainer.S_List_LeftAligned.Contains(S_BaseLetter))
        {
            GO_Diacritic_Object.transform.localPosition = new Vector2(LetCon_LetterContainer.f_LeftAligned, 0);
        }
        else
        {
            GO_Diacritic_Object.transform.localPosition = new Vector2(0, 0);
        }
    }
    #endregion


    //All of these are called in AllPopulatingFunctions but are sectioned off here.
    #region Click to Answer

    Keyboard keyb_Keyboard;
    public bool B_IsHiragana;

    //Look for the keyboard.
    void FindKeyboard()
    {
        keyb_Keyboard = GameObject.FindObjectOfType<Keyboard>();
    }

        //Adds a trigger box to each of the letters in the game.
    void AddTriggerBox()
    {
        if (B_IsHiragana)
        {
            BoxCollider2D _triggerBox = gameObject.AddComponent<BoxCollider2D>();
            _triggerBox.size = new Vector2(2, 3);
        }
    }

    //Keyboard comes up when I click a key.
    private void OnMouseDown()
    {
        //Turn on the keyboard and send in what the correct value should be.
        keyb_Keyboard.SwitchOnRegulars();
        keyb_Keyboard.PassInCorrectKey(this);
    }
    #endregion
}