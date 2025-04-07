using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Scriptable Objects/Key")]

public class Key : ScriptableObject
{

    public string keyName;
    public int id;
    public Sprite keySprite;


}
