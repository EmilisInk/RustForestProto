using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRespawnWatcher : MonoBehaviour
{
    public Spawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.NotifyDestroyed(gameObject);
        }
    }
}
