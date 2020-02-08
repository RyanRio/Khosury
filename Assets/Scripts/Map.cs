using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    List<Click> clicks;
    List<Slider> sliders;
    public string beatmapPath;
    BeatmapReader reader;
    float radius;
    float approach;
    float drain;
    float tolerance;

    float SCREEN_HEIGHT = Camera.main.orthographicSize * 2;
    float SCREEN_WIDTH = screenHeight * Screen.width / Screen.height;


    // Start is called before the first frame update
    void Start()
    {
        reader.read(beatmapPath);
        clicks = reader.getClicks();
        sliders = reader.getSliders();
        radius = reader.getRadius();
        approach = reader.getApproach();
        drain = reader.getDrain();
        tolerance = 0.005f * approach;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void handleClick(Vector2 pos)
    {

    }

    Vector2 toBeatmapCoords(Vector2 pos)
    {
        
        float x = pos.x * 16 / SCREEN_WIDTH + 8;
        float y = pos.y * 9 / SCREEN_HEIGHT + 4.5;
        return new Vector2(x, y);
    }

    Vector2 toUnityCoords(Vector2 pos)
    {
        float x = (pos.x - 8) * SCREEN_WIDTH / 16;
        float y = (pox.y - 4.5) * SCREEN_HEIGHT / 9;
        return new Vector2(x, y);
    }
}
