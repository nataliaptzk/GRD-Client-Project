using System;
using System.Collections;
using System.Collections.Generic;
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