using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{


    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;


    private static HealthManager _instance;
    public static HealthManager Instance { get { return _instance; } }

    public SimpleHealthBar healthBarGUI;
    public int maxHealth = 10000; // 10 * the max score gain
    public int healthLossPerSecond = 5;
    private int curHealth = 10000;

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

        gradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[1].color = Color.green;

        colorKey[0].time = 0.0f;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);


    }

    // Update is called once per frame
    void Update()
    {
        curHealth -= healthLossPerSecond;
        healthBarGUI.UpdateBar(curHealth, maxHealth);
        healthBarGUI.UpdateColor(gradient.Evaluate((float)curHealth / (float)maxHealth));
        if (curHealth <= 0)
        {
            cb();
        }




    }

    public delegate void Callback();

    private Callback cb;

    public void notifyDead(Callback cb)
    {
        this.cb = cb;
    }

    public void score(int score)
    {
        curHealth += score;
    }
}
