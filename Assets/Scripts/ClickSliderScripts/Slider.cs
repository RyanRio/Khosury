using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slider
{
    public float x1, x2;
    public float y1, y2;
    public float startTime;
    public float endTime;

    public Slider(float x1, float y1, float x2, float y2, float startTime, float endTime)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
        this.startTime = startTime;
        this.endTime = endTime;
    }
}
