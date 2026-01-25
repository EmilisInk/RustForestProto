using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resultUI : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;

    public TMP_Text winTimeText;
    public TMP_Text loseTimeText;

    public Timeris timer;

    private bool ended;

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        if (timer == null) timer = FindObjectOfType<Timeris>();
    }

    public void ShowWin()
    {
        if (ended) return;
        ended = true;

        float t = timer != null ? timer.StopTimer() : 0f;

        if (losePanel != null) losePanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(true);

        if (winTimeText != null && timer != null)
            winTimeText.text = "Time Survived: " + timer.FormatTime(t);

        PauseAndCursor();
    }

    public void ShowLose()
    {
        if (ended) return;
        ended = true;

        float t = timer != null ? timer.StopTimer() : 0f;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(true);

        if (loseTimeText != null && timer != null)
            loseTimeText.text = "Time Survived: " + timer.FormatTime(t);

        PauseAndCursor();
    }

    void PauseAndCursor()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}