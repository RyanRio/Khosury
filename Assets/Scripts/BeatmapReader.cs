using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class BeatmapReader
{
    public string beatmap_path = "Assets/Beatmaps/";

    public List<Click> notes;
    public List<Slider> sliders;
    public float approach;
    public float drain = 0;
    public float radius;


    public void read(string beatmap_name)
    {
        string path = beatmap_path + beatmap_name;
        StreamReader reader = new StreamReader(path);

        BeatmapReader map = JsonUtility.FromJson<BeatmapReader>(reader.ReadToEnd());
        this.notes = map.notes;
        Debug.Log(map.notes.Count);
        Debug.Log(map.radius);
        this.sliders = map.sliders;
        this.approach = map.approach;
        this.drain = map.drain;
        this.radius = map.radius;
    }

    public List<Click> getClicks()
    {
        return notes;
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
