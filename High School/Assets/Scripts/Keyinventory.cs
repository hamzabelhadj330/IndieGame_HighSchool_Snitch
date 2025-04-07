using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyinventory : MonoBehaviour
{
    [SerializeField] private List<int> keyIds = new List<int>();

    public static Keyinventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional : keeps inventory persistent across scenes
        }
        else
        {
            Destroy(gameObject); //Ensures there's only one instance
        }
    }

    public void AddKey(Key key)
    {
        if (!keyIds.Contains(key.id))
        {
             keyIds.Add(key.id);
            Debug.Log($"Key added: {key.keyName} (ID: {key.id})");

            //update UI
            UIManager.Instance.AddKeyToUI(key);
        }
    }

    public bool HasKey(Key key)
    {
        return keyIds.Contains(key.id);
    }

}
