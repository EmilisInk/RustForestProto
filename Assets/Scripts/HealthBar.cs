using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth playerHealth;
    public TMP_Text healthText;

    private void Start()
    {
        slider.value = 1f;
        UpdateText();
    }

    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, playerHealth.HealthPercent(), Time.deltaTime * 10f);
        UpdateText();
    }
    void UpdateText()
    {
        healthText.text = $"{playerHealth.currentHealth}";
    }
}
