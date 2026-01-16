//using System.Collections;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;

//public class CraftPickaxe : MonoBehaviour
//{
//    public Item itemStone;
//    public Item itemWood;
//    public Item itemMetal;
//    public Item itemSulfur;
//    public Item StoneHatchet;
//    public Item StonePickaxe;
//    public Item MetalHatchet;
//    public Item MetalPickaxe;
//    public void CraftSHatchet()
//    {
//        if (InventoryManager.Instance.HasItem(itemStone, 100) && InventoryManager.Instance.HasItem(itemWood, 200))
//        {
//            InventoryManager.Instance.RemoveItem(itemStone, 100);
//            InventoryManager.Instance.RemoveItem(itemWood, 200);
//            StartCoroutine(CraftDelay());
//        }
//        else
//        {
//            Debug.Log("Not enough materials to craft " + StoneHatchet.itemName);
//        }
//    }

//    public void CraftSPickaxe()
//    {
//        if (InventoryManager.Instance.HasItem(itemStone, 100) && InventoryManager.Instance.HasItem(itemWood, 200))
//        {
//            InventoryManager.Instance.RemoveItem(itemStone, 100);
//            InventoryManager.Instance.RemoveItem(itemWood, 200);
//            StartCoroutine(CraftDelay());
//        }
//        else
//        {
//            Debug.Log("Not enough materials to craft " + StonePickaxe.itemName);
//        }
//    }

//    IEnumerator CraftDelay()
//    {
//        yield return new WaitForSeconds(2f);
//        InventoryManager.Instance.AddItem(crafted, 1);
//        Debug.Log("Crafted: " + crafted.itemName);
//    }
//}
