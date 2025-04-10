using UnityEngine;

public class PushToOpen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DoorInteractable targetDoor; // Door to open
    [SerializeField] private string requiredTag = "Pushable"; // Object to push

    [Header("Trigger Settings")]
    [SerializeField] private bool openOnce = true;
    private bool hasOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasOpened && openOnce) return;

        if (other.CompareTag(requiredTag))
        {
            // Unlock the door first
            targetDoor.isLocked = false;

            // Now toggle the door open
            targetDoor.ToggleDoor();

            hasOpened = true;
            Debug.Log("Door unlocked and opened via pushable object.");
        }
    }
}
