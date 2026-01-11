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
        int remaining = amount;

        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.IsSameItem(item))
            {
                remaining = slot.AddAmount(item, amount);
                if(remaining <= 0) return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                remaining = slot.AddAmount(item, amount);
                if(remaining <= 0) return;
            }
        }
        
        Debug.Log("Inventory Full");
    }
    
    public void RemoveItem(Item item, int amount)
    {
        int remaining = amount;
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.IsSameItem(item))
            {
                remaining = slot.RemoveAmount(amount);
                if(remaining <= 0) return;
            }
        }
        
        Debug.Log("Not enough items to remove");
    }


    public bool HasItem(Item item, int amount)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.IsSameItem(item))
            {
                total += slot.GetAmount();
                if (total >= amount) return true;
            }
        }
        return false;
    }
}
