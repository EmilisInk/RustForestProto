using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public GameObject inventoryUI;
    private bool isOpen = false;

    void Start()
    {
        inventoryUI.SetActive(isOpen);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            inventoryUI.SetActive(isOpen);
        }
    }
}
