using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizecheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("muh size is: " + GetComponent<SpriteRenderer>().bounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
