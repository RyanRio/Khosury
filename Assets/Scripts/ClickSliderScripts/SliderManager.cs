using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    Dictionary<int, GameObject> sliders;
    public GameObject sliderPrefab;
    float SCREEN_HEIGHT;
    float SCREEN_WIDTH;


    public void spawn(Slider slider)
    {
        int hash = slider.GetHashCode();

        GameObject sliderObj = Instantiate(sliderPrefab);

        sliderObj.transform.position = toUnityCoords(new Vector2(slider.x1, slider.y1));
  
        ManageSliderSelf manager = sliderObj.GetComponent<ManageSliderSelf>();

        manager.endPosition = toUnityCoords(new Vector2(slider.x2, slider.y2));
        manager.inTime = slider.endTime - slider.startTime;

        sliderObj.SetActive(true);
    }

    public void Start()
    {
        SCREEN_HEIGHT = Camera.main.orthographicSize * 2;
        SCREEN_WIDTH = SCREEN_HEIGHT * Screen.width / Screen.height;

        Slider s = new Slider(2.0f, 2.0f, 5f, 2.0f, 1.0f, 3.0f);
        spawn(s);
    }

    Vector2 toUnityCoords(Vector2 pos)
    {
        float x = (pos.x - 8f) * SCREEN_WIDTH / 16f;
        float y = (pos.y - 4.5f) * SCREEN_HEIGHT / 9f;
        return new Vector2(x, y);
    }


}
