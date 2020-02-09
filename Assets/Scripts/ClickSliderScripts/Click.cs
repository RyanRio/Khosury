using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Click
{
    public float x, y;
    public float time;

    public Click(float x, float y, float time)
    {
        this.x = x;
        this.y = y;
        this.time = time;
    }
}
