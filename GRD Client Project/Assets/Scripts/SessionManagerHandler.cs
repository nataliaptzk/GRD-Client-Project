using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManagerHandler : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (SessionManager.CurrentDifficulty == null)
        {
            SessionManager.CurrentDifficulty = _gameManager.Difficulties[0];
        }
    }

    public void ChooseDifficultyHandler(Difficulty difficulty)
    {
        SessionManager.ChooseDifficulty(difficulty);
    }

    public void ResetSessionHandler(Difficulty difficulty)
    {
        SessionManager.ResetSession(difficulty);
    }
}