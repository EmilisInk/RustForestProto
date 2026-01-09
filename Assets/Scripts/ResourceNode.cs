using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public string resourceType;
    public int totalResources = 1000;
    public int Gather()
    {
        if (totalResources <= 0)
        {
            Debug.Log(resourceType + " node is depleted.");
            Destroy(gameObject);
            return 0;
        }
        int gatherAmount = 0;
        if (resourceType == "Tree") gatherAmount = 50;
        else if (resourceType == "Rock") gatherAmount = 30;
        else gatherAmount = 5;


        int gatheredAmount = Mathf.Min(gatherAmount, totalResources);
        totalResources -= gatheredAmount;

        Debug.Log("Gathered " + gatheredAmount + " from " + resourceType + ". Remaining: " + totalResources);

        return gatheredAmount;
   }
}
