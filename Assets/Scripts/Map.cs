using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : MonoBehaviour
{
    List<Click> clicks;
    List<Slider> sliders;
    public string beatmapPath;
    public BeatmapReader reader;
    float radius;
    float approach;
    float drain;
    float tolerance;

    float SCREEN_HEIGHT;
    float SCREEN_WIDTH;


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
        SCREEN_HEIGHT = Camera.main.orthographicSize * 2;
        SCREEN_WIDTH = SCREEN_HEIGHT * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        clicks = clicks.Where(c => !isTooOld(c)).ToList();
        sliders = sliders.Where(s => !isTooOld(s)).ToList();
    }

    void handleClick(Vector2 pos)
    {
        // todo sliders
        foreach (Click c in clicks)
        {
            float dsq = Vector2.SqrMagnitude(new Vector2(c.x, c.y) - pos);
            if (dsq <= radius * radius)
            {
                if (Mathf.Abs(Time.time / 1000f - c.time) <= 2 * tolerance)
                {
                    // todo notify success of c
                    break;
                }
            } 
            else
            {
                // todo notify fail of c
                break;
            }
        }
    }

    bool isVisible(Click c)
    {
        return Time.time / 1000f >= c.time - approach && Time.time / 1000f <= c.time + tolerance;
    }

    bool isVisible(Slider s)
    {
        return Time.time / 1000f >= s.startTime - approach && Time.time / 1000f <= s.endTime + tolerance;
    }

    bool isTooOld(Click c)
    {
        return Time.time / 1000f > c.time + tolerance;
    }

    bool isTooOld(Slider s)
    {
        return Time.time / 1000f > s.endTime + tolerance;
    }

    List<Click> visibleClicks()
    {
        return clicks.Where(c => isVisible(c)).ToList();
    }

    List<Slider> visibleSliders()
    {
        return sliders.Where(s => isVisible(s)).ToList();
    }

    Vector2 toBeatmapCoords(Vector2 pos)
    {
        
        float x = pos.x * 16f / SCREEN_WIDTH + 8f;
        float y = pos.y * 9f / SCREEN_HEIGHT + 4.5f;
        return new Vector2(x, y);
    }

    Vector2 toUnityCoords(Vector2 pos)
    {
        float x = (pos.x - 8f) * SCREEN_WIDTH / 16f;
        float y = (pos.y - 4.5f) * SCREEN_HEIGHT / 9f;
        return new Vector2(x, y);
    }
}
