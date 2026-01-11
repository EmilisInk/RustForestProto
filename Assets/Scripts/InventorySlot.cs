using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class InventorySlot : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler

{
    public Image icon;
    public TMP_Text amountText;

    private Item item;
    private int amount;

    private Transform originalParent;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        Clear();
    }

    public bool IsEmpty() => item == null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(IsEmpty()) return;
        
        originalParent = icon.transform.parent;
        icon.transform.SetParent(canvas.transform);
        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!IsEmpty()) return;
        
        icon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventorySlot fromSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();
        if (fromSlot == null || fromSlot == this)
        {
            return;
        }

        SwapItems(fromSlot);
    }


    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot fromSlot = eventData?.pointerDrag?.GetComponent<InventorySlot>();
        if (fromSlot == null || fromSlot == this) return;

        SwapItems(fromSlot);
    }

    public void SwapItems(InventorySlot other)
    {
        Item tempItem = item;
        int tempAmount = amount;

        item = other.item;
        amount = other.amount;

        other.item = tempItem;
        other.amount = tempAmount;

        UpdateUI();
        other.UpdateUI();
    }

    void UpdateUI()
    {
        if(item == null)
        {
            icon.sprite = null;
            icon.color = new Color(1, 1, 1, 0);
            amountText.text = "";
            return;
        }

        icon.sprite = item.icon;
        icon.color = Color.white;
        amountText.text = amount.ToString();
    }

    public bool IsSameItem(Item newItem)
    {
        return item == newItem;
    }

    public int AddAmount(Item newItem, int addAmount)
    {
        if(item == null)
        {
            item = newItem;
            amount = 0;
        }

        int spaceLeft = item.maxStack - amount;
        int added = Mathf.Min(spaceLeft, addAmount);

        amount += added;

        UpdateUI();

        return addAmount - added;
    }

    public int RemoveAmount(int removeAmount)
    {
        if(item == null) return removeAmount;
        int removed = Mathf.Min(amount, removeAmount);
        amount -= removed;
        if(amount <= 0)
        {
            item = null;
            amount = 0;
        }
        UpdateUI();
        return removeAmount - removed;
    }

    public void SetItemDirect(Item newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
        UpdateUI();
    }
    public void Clear()
    {
        item = null;
        amount = 0;
        UpdateUI();
    }

    public int GetAmount()
    {
        return amount;
    }

    public Item GetItem()
    {
        return item;
    }


}
