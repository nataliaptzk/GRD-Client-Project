using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] protected MiniGameInfo _gameInfo;
    [SerializeField] protected float _miniGameBaseTime;
    protected GameManager _gameManager;
    [SerializeField] protected Timer _timer;
    [SerializeField] private SceneSwitcher _sceneSwitcher;


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

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
    }
}