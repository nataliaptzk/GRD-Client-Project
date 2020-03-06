using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] protected MiniGameInfo _gameInfo;
    [SerializeField] protected float _miniGameBaseTime;
    [SerializeField] private Timer _timer;
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private GameObject _tutorialScreen;
    [SerializeField] private GameObject _endScreen;
    protected GameManager _gameManager;

    private int _correctAnswers;
    private int _incorrectAnswers;

    [SerializeField] private TextMeshProUGUI _titleTextBox;
    [SerializeField] private TextMeshProUGUI _remainingTimeTextBox;
    [SerializeField] private TextMeshProUGUI _correctAnswersTextBox;
    [SerializeField] private TextMeshProUGUI _incorrectAnswersTextBox;
    [SerializeField] private TextMeshProUGUI _scoreTextBox;


    public void FinishMiniGame()
    {
        DisplayFinishedLevelInfo();
    }

    public void LoadNextLevel()
    {
        _sceneSwitcher.SwitchScene(_gameInfo.sceneTitleToLoad);
    }

    private void DisplayFinishedLevelInfo()
    {
        _endScreen.SetActive(true);
        _titleTextBox.text = _gameInfo.title;
        _remainingTimeTextBox.text = _timer.timeLeft.ToString();
        _correctAnswersTextBox.text = _gameManager.GetComponent<Score>().correct.ToString();
        _incorrectAnswersTextBox.text = _gameManager.GetComponent<Score>().incorrect.ToString();
        _scoreTextBox.text = SessionManager.Score.ToString();
    }

    protected void DisplayTutorialScreen()
    {
        _tutorialScreen.SetActive(true);
    }

    public void CloseTutorial()
    {
        // here start timer
        _tutorialScreen.SetActive(false);
        StartCoroutine(_timer.Countdown(SessionManager.CurrentDifficulty.duration * _miniGameBaseTime));
    }
}