using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

public delegate void HiraganaClicked();
public static event HiraganaClicked OnHiraganaClicked;


}
