using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    List<Sprite> Spr_List_AllDevanagariLetterSprites = new List<Sprite>();

    List<Sprite> Spr_List_AllHiraganaLetterSprites = new List<Sprite>();
    //The dictionary matches each of the array values to a string.
    public static Dictionary<string, Sprite> SToSpr_Dic_DevanagariLettersToSprites = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> SToSpr_Dic_HiraganaLettersToSprites = new Dictionary<string, Sprite>();

    List<LetterFormation> Scr_List_LetterGroups = new List<LetterFormation>();
    #region Diacritic Alignment Variables
    public static List<string> S_List_RightAligned = new List<string>();
    public static List<string> S_List_FarRightAligned = new List<string>();
    public static List<string> S_List_LeftAligned = new List<string>();

    //These floats tell us how far the U, Uu, and R diacritics need to be offset on the X-Axis. See Offset Diacritic Function for more.
    public float f_RightAligned, f_FarRightAligned, f_LeftAligned;
    #endregion

    //The subsequent lists put those values into lists so that we can check Contains later.


    // Use this for initialization
    public void LetterContinerInitialize()
    {
        //Keep this as the first one. It makes the main list that populates the dictionary below.
        MakeMainLetterListsFromFolder();
        MakeFarRightAlignedListFromFolder();
        MakeRightAlignedListFromFolder();
        MakeLeftAlignedListFromFolder();
        MakeDictionaries();
    }

    void MakeMainLetterListsFromFolder()
    {
        foreach (Sprite spr_Letter in Resources.LoadAll<Sprite>("Resources_Letters/Devanagari"))
        {
            Spr_List_AllDevanagariLetterSprites.Add(spr_Letter);
        }

        foreach (Sprite spr_Letter in Resources.LoadAll<Sprite>("Resources_Letters/Hiragana"))
        {
            Spr_List_AllHiraganaLetterSprites.Add(spr_Letter);
        }
    }


    void MakeDictionaries()
    {
        foreach (Sprite spr_letter in Spr_List_AllDevanagariLetterSprites)
        {
            SToSpr_Dic_DevanagariLettersToSprites.Add(spr_letter.name, spr_letter);
        }

        foreach (Sprite spr_Letter in Spr_List_AllHiraganaLetterSprites)
        {
            SToSpr_Dic_HiraganaLettersToSprites.Add(spr_Letter.name, spr_Letter);
        }
    }
    #region Alignment Lists
    void MakeFarRightAlignedListFromFolder()
    {
        foreach (Sprite spr_Letter in Resources.LoadAll<Sprite>("Resources_Letters/Devanagari/FarRightAligned"))
        {
            S_List_FarRightAligned.Add(spr_Letter.name);
        }

    }

    void MakeRightAlignedListFromFolder()
    {
        foreach (Sprite spr_Letter in Resources.LoadAll<Sprite>("Resources_Letters/Devanagari/RightAligned"))
        {
            S_List_RightAligned.Add(spr_Letter.name);
        }

    }

    void MakeLeftAlignedListFromFolder()

    {
        foreach (Sprite spr_Letter in Resources.LoadAll<Sprite>("Resources_Letters/Devanagari/LeftAligned"))
        {
            S_List_LeftAligned.Add(spr_Letter.name);
        }

    }
    #endregion

}
