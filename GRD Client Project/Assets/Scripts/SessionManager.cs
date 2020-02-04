using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SessionManager
{
    private static Difficulty _currentDifficulty;

    private static Score _score;

    private static string _nickname;

    public static Difficulty CurrentDifficulty
    {
        get => _currentDifficulty;
        set => _currentDifficulty = value;
    }

    public static Score Score
    {
        get => _score;
        set => _score = value;
    }

    public static string Nickname
    {
        get => _nickname;
        set => _nickname = value;
    }


    public static void ChooseDifficulty(Difficulty difficulty)
    {
        _currentDifficulty = difficulty;
    }

    public static void ResetSession(Difficulty difficulty)
    {
        _currentDifficulty = difficulty;
        // fill in!
    }
}