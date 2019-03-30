using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterFormation : MonoBehaviour
{

    #region Letter Populating Variables

    public string S_BaseLetter;
    public string S_Diacritic;

    //The dictionary matches each of the array values to a string.
    Dictionary<string, Sprite> OldDic = new Dictionary<string, Sprite>();

    //This finds the sprite renderer in the current game object so that we can edit the base letter / diacritic.
    SpriteRenderer SprRnd_BaseLetter;
    SpriteRenderer SprRnd_Diacritic;

    GameObject Diacritic_Object;
    #endregion

    #region Diacritic Alignment Variables


    LetterContainer Scr_LetterContainer;
    #endregion

    // Update is called once per frame
    void Update()
    {

        //Useful functions, don't forget.
        if (Input.GetKeyDown(KeyCode.S))
        {
            print(S_Diacritic.Substring(1, 2));
            print(S_Diacritic.Length);
        }
    }

    #region Letter Populating Functions

    void GetLetterContainer()
    {
        Scr_LetterContainer = GameObject.FindObjectOfType<LetterContainer>();
    }
    void GetSpriteRenderers()
    {
        SprRnd_BaseLetter = gameObject.GetComponent<SpriteRenderer>();

        SprRnd_Diacritic = Diacritic_Object.GetComponent<SpriteRenderer>();

    }

    void GetChildObject()
    {
        Diacritic_Object = transform.GetChild(0).gameObject;
    }

    void PopulateSprites()
    {
        if (LetterContainer.SToSpr_Dic_LettersToSprites.ContainsKey(S_BaseLetter))
            SprRnd_BaseLetter.sprite = LetterContainer.SToSpr_Dic_LettersToSprites[S_BaseLetter];
        else
        {
            Debug.Log("Ain't nothing but a G thang.");
        }

        if (LetterContainer.SToSpr_Dic_LettersToSprites.ContainsKey(S_Diacritic)) SprRnd_Diacritic.sprite = LetterContainer.SToSpr_Dic_LettersToSprites[S_Diacritic];
        else
        {
            Debug.Log("Ain't nothing but a G thang.");
        }
    }

    public void AllPopulatingFunctions()
    {
        GetLetterContainer();
        GetChildObject();
        GetSpriteRenderers();
        PopulateSprites();
        OffsetDiacritic();
    }
    #endregion

    #region Diacritic Alignment Functions


    //Offsets Diacritic on X Axis if List Above contains the letter.
    void OffsetDiacritic()
    {
        if (LetterContainer.S_List_RightAligned.Contains(S_BaseLetter))
        {
            Diacritic_Object.transform.localPosition = new Vector2(Scr_LetterContainer.f_RightAligned, 0);
        }

        else if (LetterContainer.S_List_FarRightAligned.Contains(S_BaseLetter))
        {
            Diacritic_Object.transform.localPosition = new Vector2(Scr_LetterContainer.f_FarRightAligned, 0);
        }

        else if (LetterContainer.S_List_LeftAligned.Contains(S_BaseLetter))
        {
            Diacritic_Object.transform.localPosition = new Vector2(Scr_LetterContainer.f_LeftAligned, 0);
        }
        else
        {
            Diacritic_Object.transform.localPosition = new Vector2(0, 0);
        }
    }
    #endregion
}
