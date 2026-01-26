using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public string resourceType;
    public int totalResources = 1000;

    public Item item;
    public int Gather()
    {
        if (totalResources <= 0)
        {
            //Debug.Log(resourceType + " node is depleted.");
            Destroy(gameObject);
            return 0;
        }
        int gatherAmount = 0;

        Item tool = InventoryManager.Instance.GetHotbarItem(0);
        if (resourceType == "Tree" && tool.itemName == "Rock") gatherAmount = 10;
        else if (resourceType == "Tree" && tool.itemName == "StoneHatchet") gatherAmount = 20;
        else if (resourceType == "Tree" && tool.itemName == "MetalHatchet") gatherAmount = 30;

        else if (resourceType == "Stone" && tool.itemName == "Rock") gatherAmount = 7;
        else if (resourceType == "Stone" && tool.itemName == "StonePickaxe") gatherAmount = 36;
        else if (resourceType == "Stone" && tool.itemName == "MetalPickaxe") gatherAmount = 45;

        else if (resourceType == "Metal" && tool.itemName == "Rock") gatherAmount = 5;
        else if (resourceType == "Metal" && tool.itemName == "StonePickaxe") gatherAmount = 24;
        else if (resourceType == "Metal" && tool.itemName == "MetalPickaxe") gatherAmount = 30;

        else if (resourceType == "Sulfur" && tool.itemName == "Rock") gatherAmount = 3;
        else if (resourceType == "Sulfur" && tool.itemName == "StonePickaxe") gatherAmount = 18;
        else if (resourceType == "Sulfur" && tool.itemName == "MetalPickaxe") gatherAmount = 50;

        else if (resourceType == "Pipe" && tool.itemName == "Rock") gatherAmount = 1;
        else if (resourceType == "Pipe" && tool.itemName == "StonePickaxe") gatherAmount = 1;
        else if (resourceType == "Pipe" && tool.itemName == "MetalPickaxe") gatherAmount = 1;
        else if (resourceType == "Pipe" && tool.itemName == "StoneHatchet") gatherAmount = 1;
        else if (resourceType == "Pipe" && tool.itemName == "MetalHatchet") gatherAmount = 1;
        else
        {
            return 0;
        }

        int gatheredAmount = Mathf.Min(gatherAmount, totalResources);
        totalResources -= gatheredAmount;

        if(item != null && gatherAmount > 0)
        {
            InventoryManager.Instance.AddItem(item, gatheredAmount);
            //Debug.Log("Added " + gatheredAmount + " " + item.itemName + " to inventory.");
        }

        //Debug.Log("Gathered " + gatheredAmount + " from " + resourceType + ". Remaining: " + totalResources);

        if(totalResources <= 0)
        {
            Destroy(gameObject);
        }

        return gatheredAmount;
    }
}
