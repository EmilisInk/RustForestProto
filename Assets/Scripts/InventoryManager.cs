using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI Elements")]
    public GameObject inventoryPanel;
    public Transform slotsParent;

    public InventorySlot[] slots;

    [Header("Starting Items")]
    public Item startingItem1;
    public int startingItem1Amount = 1;
    public Item startingItem2;
    public int startingItem2Amount = 1;
    public int starterSlotIndex1 = 24;
    public int starterSlotIndex2 = 25;



    [Header("Hotbar")]
    public int hotbarSize = 6;
    public int hotbarStartIndex = 23;

    private List<GameObject> hideObjects = new List<GameObject>();

    private void Start()
    {
        if (startingItem1 != null)
        {
            slots[starterSlotIndex1].AddAmount(startingItem1, startingItem1Amount);
        }
        if (startingItem2 != null)
        {
            slots[starterSlotIndex2].AddAmount(startingItem2, startingItem2Amount);
        }
    }

    private void Awake()
    {
        Instance = this;

        hideObjects.Clear();
        for(int c =  0; c < inventoryPanel.transform.childCount; c++)
        {
            var child = inventoryPanel.transform.GetChild(c).gameObject;
            if (slotsParent != null && child == slotsParent.gameObject) continue;
            hideObjects.Add(child);
        }
    }

    public void SetInventoryOpen(bool open)
    {
        foreach(var go in hideObjects)
            if(go != null) go.SetActive(open);

        if(slotsParent != null)
            slotsParent.gameObject.SetActive(true);

        for(int i = 0; i < slots.Length; i++)
        {
            bool isHotbar = (i >= hotbarStartIndex && i < hotbarStartIndex + hotbarSize);
            slots[i].gameObject.SetActive(isHotbar || open);
        }
    }

    public int GetHotbarItemAmount(int index)
    {
        if(index < 0 || index >= hotbarSize) return 0;
        if(index >= slots.Length) return 0;
        return slots[index].GetAmount();
    }

    public void AddItem(Item item, int amount)
    {
        int remaining = amount;

        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.IsSameItem(item))
            {
                remaining = slot.AddAmount(item, remaining);
                if(remaining <= 0) return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                remaining = slot.AddAmount(item, remaining);
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
                remaining = slot.RemoveAmount(remaining);
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

    public InventorySlot GetHotbarSlot(int hotbarIndex)
    {
        int index = hotbarStartIndex + hotbarIndex;

        if (slots == null) return null;
        if (hotbarIndex < 0 || hotbarIndex >= hotbarSize) return null;
        if (index < 0 || index >= slots.Length) return null;

        return slots[index];
    }

    public Item GetHotbarItem(int hotbarIndex)
    {
        var s = GetHotbarSlot(hotbarIndex);
        return s != null ? s.GetItem() : null;
    }

    public int GetHotbarAmount(int hotbarIndex)
    {
        var s = GetHotbarSlot(hotbarIndex);
        return s != null ? s.GetAmount() : 0;
    }
}
