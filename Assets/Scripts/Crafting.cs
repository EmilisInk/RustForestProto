using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public CraftRecipe[] recipes;

    private CraftRecipe currentRecipe;

    public Item selectedItem;
    private Item craftingItem;


    public void Craft(Item outputItem)
    {
        foreach (CraftRecipe recipe in recipes)
        {
            if (recipe.output == outputItem)
            {
                currentRecipe = recipe;
                
                if(HasMaterials(recipe))
                {
                    RemoveMaterials(recipe);
                    StartCoroutine(CraftDelay(currentRecipe));
                }
                else
                {
                    Debug.Log("Not enough materials to craft: " + outputItem.itemName);
                }

                return;
            }
        }

        Debug.Log("No recipe found for: " + outputItem.itemName);
    }


    bool HasMaterials(CraftRecipe recipe)
    {
        for (int i = 0; i < recipe.materials.Length; i++)
        {
            if(!InventoryManager.Instance.HasItem(recipe.materials[i], recipe.amounts[i]))
            {
                return false;
            }
        }
        return true;
    }

    void RemoveMaterials(CraftRecipe recipe)
    {
        for (int i = 0; i < recipe.materials.Length; i++)
        {
            InventoryManager.Instance.RemoveItem(recipe.materials[i], recipe.amounts[i]);
        }
    }

    IEnumerator CraftDelay(CraftRecipe recipeToCraft)
    {
        yield return new WaitForSeconds(2f);
        InventoryManager.Instance.AddItem(recipeToCraft.output, 1);
        Debug.Log("Crafted: " + currentRecipe.output.itemName);
    }
}
