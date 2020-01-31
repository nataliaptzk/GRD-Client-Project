using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private static SessionManager _sessionManager;
    public Difficulty currentDifficulty;
    public Score score;
    public string nickname;

    private void Awake()
    {
        if (_sessionManager == null)
        {
            DontDestroyOnLoad(gameObject);
            _sessionManager = this;
        }
        else if (_sessionManager != this)
        {
            Destroy(gameObject);
        }
    }

 
    void GenerateSession()
    {
    }

    void CreateNickname()
    {
    }

    public void ChooseDifficulty(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
    }
}