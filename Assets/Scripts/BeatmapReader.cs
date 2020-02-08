using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class BeatmapReader
{
    public string beatmap_path = "Assets/Beatmaps/";

    private List<Click> clicks;
    private List<Slider> sliders;
    private float approach;
    private float drain = 0;
    private float radius;


    public void read(string beatmap_name)
    {
        string path = beatmap_path + beatmap_name;
        StreamReader reader = new StreamReader(path);

        BeatmapReader map = JsonUtility.FromJson<BeatmapReader>(reader.ReadToEnd());
        this.clicks = map.clicks;
        this.sliders = map.sliders;
        this.approach = map.approach;
        this.drain = map.drain;
        this.radius = map.radius;
    }

    public List<Click> getClicks()
    {
        return clicks;
    }

    public List<Slider> getSliders()
    {
        return sliders;
    }

    public float getRadius()
    {
        return radius;
    }

    public float getApproach()
    {
        return approach;
    }

    public float getDrain()
    {
        return drain;
    }
}
