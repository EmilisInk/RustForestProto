using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedObject : MonoBehaviour
{
    public GameObject Rock;
    public GameObject Ak;
    public GameObject Torch;
    public GameObject StoneAxe;
    public GameObject StonePickaxe;
    public GameObject MetalAxe;
    public GameObject MetalPickaxe;
    public GameObject BuildingPlan;
    public GameObject Hammer;

    private void Update()
    {
        if(InventoryManager.Instance == null) return;

        Item item = InventoryManager.Instance.GetHotbarItem(0);

        Rock.SetActive(false);
        Ak.SetActive(false);
        Torch.SetActive(false);
        StoneAxe.SetActive(false);
        StonePickaxe.SetActive(false);
        MetalAxe.SetActive(false);
        MetalPickaxe.SetActive(false);
        BuildingPlan.SetActive(false);
        Hammer.SetActive(false);


        if (item == null) return;

        if (item.itemName == "Rock")
        {
            Rock.SetActive(true);
        }
        else if (item.itemName == "AK")
        {
            Ak.SetActive(true);
        }
        else if (item.itemName == "Torch")
        {
            Torch.SetActive(true);
        }
        else if (item.itemName == "StoneHatchet")
        {
            StoneAxe.SetActive(true);
        }
        else if (item.itemName == "MetalHatchet")
        {
            MetalAxe.SetActive(true);
        }
        else if (item.itemName == "StonePickaxe")
        {
            StonePickaxe.SetActive(true);
        }
        else if (item.itemName == "MetalPickaxe")
        {
            MetalPickaxe.SetActive(true);
        }
        else if (item.itemName == "BuildingPlan")
        {
            BuildingPlan.SetActive(true);
        }
        else if (item.itemName == "Hammer")
        {
            Hammer.SetActive(true);
        }
    }
}
