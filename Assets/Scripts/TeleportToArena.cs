using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToArena : MonoBehaviour
{
    public Transform arenaSpawnPoint;
    public GameObject player;

    public void Teleport()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = arenaSpawnPoint.position;
    }
}
