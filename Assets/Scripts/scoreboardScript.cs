using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreboardScript : MonoBehaviour
{
    private static scoreboardScript _instance;
    public float score;
    public Text scoreText;
    public static scoreboardScript Instance { get { return _instance; } }


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
    }

    public float getScore()
    {
        return score;
    }

    public void updateScore(int newScore)
    {
        this.score += newScore;
        scoreText.text = "Score : " + this.score;

    }



}
