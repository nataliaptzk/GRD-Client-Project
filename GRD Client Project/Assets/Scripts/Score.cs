using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreField;

    // [NonSerialized] public int score;

    private void Start()
    {
        var tempGO = GameObject.FindGameObjectWithTag("ScoreField");
        if (tempGO != null)
        {
            scoreField = tempGO.GetComponent<TextMeshProUGUI>();
        }


        if (scoreField != null)
        {
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        scoreField.text =  SessionManager.Score.ToString();
    }

    public void AddScore(int points)
    {
        SessionManager.Score += points;
        SessionManager.Score = Mathf.Max(0, SessionManager.Score);
        UpdateScoreUI();
    }
}