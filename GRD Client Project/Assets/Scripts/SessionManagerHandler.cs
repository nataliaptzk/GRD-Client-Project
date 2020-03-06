using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public void AssignNicknamePartA(TMP_InputField nameA)
    {
        SessionManager.nickA = nameA.text;
    }

    public void AssignNicknamePartB(TMP_InputField nameB)
    {
        SessionManager.nickB = nameB.text;
    }

    public void ChooseConsent(Toggle consent)
    {
        SessionManager.Consent = consent.isOn;
        Debug.Log(SessionManager.Consent);
    }

    public void ResetSessionHandler(Difficulty difficulty)
    {
        SceneManager.LoadScene("01 WelcomeScreen");
        SessionManager.ResetSession(difficulty);
    }

    public void CreateSessionHandler()
    {
        SessionManager.CreateSession();

    }
}