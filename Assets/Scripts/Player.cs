using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform PlayerPrefab;

    private void Update()
    {
        transform.position = PlayerPrefab.transform.position + new Vector3(0, 1, -5);
    }
}
