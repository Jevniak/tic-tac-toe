using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;

    private int score;

    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 0;
            SaveScore();
        }
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public void IncrementScore(int value)
    {
        score += value;
        SaveScore();
    }

    public void DecrementScore(int value)
    {
        score -= value;
        if (score < 0)
        {
            score = 0;
        }
        SaveScore();
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }
}