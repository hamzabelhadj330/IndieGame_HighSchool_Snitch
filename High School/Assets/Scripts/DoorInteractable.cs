using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private Transform doorPivot; //Pivot point of door
    [SerializeField] private float openAngle = 90f; //The angle to open the door
    [SerializeField] private float closeAngle = 0f; //The angle to close the door
    [SerializeField] private float rotationSpeed = 2f; //Speed of the door rotation

    //Lock Settings
    [Header("Lock Settings")]
    [SerializeField] public bool isLocked = false; //determines if the door is locked
    [SerializeField] private Key requiredKey; // key required to unlock the door

    //Audio

    [Header("Audio Settings")]
    [SerializeField] private AudioClip openSound; //sound effect for opening the door 
    [SerializeField] private AudioClip closeSound; //Sound effect for closing the door
    [SerializeField] private AudioClip lockedSound; //sound effect for locked  door interaction 

    private AudioSource audioSource;




    private bool isDoorOpen = false;
    private bool isAnimating = false;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = Quaternion.Euler(0f, closeAngle, 0f);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isAnimating)
        {
            doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(doorPivot.localRotation, targetRotation) < 0.1f )
            {
                doorPivot.localRotation = targetRotation;
                isAnimating = false;
            }
        }
    }

    public void ToggleDoor()
    {
        if (isLocked)
        {
            if (Keyinventory.Instance.HasKey(requiredKey))
            {
                Debug.Log($"Door '{gameObject.name}' unlocked with the correct key: {requiredKey.keyName}.");
                isLocked = false;
            }
            else
            {
                //playsound
                PlaySound(lockedSound);

                Debug.Log($"Door '{gameObject.name}' is locked. You need the correct key: {requiredKey.keyName}.");
                return;
            }
        }



        if (!isAnimating)
        {
            isDoorOpen = !isDoorOpen;

            targetRotation = Quaternion.Euler(0f, isDoorOpen ? openAngle : closeAngle, 0f);

            isAnimating = true;

            //playsound
            PlaySound(isDoorOpen ? openSound : closeSound);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    ///DoorUnlock

    public void UnlockDoor()
    {
        isLocked = false;
        ToggleDoor();  // This will open the door when unlocked
        Debug.Log("Door Unlocked!");
    }

    public void LockDoor()
    {
        isLocked = true;
        ToggleDoor();  // This will close the door when locked
        Debug.Log("Door Locked!");
    }
    public void OpenDoor()
    {
        if (isLocked) return;  // Don't open if the door is locked
        if (!isDoorOpen)
        {
            ToggleDoor();  // Use ToggleDoor to open it
        }
    }

    public void CloseDoor()
    {
        if (isDoorOpen)
        {
            ToggleDoor();  // Use ToggleDoor to close it
        }
    }

    //////////Door 
}
