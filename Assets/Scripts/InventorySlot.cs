using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text amountText;

    private Item item;
    private int amount;

    public bool IsEmpty() => item == null;

    public bool IsSameItem(Item newItem)
    {
        return item == newItem;
    }

    public void SetItem(Item newItem, int addAmount)
    {
        Debug.Log("SetItem called on slot " + gameObject.name + " with item: " + newItem.name + " amount: " + addAmount);
        if (item == null) item = newItem;
        amount += addAmount;

        if (icon != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            icon.color = Color.white;
        }
        if (amountText != null)
        {
            amountText.text = amount.ToString();
            amountText.enabled = true;
        }
        Debug.Log($"Slot {gameObject.name} now has {amount} of {item.itemName}");
    }
    public void clear()
    {
        item = null;
        icon.enabled = false;
        amount = 0;
        amountText.text = "";
    }
}
