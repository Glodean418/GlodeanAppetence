using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShopItem", order = 1)]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int cost;
    public bool available;
    public Sprite itemIcon;
    public ShopItem nextUpgrade;  // Next upgrade
    public bool isFinalUpgrade;   // Flag for final upgrade
    public int clothingIndex;
    public string familyMember;
}

