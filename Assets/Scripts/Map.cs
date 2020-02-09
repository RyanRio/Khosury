﻿using System.Collections;
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
    AudioClip HITSOUND;

    private static Map _instance;
    public static Map Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        reader.read(beatmapPath);
        clicks = reader.getClicks();
        sliders = reader.getSliders();
        radius = reader.getRadius();
        approach = reader.getApproach();
        drain = reader.getDrain();
        tolerance = 0.1f * approach;
        SCREEN_HEIGHT = Camera.main.orthographicSize * 2;
        SCREEN_WIDTH = SCREEN_HEIGHT * Screen.width / Screen.height;
        HITSOUND = Resources.Load<AudioClip>("Songs/hit");

        Spawner.Instance.init(radius, approach);
    }

    // Update is called once per frame
    void Update()
    {
        List<Click> cToRemove = clicks.Where(c => isTooOld(c)).ToList();
        List<Slider> sToRemove = sliders.Where(s => isTooOld(s)).ToList();
        foreach (Click c in cToRemove)
        {
            Spawner.Instance.deleteClick(c.GetHashCode());
        }
        foreach (Slider s in sToRemove)
        {
            Spawner.Instance.deleteSlider(s.GetHashCode());
        }
        clicks = clicks.Where(c => !isTooOld(c)).ToList();
        sliders = sliders.Where(s => !isTooOld(s)).ToList();
        foreach (Click c in visibleClicks())
        {
            Spawner.Instance.spawnClick(toUnityCoords(new Vector2(c.x, c.y)), c.GetHashCode());
        }
        foreach (Slider s in visibleSliders())
        {
            // sliders 2 pos in future
            Spawner.Instance.spawnSlider(toUnityCoords(new Vector2(s.x1, s.y1)), s.GetHashCode());
        }
    }

    public void handleClick(Vector2 pos)
    {
        Debug.Log("click time: " + Time.time);

        List<Click> visi = visibleClicks();

        foreach (Click c in visi)
        {
            float dsq = Vector2.SqrMagnitude(new Vector2(c.x, c.y) - pos);
            if (dsq <= (radius * radius))
            {
                Spawner.Instance.deleteClick(c.GetHashCode());
                this.clicks.Remove(c);
                SoundManager.instance.PlaySfx(HITSOUND);
            }
            else
            {
                Debug.Log("failed");
            }
            break;
            // Spawner.Instance.deleteClick(c.GetHashCode());
        }

    }

    bool isVisible(Click c)
    {
        return Time.time >= c.time - approach && Time.time <= c.time + tolerance;
    }

    bool isVisible(Slider s)
    {
        return Time.time >= s.startTime - approach && Time.time <= s.endTime + tolerance;
    }

    bool isTooOld(Click c)
    {
        return Time.time > c.time + tolerance;
    }

    bool isTooOld(Slider s)
    {
        return Time.time > s.endTime + tolerance;
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
