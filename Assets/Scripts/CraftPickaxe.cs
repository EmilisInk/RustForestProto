using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPickaxe : MonoBehaviour
{
    public Item itemChk;
    public Item itemChk2;
    public Item crafted;
    public void CraftItem()
    {
        if (InventoryManager.Instance.HasItem(itemChk, 100) && InventoryManager.Instance.HasItem(itemChk2, 100))
        {
            InventoryManager.Instance.RemoveItem(itemChk, 100);
            InventoryManager.Instance.RemoveItem(itemChk2, 100);
            InventoryManager.Instance.AddItem(crafted, 1);
            //Debug.Log("Crafted: " + crafted.itemName);
        }
        else
        {
            Debug.Log("Not enough materials to craft " + crafted.itemName);
        }
    }
}
