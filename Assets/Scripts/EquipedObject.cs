using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedObject : MonoBehaviour
{
    public GameObject rock;
    public GameObject ak;
    public GameObject torch;
    public GameObject StoneAxe;
    public GameObject StonePickaxe;
    public GameObject MetalAxe;
    public GameObject MetalPickaxe;

    private void Update()
    {
        if(InventoryManager.Instance == null) return;

        Item item = InventoryManager.Instance.GetHotbarItem(0);

        rock.SetActive(false);
        ak.SetActive(false);
        torch.SetActive(false);
        StoneAxe.SetActive(false);
        StonePickaxe.SetActive(false);
        MetalAxe.SetActive(false);
        MetalPickaxe.SetActive(false);


        if (item == null) return;

        if (item.itemName == "Rock")
        {
            rock.SetActive(true);
        }
        else if (item.itemName == "AK")
        {
            ak.SetActive(true);
        }
        else if (item.itemName == "Torch")
        {
            torch.SetActive(true);
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
    }
}
