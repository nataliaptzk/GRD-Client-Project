using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    //  [SerializeField] private game _currentGame;
    /*private SortingGame _sortingGame;
    private InvestigationGame _investigationGame;
    private Quiz _quizGame;*/
    [SerializeField] private Level _currentMiniGame;

    //  public enum game
    //  {
    //      sortingGame,
    //     investigationGame,
    //     quizGame
    //  }

    private void Start()
    {
        _currentMiniGame = FindObjectOfType<Level>();
        /*if (_currentGame == game.sortingGame)
        {
            _sortingGame = FindObjectOfType<SortingGame>();
        }
        else if (_currentGame == game.investigationGame)
        {
            _investigationGame = FindObjectOfType<InvestigationGame>();
        }
        else if (_currentGame == game.quizGame)
        {
            _quizGame = FindObjectOfType<Quiz>();
        }*/
    }

    public IEnumerator StartStopwatch(float duration)
    {
        float totalTime = 0;
        while (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            var integer = (int) totalTime;
            timeText.text = integer.ToString();
            yield return null;
        }
    }

    public IEnumerator Countdown(float duration)
    {
        float totalTime = duration;
        while (totalTime >= 0)
        {
            totalTime -= Time.deltaTime;
            var integer = (int) totalTime;
            timeText.text = integer.ToString();
            yield return null;
        }

/*        if (_currentGame == game.sortingGame)
        {
            _sortingGame.FinishMiniGame(_currentGame);
        }
        else if (_currentGame == game.investigationGame)
        {
            _investigationGame.FinishMiniGame(_currentGame);
        }
        else if (_currentGame == game.quizGame)
        {
            _quizGame.FinishMiniGame(_currentGame);
        }*/

        _currentMiniGame.FinishMiniGame();
    }
}