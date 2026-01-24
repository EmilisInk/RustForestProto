using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public int maxStack = 1000;

    [Header("Type")]
    public bool isWeapon;
}
