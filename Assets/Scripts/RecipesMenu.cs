using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesMenu : MonoBehaviour
{
    public GameObject recipesPanel;

    private void Start()
    {
        recipesPanel.SetActive(false);
    }

    public void OpenRecipes()
    {
        recipesPanel.SetActive(true);
    }

    public void CloseRecipes()
    {
        recipesPanel.SetActive(false);
    }
}
