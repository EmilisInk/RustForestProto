using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    public GameObject winUI;

    private void Start()
    {
        winUI.SetActive(false);
    }
    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            FindObjectOfType<resultUI>()?.ShowWin();
        }
    }
}
