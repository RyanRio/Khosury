﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class Map : MonoBehaviour
{

    List<Click> clicks;
    List<Slider> sliders;
    public string beatmapPath;
    public BeatmapReader reader;
    public float radius;
    public float approach;
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
        HealthManager.Instance.notifyDead(die);
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
            SliderManager.Instance.deleteSilder(s.GetHashCode());
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
            SliderManager.Instance.spawn(s);

            if(s.startTime <= Time.time)
            {
                SliderManager.Instance.moveSlider(s.GetHashCode(), new Vector2(s.x2, s.y2), s.endTime);
            }
        }
        
    }

    void die()
    {
        print("oof i died");
        SceneManager.LoadScene("OOF_I_DIED");
    }

    public void handleDrag(Vector2 pos)
    {
        List<Slider> visiSliders = visibleSliders();

        foreach (Slider s in visiSliders)
        {
            // calculate where the slider is based on how much time has passed and how far it has to go
            float curTime = Time.time;
            float startTime = s.startTime;
            float endTime = s.endTime;

            float progressInTime = (curTime - startTime) / (endTime - startTime);

            Vector2 progress = Vector2.Lerp(new Vector2(s.x1, s.y1), new Vector2(s.x2, s.y2), progressInTime);

            float dsq = Vector2.SqrMagnitude(progress - pos);

            if (dsq <= (radius * radius))
            {
                scoreboardScript.Instance.updateScore(100);
                HealthManager.Instance.score(100);
            }
        }
    }

    public void handleClick(Vector2 pos)
    {

        List<Click> visi = visibleClicks();

        foreach (Click c in visi)
        {
            float dsq = Vector2.SqrMagnitude(new Vector2(c.x, c.y) - pos);
            if (dsq <= (radius * radius))
            {
                this.updateScore(c);
                Spawner.Instance.deleteClick(c.GetHashCode());
                this.clicks.Remove(c);
                SoundManager.instance.PlaySfx(HITSOUND);
            }
            else
            {
            }
            break;
            // Spawner.Instance.deleteClick(c.GetHashCode());
        }

    }

    // updates the score based on the players gamer skill
    private void updateScore(Click c)
    {
        float beginTime = c.time - approach;
        float curTime = Time.time;
        if (Time.time > c.time)
        {
            curTime = c.time;
        }
        float score = AnimationCurve.EaseInOut(c.time - approach, 0, c.time, 1000).Evaluate(curTime);
        int rounded = Mathf.RoundToInt(score);
        scoreboardScript.Instance.updateScore(rounded);
        HealthManager.Instance.score(rounded);

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
