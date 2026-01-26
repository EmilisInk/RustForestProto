using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item bodys;
    public GameObject lid;
    private bool playerInRange = false;
    private bool isOpened = false;

    void Update()
    {
        if(playerInRange && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.AddItem(bodys, 4);

            isOpened = true;

            Destroy(lid);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false; 
        }
    }
}
