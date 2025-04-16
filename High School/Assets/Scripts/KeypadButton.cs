using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public string buttonValue; // example: "1", "2", "Enter", "Clear"

    public void Press()
    {
        FindObjectOfType<KeypadManager>().HandleInput(buttonValue);
    }
}
