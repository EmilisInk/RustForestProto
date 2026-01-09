using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    [Header("Mining Settings")]
    public float miningRange = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, miningRange))
            {
                ResourceNode resourceNode = hit.collider.GetComponent<ResourceNode>();
                if (resourceNode != null)
                {
                    resourceNode.Gather();
                }
                else
                {
                    Debug.Log("No resource node found at the hit location.");
                }
            }
            else
            {
                Debug.Log("No hit raycast");

            }
        }
    }
}
