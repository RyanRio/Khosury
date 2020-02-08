using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasClicking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("pressed" + Input.mousePosition.y + " " + Input.mousePosition.x);
            //Converting Mouse Pos to 2D (vector2) World Pos
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }
}
