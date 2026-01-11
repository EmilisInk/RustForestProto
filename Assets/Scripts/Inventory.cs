using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public bool isOpen = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isOpen)
            {
                inventory.SetActive(true);
                isOpen = true;
            }
            else
            {
                inventory.SetActive(false);
                isOpen = false;
            }

        }
    }
}
