using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager sessionManager;

    private void Awake()
    {
        if (sessionManager == null)
        {
            DontDestroyOnLoad(gameObject);
            sessionManager = this;
        }
        else if (sessionManager != this)
        {
            Destroy(gameObject);
        }
    }


    public Difficulty currentDifficulty;
    public Score score;
    public string nickname;

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