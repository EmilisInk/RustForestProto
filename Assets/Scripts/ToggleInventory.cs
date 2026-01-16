using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public static bool isOpen = false;

    public Transform buttonsGroup;

    private void Start()
    {
        InventoryManager.Instance.SetInventoryOpen(isOpen);

        buttonsGroup.gameObject.SetActive(isOpen);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            
            InventoryManager.Instance.SetInventoryOpen(isOpen);

            buttonsGroup.gameObject.SetActive(isOpen);
            if (isOpen && buttonsGroup != null)
                buttonsGroup.SetAsLastSibling();

            Cursor.visible = isOpen;
            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
