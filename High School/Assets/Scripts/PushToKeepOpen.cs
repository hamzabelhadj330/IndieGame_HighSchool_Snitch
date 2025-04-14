using UnityEngine;
using System.Collections;

public class PushToKeepOpen : MonoBehaviour
{
    public DoorInteractable targetDoor;
    private bool isLocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PushObject"))
        {
            // Unlock the door and open it when the object enters
            targetDoor.UnlockDoor();
            targetDoor.OpenDoor();  // Ensure the door opens when unlocked
            isLocked = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PushObject"))
        {
            // Wait a moment before locking and closing the door
            if (!isLocked)
            {
                StartCoroutine(CloseAndLockDoorAfterDelay());
            }
        }
    }

    private IEnumerator CloseAndLockDoorAfterDelay()
    {
        // Close the door first
        targetDoor.CloseDoor();
        yield return new WaitForSeconds(1f); // Wait for the door to close
        targetDoor.LockDoor();  // Lock the door after closing
        isLocked = true;  // Prevent locking the door until the next trigger
    }
}
