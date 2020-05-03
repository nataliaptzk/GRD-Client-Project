using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreField;
    [SerializeField] private ParticleSystem _gainPoints;
    [SerializeField] private ParticleSystem _losePoints;

    public int correct;

    public int incorrect;

    // [NonSerialized] public int score;
    private Level _level;

    private void Awake()
    {
        _level = FindObjectOfType<Level>();
    }

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
        scoreField.text = SessionManager.Score.ToString();
    }

    public void AddScore(int points)
    {
        if (points == 0)
        {
        }
        else if (points < 0 && SessionManager.Score != 0)
        {
            var losePointsMain = _losePoints.main;
            losePointsMain.maxParticles = points * -1;
            _losePoints.Play();
        }
        else if (points > 0)
        {
            var gainPointsMain = _gainPoints.main;
            gainPointsMain.maxParticles = points;
            _gainPoints.Play();
        }

        SessionManager.Score += points;
        SessionManager.Score = Mathf.Max(0, SessionManager.Score);


        UpdateScoreUI();
    }

    public void CountCorrect()
    {
        correct++;
        _level.FlashCorrectColour();
    }

    public void CountIncorrect()
    {
        incorrect++;
        _level.FlashIncorrectColour();
    }
}