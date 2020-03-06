using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DidYouKnow : MonoBehaviour
{
    [SerializeField] private List<Message> _DYKMessages = new List<Message>();
    [SerializeField] private List<Message> _currentDifficultyDYKMessages = new List<Message>();
    [SerializeField] private TextMeshProUGUI _messageText;

    [SerializeField] private Timer _timer;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void Start()
    {
        StartCoroutine(_timer.StartStopwatch(60));
        LoadJson();
    }


    private void LoadCurrentDifficultyDYKMessages()
    {
        for (int i = 0; i < _DYKMessages.Count; i++)
        {
            if (_DYKMessages[i].difficulty == SessionManager.CurrentDifficulty.name)
            {
                _currentDifficultyDYKMessages.Add(_DYKMessages[i]);
            }
        }

        DisplayMessage();
    }

    private void DisplayMessage()
    {
        if (SceneManager.GetActiveScene().name == "04b DidYouKnowScreen")
        {
            _messageText.text = _currentDifficultyDYKMessages[0].didYouKnowMessage;
        }
        else if (SceneManager.GetActiveScene().name == "05b DidYouKnowScreen")
        {
            _messageText.text = _currentDifficultyDYKMessages[1].didYouKnowMessage;
        }
    }

    [Serializable]
    public class Message
    {
        public string didYouKnowMessage;
        public string difficulty;
    }

    [Serializable]
    public class RootObject
    {
        public List<Message> messages;
    }

    private void LoadJson()
    {
        using (StreamReader r = new StreamReader("Assets/Resources/didYouKnowData.json"))
        {
            string json = r.ReadToEnd();
            _DYKMessages = JsonUtility.FromJson<RootObject>(json).messages;
        }

        LoadCurrentDifficultyDYKMessages();
    }
}