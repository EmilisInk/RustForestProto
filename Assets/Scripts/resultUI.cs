using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resultUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Time Texts")]
    public TMP_Text winTimeText;
    public TMP_Text loseTimeText;

    [Header("Timer")]
    public Timeris timer;

    [Header("Disable these scripts on end (drag your Movement, CameraSettings, Shooting, Mining etc.)")]
    public MonoBehaviour[] disableOnEnd;

    private bool ended;

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        ended = false;
    }

    public void ShowWin()
    {
        if (ended) return;
        ended = true;

        float t = (timer != null) ? timer.StopTimer() : 0f;

        if (losePanel != null) losePanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(true);
        if (winTimeText != null && timer != null)
            winTimeText.text = "Time Survived: " + timer.FormatTime(t);

        FreezeGame();
    }

    public void ShowLose()
    {
        if (ended) return;
        ended = true;

        float t = (timer != null) ? timer.StopTimer() : 0f;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(true);
        if (loseTimeText != null && timer != null)
            loseTimeText.text = "Time Survived: " + timer.FormatTime(t);

        FreezeGame();
    }

    void FreezeGame()
    {
        Time.timeScale = 0f;

        if (disableOnEnd != null)
        {
            foreach (var s in disableOnEnd)
                if (s != null) s.enabled = false;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}