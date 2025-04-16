using UnityEngine;
using UnityEngine.UI;

public class KeypadInteractor : MonoBehaviour
{
    public float interactRange = 3f;
    public Camera playerCam;
    public Image crosshair;
    public GameObject interactionText; // Assign your UI Text GameObject here

    private KeypadButton currentButton;

    void Update()
    {
        HandleRaycast();
        HandleInteraction();
    }

    void HandleRaycast()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            KeypadButton button = hit.collider.GetComponent<KeypadButton>();
            if (button != null)
            {
                currentButton = button;
                crosshair.color = Color.red;
                interactionText.SetActive(true);
                return;
            }
        }

        // If nothing hit or not a keypad button
        currentButton = null;
        crosshair.color = Color.white;
        interactionText.SetActive(false);
    }

    void HandleInteraction()
    {
        if (currentButton != null && Input.GetKeyDown(KeyCode.E))
        {
            currentButton.Press();
        }
    }
}
