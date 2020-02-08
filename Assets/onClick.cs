using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour
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
            Debug.Log("pressed");
            //Converting Mouse Pos to 2D (vector2) World Pos
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
            if(hit)
            {
                if (hit.transform.tag == "Click")
                {
                    Debug.Log(hit.transform.name);
                }
            }

        }
    }
}
