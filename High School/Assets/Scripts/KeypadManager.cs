using UnityEngine;
using TMPro;
using System.Collections;

public class KeypadManager : MonoBehaviour
{
    public string correctCode = "1234";
    private string currentInput = "";

    public TMP_Text displayText;
    public GameObject objectToActivate;

    private Coroutine clearTextCoroutine;

    public void HandleInput(string value)
    {
        if (value == "Clear")
        {
            currentInput = "";
            UpdateDisplay();
        }
        else if (value == "Enter")
        {
            if (currentInput == correctCode)
            {
                Debug.Log("Correct code!");
                displayText.text = "Correct code!";
                objectToActivate?.SetActive(true);
            }
            else
            {
                Debug.Log("Incorrect code.");
                displayText.text = "Incorrect code.";
            }

            currentInput = "";

            // Reset display after 1.5 seconds
            if (clearTextCoroutine != null) StopCoroutine(clearTextCoroutine);
            clearTextCoroutine = StartCoroutine(ClearDisplayAfterDelay(1.5f));
        }
        else
        {
            if (currentInput.Length < correctCode.Length)
                currentInput += value;
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    IEnumerator ClearDisplayAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        displayText.text = "";
    }
}
