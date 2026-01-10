using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public InventorySlot[] slots;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item, int amount)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.IsSameItem(item))
            {
                slot.SetItem(item, amount);
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item, amount);
                return;
            }
        }
        
        Debug.Log("Inventory Full");
    }
}
