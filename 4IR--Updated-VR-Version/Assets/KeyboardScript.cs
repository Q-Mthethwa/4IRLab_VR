using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardScript : MonoBehaviour
{

    public void ShowKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
