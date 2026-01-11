using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CraftPickaxe : MonoBehaviour
{
    public Item itemChk;
    public Item itemChk2;
    public Item crafted;
    public void CraftItem()
    {
        if (InventoryManager.Instance.HasItem(itemChk, 100) && InventoryManager.Instance.HasItem(itemChk2, 200))
        {
            InventoryManager.Instance.RemoveItem(itemChk, 100);
            InventoryManager.Instance.RemoveItem(itemChk2, 200);
            StartCoroutine(CraftDelay());
        }
        else
        {
            Debug.Log("Not enough materials to craft " + crafted.itemName);
        }
    }

    IEnumerator CraftDelay()
    {
        yield return new WaitForSeconds(2f);
        InventoryManager.Instance.AddItem(crafted, 1);
        Debug.Log("Crafted: " + crafted.itemName);
    }
}
