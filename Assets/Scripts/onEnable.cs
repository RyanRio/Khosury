using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onEnable : MonoBehaviour
{

    public Animator anim;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void setSpeed(float speed)
    {

        this.speed = speed;
    }

    private void OnEnable()
    {
        Debug.Log("setting speed of mult");
        Debug.Log("prev mult: " + anim.GetFloat("mult"));
        anim.SetFloat("mult", 1.0f / Map.Instance.approach);

        Debug.Log("new mult: " + anim.GetFloat("mult"));
    }
}
