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
            Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float newx = Mathf.Lerp(0, 16, pos.x);
            float newy = Mathf.Lerp(0, 9, pos.y);
            Map.Instance.handleClick(new Vector2(newx, newy));
        }
    }
}
