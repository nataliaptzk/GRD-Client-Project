using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class Quiz : Level
{
    [SerializeField] private TextMeshProUGUI _questionField;
    [SerializeField] private GameObject _answerDropArea;

    [SerializeField] private List<GameObject> answerObjects = new List<GameObject>();

    [SerializeField] private List<Question> _questions = new List<Question>();
    private int _currentQuestion;

    //public List<Question> Questions => _questions;
    public int CurrentQuestion => _currentQuestion;

    private void Awake()
    {
        _currentQuestion = 0;
        FirstQuestion();
    }

    private void Start()
    {
        LoadJson();
        StartCoroutine(_timer.Countdown(SessionManager.CurrentDifficulty.duration * _miniGameBaseTime));
    }

    private void FirstQuestion()
    {
        /*_questionField.text = _questions[_currentQuestion].question;

        for (int i = 0; i < _questions[_currentQuestion].answers.Count; i++)
        {
            answerObjects[i].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _questions[_currentQuestion].answers[i];
        }*/
    }

    public void NextQuestion()
    {
/*        _currentQuestion++;
        _currentQuestion = Mathf.Clamp(_currentQuestion, 0, _questions.Count - 1);
        _questionField.text = _questions[_currentQuestion].question;

        for (int i = 0; i < _questions[_currentQuestion].answers.Count; i++)
        {
            answerObjects[i].gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _questions[_currentQuestion].answers[i];
        }*/
    }

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

    [ContextMenu("asdf")]
    public void LoadJson()
    {
        using (StreamReader r = new StreamReader("Assets/Resources/quizdata.json"))
        {
            string json = r.ReadToEnd();
            _questions = JsonUtility.FromJson<RootObject>(json).questions;

        }
    }
}