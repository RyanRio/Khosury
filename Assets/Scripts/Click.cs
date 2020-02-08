using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Click
{
    public readonly float x, y;
    public readonly float time;

    public Click(float x, float y, float time)
    {
        this.x = x;
        this.y = y;
        this.time = time;
    }
}
