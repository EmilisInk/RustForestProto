using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    [Header("Mining Settings")]
    public float miningRange = 1f;

    public Swing swing;

    void Update()
    {
        if(ToggleInventory.isOpen)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, miningRange))
            {
                ResourceNode resourceNode = hit.collider.GetComponent<ResourceNode>();
                if (resourceNode != null)
                {
                    Item tool = InventoryManager.Instance.GetHotbarItem(0);

                    if(!CanMine(tool, resourceNode.resourceType))
                    {
                        Debug.Log("Cannot mine this resource with the current tool.");
                        return;

                    }
                    if (swing != null && swing.IsPlaying) return;

                    if(swing != null) swing.Play();

                    resourceNode.Gather();
                }
                else
                {
                    Debug.Log("No resource node found at the hit location.");
                }
            }
            else
            {


            }
        }
    }

    bool CanMine(Item tool, string resourceType)
    {
        if (tool == null) return false;

        string toolName = tool.itemName;

        if (toolName == "Rock") return true;

        if((toolName == "StoneHatchet" || toolName == "MetalHatchet") && resourceType == "Tree")
            return true;

        if((toolName == "StonePickaxe" || toolName == "MetalPickaxe") && resourceType == "Stone" || resourceType == "Metal" || resourceType == "Sulfur")
            return true;

        return false;
    }
}
