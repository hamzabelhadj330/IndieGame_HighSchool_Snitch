using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectable : MonoBehaviour
{

    [SerializeField] private Key key;
    [SerializeField] private AudioClip pickupSound;

    [SerializeField] private AudioSource audioSource;

    public void KeyPickup()
    {
        if (key != null)
        {
            Keyinventory.Instance.AddKey(key); //add to inventory

            if (pickupSound != null && audioSource !=null)
            {
                audioSource.PlayOneShot(pickupSound);
            }


            gameObject.SetActive(false);
        }


        //logic
    }

}
