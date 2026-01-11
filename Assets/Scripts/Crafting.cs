//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Crafting : MonoBehaviour
//{
//    public Item itemChk;

//    public void CraftItem()
//    {
//        if (inventory.GetItemCount("Wood") >= 2 && inventory.GetItemCount("Stone") >= 1)
//        {
//            inventory.RemoveItem("Wood", 2);
//            inventory.RemoveItem("Stone", 1);
//            inventory.AddItem(itemChk, 1);
//            Debug.Log("Crafted: " + itemChk.itemName);
//        }
//        else
//        {
//            Debug.Log("Not enough materials to craft " + itemChk.itemName);
//        }
//    }
//}
