using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItem : MonoBehaviour
{

    public enum ItemType
    {
        None,
        Door,
        Key,
    }

    [Header("Item Type")]
    [SerializeField] private ItemType itemType = ItemType.None;

    private DoorInteractable doorInteractable;
    private KeyCollectable keyCollectable;

    private void Awake()
    {
        switch (itemType)
        {
            case ItemType.Door: doorInteractable = GetComponent<DoorInteractable>(); break;
            case ItemType.Key: keyCollectable = GetComponent<KeyCollectable>(); break;
        }
    }
     public void ObjectInteraction()
    {
        switch (itemType)
        {
            case ItemType.Door: doorInteractable?.ToggleDoor();  break;
            case ItemType.Key: keyCollectable?.KeyPickup(); break;
        }
    }
       
   
}
