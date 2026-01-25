using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportToArena : MonoBehaviour
{
    public Transform arenaSpawnPoint;
    public GameObject player;
    public Button btn;


    public int count;
    public void Teleport()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = arenaSpawnPoint.position;
        count++;
    }

    private void Update()
    {
        if (count >= 1)
        {
            btn.enabled = false;
        }
    }
}
