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
    public void AllPopulatingFunctions(bool isHiragana)
    {
        GetLetterContainer();
        GetChildObject();
        GetSpriteRenderers();

        //Currently called from Script Writer. Adds a diacritic offset if the letter is devanagari.
        if (!isHiragana)
        {
            PopulateDevanagariSprites();
            OffsetDiacritic();
        }
        //If is hiragana, populate as hiragana.
        else
        { PopulateHiraganaSprites(); }
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
}
