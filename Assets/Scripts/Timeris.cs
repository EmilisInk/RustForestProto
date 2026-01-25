using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeris : MonoBehaviour
{
    private float startTime;
    private float stoppedTime;
    private bool running;

    void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        startTime = Time.time;
        running = true;
        stoppedTime = 0f;
    }

    public float StopTimer()
    {
        if (!running) return stoppedTime;
        stoppedTime = Time.time - startTime;
        running = false;
        return stoppedTime;
    }

    public float GetTime()
    {
        return running ? (Time.time - startTime) : stoppedTime;
    }

    public string FormatTime(float seconds)
    {
        int s = Mathf.FloorToInt(seconds);
        int m = s / 60;
        s = s % 60;
        return m.ToString("00") + ":" + s.ToString("00");
    }
}