using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherRate : MonoBehaviour
{
    private void Update()
    {
        Item tool = InventoryManager.Instance.GetHotbarItem(0);
        if (tool != null && tool.itemName == "Axe")
        {
            Debug.Log("Using Axe - Increased gather rate!");
            
        }
    }
}
