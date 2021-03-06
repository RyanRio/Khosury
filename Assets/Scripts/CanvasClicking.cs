﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasClicking : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("z") || Input.GetKeyDown("x"))
        {
            Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float newx = Mathf.Lerp(0, 16, pos.x);
            float newy = Mathf.Lerp(0, 9, pos.y);
            Map.Instance.handleClick(new Vector2(newx, newy));
        }
        else if (Input.GetMouseButton(0) || Input.GetKey("z") || Input.GetKey("x"))
        {
            Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float newx = Mathf.Lerp(0, 16, pos.x);
            float newy = Mathf.Lerp(0, 9, pos.y);
            Map.Instance.handleDrag(new Vector2(newx, newy));
        }
    }
}
