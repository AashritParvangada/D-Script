using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptWriter : MonoBehaviour
{

    [SerializeField] string[] S_Arr_Letters;
    [SerializeField] float F_MaxX, F_MaxY;
    [SerializeField] float F_HorizontalSkip, F_VerticalSkip;
    [SerializeField] GameObject G_LetterGroupPrefab;
    [SerializeField] int[] I_Arr_HiraganaSpots;

    LetterContainer Scr_LetterContainer;
    char[] c_underscore;

    [SerializeField] Transform T_InstantiationPoint;

    private void Start()
    {
        ScriptWriterInitialize();
        WriteScript();
    }

    //Sets the c_underscore character to _, looks for the letter container and initializes it using letter container function.
    void ScriptWriterInitialize()
    {
        SetInstantiationPoint(-F_MaxX, -F_MaxY);
        string s_underscore = "_";
        c_underscore = s_underscore.ToCharArray();
        Scr_LetterContainer = GameObject.FindObjectOfType<LetterContainer>();
        Scr_LetterContainer.LetterContinerInitialize();
    }

    //Instantiates each letter group at the right place, then runs the move inst. point function.
    public void WriteScript()
    {
        
        int iterationThroughS_Arr_Letters = 0;
        foreach (string s_LetterGroup in S_Arr_Letters)
        {

            if (s_LetterGroup != "")
            {
                InstantiateAndPopulateLetterGroup(FindCharacterIndexInString(s_LetterGroup, c_underscore[0]), s_LetterGroup.Length
                , G_LetterGroupPrefab, T_InstantiationPoint, s_LetterGroup, isItHiragana(iterationThroughS_Arr_Letters));
                MoveInstantiationPoint(F_HorizontalSkip, F_VerticalSkip);
            }

            else if (s_LetterGroup == "")
            {
                Debug.Log("Empty Cell");
                MoveInstantiationPoint(F_HorizontalSkip, F_VerticalSkip);
            }
            iterationThroughS_Arr_Letters++;
        }
    }

    //Make letter group, set the base letter and diacritic, run Populating Functions within the letter group.
    public void InstantiateAndPopulateLetterGroup(int _indexOfFirstUnderscore, int _letterGroupLength
    , GameObject _letterGroupPrefab, Transform _InstPointTransform, string _LetterGroup, bool isHiragana)
    {
        GameObject g_letterGroup = Instantiate(_letterGroupPrefab, _InstPointTransform);
        g_letterGroup.transform.SetParent(gameObject.transform);
        LetterFormation _LetterGroupScript = g_letterGroup.GetComponent<LetterFormation>();

        //If it's not Hiragana, set base letter and diacritic.
        if (!isHiragana)
        {
            _LetterGroupScript.S_BaseLetter = _LetterGroup.Substring(0, _indexOfFirstUnderscore + 1);
            _LetterGroupScript.S_Diacritic = _LetterGroup.Substring(_indexOfFirstUnderscore, _letterGroupLength - _indexOfFirstUnderscore);
        }

        //Otherwise just set base letter.
        else
        {
            _LetterGroupScript.S_BaseLetter = _LetterGroup;
        }

        //Populate the letter group based on whether it's hiragana or not.
        _LetterGroupScript.AllPopulatingFunctions(isHiragana);
    }

    //Find a character in a string.
    public int FindCharacterIndexInString(string s_stringToCheck, char c_characterToLookFor)
    {
        int i_characterIndex = 0;

        foreach (char c_character in s_stringToCheck)
        {
            if (c_character == c_characterToLookFor)
            {
                return i_characterIndex;
            }

            else
            {
                i_characterIndex++;
            }
        }

        return -1;

    }

    //Set inst point to a location.
    public void SetInstantiationPoint(float _x, float _y)
    {
        T_InstantiationPoint.position = new Vector3(_x, _y, 0);
    }

    //Moves inst. point by the vertical and horizontal skip. If exceeds margins, change line.
    public void MoveInstantiationPoint(float _xSkip, float _ySkip)
    {
        SetInstantiationPoint(T_InstantiationPoint.position.x + _xSkip, T_InstantiationPoint.position.y);
        if (T_InstantiationPoint.position.x > F_MaxX)
        {
            SetInstantiationPoint(-F_MaxX, T_InstantiationPoint.position.y + _ySkip);
        }
    }

    //Insert an iteration of an array and compare it with the ints in I_Arr_Hiragana Spots, return true if match.
    bool isItHiragana(int iterationInArray)
    {
        foreach (int value in I_Arr_HiraganaSpots)
        {
            if (value == iterationInArray)
            {
                return true;
                //Debug.Log("Returning True");
            }
        }

        return false;
    }

}
