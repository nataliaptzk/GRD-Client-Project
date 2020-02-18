using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] private MiniGameInfo _gameInfo;
    [SerializeField] private TextMeshProUGUI _questionField;
    [SerializeField] private GameObject _answerDropArea;
    [SerializeField] private List<GameObject> answerObjects = new List<GameObject>();

    [SerializeField] private List<Question> _questions = new List<Question>();
    private int _currentQuestion;

    public List<Question> Questions => _questions;

    public int CurrentQuestion => _currentQuestion;

    private void Awake()
    {
        _currentQuestion = 0;
        FirstQuestion();
    }

    private void FirstQuestion()
    {
        _questionField.text = _questions[_currentQuestion].question;

        for (int i = 0; i < _questions[_currentQuestion].answers.Count; i++)
        {
            answerObjects[i].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _questions[_currentQuestion].answers[i];
        }
    }

    public void NextQuestion()
    {
        _currentQuestion++;
        _currentQuestion = Mathf.Clamp(_currentQuestion, 0, _questions.Count - 1);
        _questionField.text = _questions[_currentQuestion].question;

        for (int i = 0; i < _questions[_currentQuestion].answers.Count; i++)
        {
            answerObjects[i].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _questions[_currentQuestion].answers[i];
        }
    }

    public void SubmitAnswer()
    {
    }
}

[Serializable]
public class Question
{
    public string question;
    public List<string> answers = new List<string>();
    public int correctAnswer;
}