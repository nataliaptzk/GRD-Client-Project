using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreField;

    [NonSerialized] public int score;

    private void Awake()
    {
        score = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreField.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        score = Mathf.Max(0, score);
       UpdateScoreUI();
    }
}