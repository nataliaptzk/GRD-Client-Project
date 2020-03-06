using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManagerHandler : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        if (SessionManager.CurrentDifficulty == null && SceneManager.GetActiveScene().buildIndex > 2)
        {
            _gameManager = FindObjectOfType<GameManager>();
            SessionManager.CurrentDifficulty = _gameManager.Difficulties[0];
        }
    }

    public void ChooseDifficultyHandler(Difficulty difficulty)
    {
        SessionManager.ChooseDifficulty(difficulty);
    }

    public void ResetSessionHandler(Difficulty difficulty)
    {
        SceneManager.LoadScene("01 WelcomeScreen");
        SessionManager.ResetSession(difficulty);
    }
}