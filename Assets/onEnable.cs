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
        anim.SetFloat("mult", speed);

        Debug.Log("new mult: " + anim.GetFloat("mult"));

        Debug.Log("prev bool: " + anim.GetBool("asd"));
        anim.SetBool(Animator.StringToHash("asd"), true);
        Debug.Log("bool: " + anim.GetBool(Animator.StringToHash("asd")));
    }
}
