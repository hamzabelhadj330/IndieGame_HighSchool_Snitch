using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private Transform keyPanel; // Parent container for key images
    [SerializeField] private GameObject keyImagePrefab; //prefab for the key image

    private Dictionary<Key, GameObject> keyImages = new Dictionary<Key, GameObject>();
    public static UIManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKeyToUI(Key key)
    {
        if (!keyImages.ContainsKey(key))
        {
            GameObject keyImage = Instantiate(keyImagePrefab, keyPanel);
            keyImage.GetComponent<Image>().sprite = key.keySprite;
            keyImages[key] = keyImage;
        }
    }

}

