﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

/// <summary>
/// This class is class responsible for the behaviour of the Quiz game. It reads the questions from the .json file, displays the questions and answers on the screen and fills in data in data collection file when the player exits the game before finishing
/// - Natalia Pietrzak
/// </summary>\
public class Quiz : Level
{
    [SerializeField] private TextMeshProUGUI _questionField;
    [SerializeField] private TextMeshProUGUI _questionNumberField;

    [SerializeField] private List<GameObject> answerObjects = new List<GameObject>();

    private List<Question> _questions = new List<Question>();
    [SerializeField] private List<Question> _currentDifficultyQuestions = new List<Question>();
    private int _currentQuestion;
    private List<Vector3> _answersPositions;
    [NonSerialized] public int correctAnswer = 0; // the correct answer is Always the answer with index 0, the json with questions contains correct answers ALWAYS in ANSWER1 variable


    private void Awake()
    {
        DisplayTutorialScreen();
        _gameManager = FindObjectOfType<GameManager>();
        _currentQuestion = 0;
        StartCoroutine(LoadJson());
    }


    private void FirstQuestion()
    {
        _questionField.text = _currentDifficultyQuestions[_currentQuestion].question;
        DisplayQuestionAndAnswers();
    }

    private void UpdateQuestionNumber()
    {
        _questionNumberField.text = _currentQuestion + 1 + "/" + _currentDifficultyQuestions.Count;
    }

    public void NextQuestion()
    {
        _currentQuestion++;

        if (_currentQuestion < _currentDifficultyQuestions.Count)
        {
            DisplayQuestionAndAnswers();
            ShuffleAnswerObjects();
        }
        else if (_currentQuestion >= _currentDifficultyQuestions.Count)
        {
            Invoke("FinishMiniGame", 1f);
        }
    }

    private void DisplayQuestionAndAnswers()
    {
        UpdateQuestionNumber();
        _questionField.text = _currentDifficultyQuestions[_currentQuestion].question;

        answerObjects[0].gameObject.SetActive(true);
        answerObjects[0].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentDifficultyQuestions[_currentQuestion].answer1;

        answerObjects[1].gameObject.SetActive(true);
        answerObjects[1].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentDifficultyQuestions[_currentQuestion].answer2;

        answerObjects[2].gameObject.SetActive(true);
        answerObjects[2].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentDifficultyQuestions[_currentQuestion].answer3;
    }

    [ContextMenu("Shuffle Answers")]
    // this function shuffles the position of answer game objects
    public void ShuffleAnswerObjects()
    {
        _answersPositions.Shuffle();

        for (int i = 0; i < answerObjects.Count; i++)
        {
            answerObjects[i].transform.localPosition = _answersPositions[i];
        }
    }

    private void LoadCurrentDifficultyQuestions()
    {
        for (int i = 0; i < _questions.Count; i++)
        {
            if (_questions[i].difficulty == SessionManager.CurrentDifficulty.name)
            {
                _currentDifficultyQuestions.Add(_questions[i]);
            }
        }
    }

    // ****************** READING QUESTIONS FROM JSON ********************

    [Serializable]
    public class Question
    {
        public string question;
        public string answer1;
        public string answer2;
        public string answer3;
        public string difficulty;
        public string include;
    }

    [Serializable]
    public class RootObject
    {
        public List<Question> questions;
    }

    private IEnumerator LoadJson()
    {
        string filePath = Application.streamingAssetsPath + "/quizdata.json";

        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();
            if (string.IsNullOrEmpty(www.error))
            {
                string json = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);
                _questions = JsonUtility.FromJson<RootObject>(json).questions;
                _answersPositions = new List<Vector3> {answerObjects[0].transform.position, answerObjects[1].transform.position, answerObjects[2].transform.position};
                LoadCurrentDifficultyQuestions();
                FirstQuestion();
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

    public void FillInDataCollectionForRemainingObjects()
    {
        //in case the timer runs out of time
        int tempCount = 0;
        tempCount = _currentDifficultyQuestions.Count - _currentQuestion;

        for (int i = 0; i < tempCount; i++)
        {
            DataCollectionFileManager.WriteStringContinuation("run out of time", true);
        }

        // Count the difference between amount of questions, and fill in remaining fields with N/A text (for custom data collection)
        int tempQuestionCount = 0;
        tempQuestionCount = 8 - _currentDifficultyQuestions.Count;

        for (int i = 0; i < tempQuestionCount; i++)
        {
            DataCollectionFileManager.WriteStringContinuation("N/A", true);
        }
    }
}