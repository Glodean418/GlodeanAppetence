using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemObjects : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public string itemSequence;
}
