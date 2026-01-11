using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public static bool isOpen = false;

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
            if (isOpen)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
