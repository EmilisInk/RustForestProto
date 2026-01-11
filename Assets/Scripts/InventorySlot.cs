using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler

{
    public Image icon;
    public TMP_Text amountText;

    private Item item;
    private int amount;

    private Transform originalParent;
    private Canvas canvas;

    private void Start()
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
    public void Clear()
    {
        item = null;
        amount = 0;
        UpdateUI();
    }
}
