using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Camera _mainCamera;
    private Color32 _cameraDefault;
    public MiniGameInfo GameInfo => _gameInfo;


    public void FinishMiniGame()
    {
        SortingGame sortingGame = GetComponent<SortingGame>();
        if (sortingGame)
        {
            sortingGame.FillInDataCollectionForRemainingObjects(); // In case of time running out collect data
            DisplayFinishedLevelInfo(true);
        }

        InvestigationGame investigationGame = GetComponent<InvestigationGame>();
        if (investigationGame)
        {
            investigationGame.FillInDataCollectionForRemainingObjects();
            DisplayFinishedLevelInfo(true);
        }

        Quiz quiz = GetComponent<Quiz>();
        if (quiz)
        {
            //  GetComponent<LeaderBoard>().SaveFinalResultToLeaderboardFile();
            quiz.FillInDataCollectionForRemainingObjects();
            DisplayFinishedLevelInfo(false);
        }

        Time.timeScale = 0;
    }

    public void LoadNextLevel()
    {
        _sceneSwitcher.SwitchScene(GameInfo.sceneTitleToLoad);
    }

    private void DisplayFinishedLevelInfo(bool value) // the bool value decides whether the function using it will add a ; at the end of the file entry or not
    {
        _endScreen.SetActive(true);
        _titleTextBox.text = GameInfo.title;

        int min = Mathf.FloorToInt(_timer.timeLeft / 60);
        int sec = Mathf.FloorToInt(_timer.timeLeft % 60);

        _remainingTimeTextBox.text = min.ToString("00") + ":" + sec.ToString("00");
        _correctAnswersTextBox.text = _gameManager.GetComponent<Score>().correct.ToString();
        _incorrectAnswersTextBox.text = _gameManager.GetComponent<Score>().incorrect.ToString();
        _scoreTextBox.text = SessionManager.Score.ToString();

        // Unity Analytics calls
        _gameManager.GetComponent<DataCollection>().SendFinishedLevelInfo(GameInfo.title, _gameManager.GetComponent<Score>().correct, _gameManager.GetComponent<Score>().incorrect);
        _gameManager.GetComponent<DataCollection>().HelpScreenOpened(GameInfo.title, _amountHelpScreenOpened);
        // Custom Data Collection Calls
        DataCollectionFileManager.WriteStringContinuation(_amountHelpScreenOpened.ToString(), value);
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
        Time.timeScale = 1;
        _mainCamera = Camera.main;
        _cameraDefault = _mainCamera.backgroundColor;
        SessionManager.scoreBeforeStartLevel = SessionManager.Score;
    }

    public void OpenHelpScreen()
    {
        Time.timeScale = 0;
        _helpScreen.SetActive(true);
        _amountHelpScreenOpened++;
    }

    public void CloseHelpScreen()
    {
        Time.timeScale = 1;

        _helpScreen.SetActive(false);
    }

    public void FlashCorrectColour()
    {
        Color newColour = new Color32(0, 205, 41, 0);


        LeanTween.value(_mainCamera.gameObject, _cameraDefault, newColour, .4f).setEase(LeanTweenType.easeInOutQuint).setOnUpdate(
            (Color val) => { _mainCamera.backgroundColor = val; }
        ).setOnComplete(OnCompleteChangeCamColourToDefault);
    }

    public void FlashIncorrectColour()
    {
        Color newColour = new Color32(227, 63, 44, 0);

        LeanTween.value(_mainCamera.gameObject, _cameraDefault, newColour, .4f).setEase(LeanTweenType.easeInOutQuint).setOnUpdate(
            (Color val) => { _mainCamera.backgroundColor = val; }
        ).setOnComplete(OnCompleteChangeCamColourToDefault);
    }

    private void OnCompleteChangeCamColourToDefault()
    {
        LeanTween.value(_mainCamera.gameObject, _mainCamera.backgroundColor, _cameraDefault, .2f).setEase(LeanTweenType.easeInQuint).setOnUpdate(
            (Color val) => { _mainCamera.backgroundColor = val; });
    }

    public void ReplayLevel()
    {
        SortingGame sortingGame = GetComponent<SortingGame>();
        if (sortingGame)
        {
            DataCollectionFileManager.AdjustDataFileForReplay(22); // 22 columns to remove from the file from the current session's row
        }

        InvestigationGame investigationGame = GetComponent<InvestigationGame>();
        if (investigationGame)
        {
            DataCollectionFileManager.AdjustDataFileForReplay(22); // 22 columns to remove from the file from the current session's row
        }

        Quiz quiz = GetComponent<Quiz>();
        if (quiz)
        {
            DataCollectionFileManager.AdjustDataFileForReplay(9); // 9 columns to remove from the file from the current session's row
        }

        SessionManager.Score = SessionManager.scoreBeforeStartLevel;
        _sceneSwitcher.SwitchScene(SceneManager.GetActiveScene().name);
    }
}