using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    Dictionary<int, SliderBoth> sliders;
    public GameObject sliderLinePrefab;
    public GameObject sliderClickerPrefab;

    float SCREEN_HEIGHT;
    float SCREEN_WIDTH;


    private static SliderManager _instance;
    public static SliderManager Instance { get { return _instance; } }


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
        this.sliders = new Dictionary<int, SliderBoth>();
    }

        public void Start()
    {
        SCREEN_HEIGHT = Camera.main.orthographicSize * 2;
        SCREEN_WIDTH = SCREEN_HEIGHT * Screen.width / Screen.height;
    }

    class SliderBoth
    {
        public GameObject sliderLine;
        public GameObject sliderClicker;
        public bool isMoving = false;
    }

    protected IEnumerator SmoothMovement(GameObject toMove, Vector2 end, float inTime)
    {
        Rigidbody2D rb2d = toMove.GetComponent<Rigidbody2D>();
        Vector2 curPosition = toMove.transform.position;

        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / inTime;
            toMove.transform.position = Vector2.Lerp(curPosition, toUnityCoords(end), t);
            yield return null;
        }
    }

    public void spawn(Slider slider)
    {

        int hash = slider.GetHashCode();

        SliderBoth both;
        sliders.TryGetValue(hash, out both);
        if (both == null)
        {
            both = new SliderBoth();
            both.sliderLine = Instantiate(sliderLinePrefab);
            both.sliderClicker = Instantiate(sliderClickerPrefab);

            Vector2 unityStart = toUnityCoords(new Vector2(slider.x1, slider.y1));
            Vector2 unityEnd = toUnityCoords(new Vector2(slider.x2, slider.y2));

            both.sliderLine.transform.position = unityStart;
            both.sliderClicker.transform.position = unityStart;
            both.sliderClicker.transform.localScale = new Vector2(Map.Instance.radius, Map.Instance.radius);

            float desiredWidth = Vector3.Distance(unityEnd, unityStart);


            float actualSpriteWidth = sliderLinePrefab.GetComponent<SpriteRenderer>().size.x;

            both.sliderLine.transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), (unityEnd - unityStart).normalized);

            both.sliderLine.transform.localScale = new Vector2(
                (desiredWidth / actualSpriteWidth), both.sliderLine.transform.localScale.y);

            sliders.Add(hash, both);
        }

    }

    internal void deleteSilder(int key)
    {
        SliderBoth both;
        this.sliders.TryGetValue(key, out both);
        if (both != null)
        {
            this.sliders.Remove(key);
            Destroy(both.sliderClicker);
            Destroy(both.sliderLine);
        }
    }



    Vector2 toUnityCoords(Vector2 pos)
    {
        float x = (pos.x - 8f) * SCREEN_WIDTH / 16f;
        float y = (pos.y - 4.5f) * SCREEN_HEIGHT / 9f;
        return new Vector2(x, y);
    }

    // moves its spawned sliders
    public void moveSlider(int hash, Vector2 end, float endTime)
    {
        SliderBoth slider;
        this.sliders.TryGetValue(hash, out slider);
        if(slider != null && !slider.isMoving)
        {
            slider.isMoving = true;
            this.StartCoroutine(SmoothMovement(slider.sliderClicker, end, endTime - Time.time));
        }

    }


}
