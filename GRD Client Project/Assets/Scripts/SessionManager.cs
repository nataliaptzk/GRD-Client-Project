using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SessionManager
{
    private static Difficulty _currentDifficulty;
    private static int _score;
    private static string _nickname;
    private static bool _consent = false;
    private static string _sessionID;

    public static string nickA, nickB;

    public static Difficulty CurrentDifficulty
    {
        get => _currentDifficulty;
        set => _currentDifficulty = value;
    }

    public static int Score
    {
        get => _score;
        set => _score = value;
    }

    public static string Nickname => _nickname;

    public static bool Consent
    {
        get => _consent;
        set => _consent = value;
    }

    public static string SessionId => _sessionID;


    public static void ChooseDifficulty(Difficulty difficulty)
    {
        _currentDifficulty = difficulty;
    }

    public static void CreateSession()
    {
        _sessionID = DateTime.Now + " " + _currentDifficulty.name;
        _nickname = nickA + " " + nickB;
        Debug.Log(_nickname);
    }

    public static void ResetSession(Difficulty difficulty)
    {
        _currentDifficulty = difficulty;
        _score = 0;
        _consent = false;
        _nickname = "";
        _sessionID = "";
        // fill in!
    }
}