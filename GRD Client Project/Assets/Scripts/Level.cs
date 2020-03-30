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
    [SerializeField] private GameObject _helpScreen;
    [SerializeField] private GameObject _endScreen;
    protected GameManager _gameManager;

    private int _correctAnswers;
    private int _incorrectAnswers;
    private int _amountHelpScreenOpened;

    [SerializeField] private TextMeshProUGUI _titleTextBox;
    [SerializeField] private TextMeshProUGUI _remainingTimeTextBox;
    [SerializeField] private TextMeshProUGUI _correctAnswersTextBox;
    [SerializeField] private TextMeshProUGUI _incorrectAnswersTextBox;
    [SerializeField] private TextMeshProUGUI _scoreTextBox;

    public MiniGameInfo GameInfo => _gameInfo;


    public void FinishMiniGame()
    {
        // In case of time running out collect data
        SortingGame sortingGame = GetComponent<SortingGame>();
        if (sortingGame) sortingGame.FillInDataCollectionForRemainingObjects();
        InvestigationGame investigationGame = GetComponent<InvestigationGame>();
        if (investigationGame) investigationGame.FillInDataCollectionForRemainingObjects();
        Quiz quiz = GetComponent<Quiz>();
        if (quiz)
        {
            GetComponent<LeaderBoard>().SaveFinalResultToLeaderboardFile();
            quiz.FillInDataCollectionForRemainingObjects();
        }

        DisplayFinishedLevelInfo();
    }

    public void LoadNextLevel()
    {
        _sceneSwitcher.SwitchScene(GameInfo.sceneTitleToLoad);
    }

    private void DisplayFinishedLevelInfo()
    {
        _endScreen.SetActive(true);
        _titleTextBox.text = GameInfo.title;
        _remainingTimeTextBox.text = _timer.timeLeft.ToString();
        _correctAnswersTextBox.text = _gameManager.GetComponent<Score>().correct.ToString();
        _incorrectAnswersTextBox.text = _gameManager.GetComponent<Score>().incorrect.ToString();
        _scoreTextBox.text = SessionManager.Score.ToString();

        // Unity Analytics calls
        _gameManager.GetComponent<DataCollection>().SendFinishedLevelInfo(GameInfo.title, _gameManager.GetComponent<Score>().correct, _gameManager.GetComponent<Score>().incorrect);
        _gameManager.GetComponent<DataCollection>().HelpScreenOpened(GameInfo.title, _amountHelpScreenOpened);
        // Custom Data Collection Calls
        DataCollectionFileManager.WriteStringContinuation(_amountHelpScreenOpened.ToString());
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

    public void OpenHelpScreen()
    {
        _helpScreen.SetActive(true);
        _amountHelpScreenOpened++;
    }

    public void CloseHelpScreen()
    {
        _helpScreen.SetActive(false);
    }
}