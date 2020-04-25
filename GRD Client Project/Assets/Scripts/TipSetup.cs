using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TipSetup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textBox;
    private List<Tips> _tips = new List<Tips>();
    private List<Tips> _currentDifficultyTips = new List<Tips>();

    private int _currentTipIndex;


    private void Start()
    {
        _currentTipIndex = 0;
        LoadJsonTips();
        DisplayTip(0);
    }

    public void LeftButton()
    {
        DisplayTip(-1);
    }

    public void RightButton()
    {
        DisplayTip(1);
    }

    private void DisplayTip(int changeIndexValue)
    {
        _currentTipIndex = _currentTipIndex + changeIndexValue;

        if (_currentTipIndex < 0)
        {
            _currentTipIndex = _currentDifficultyTips.Count - 1;
        }
        else if (_currentTipIndex >= _currentDifficultyTips.Count)
        {
            _currentTipIndex = 0;
        }


        _textBox.text = _currentDifficultyTips[_currentTipIndex].tip;
    }

    [Serializable]
    public class Tips
    {
        public string tip;
        public string difficulty;
    }

    [Serializable]
    public class RootObject
    {
        public List<Tips> tips;
    }

    private void LoadJsonTips()
    {
        string filePath = Application.streamingAssetsPath + "/tips.json";

        UnityWebRequest www = UnityWebRequest.Get(filePath);
        www.SendWebRequest();
        while (!www.isDone)
        {
        }

        string json = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);
        _tips = JsonUtility.FromJson<RootObject>(json).tips;

        LoadCurrentDifficultyTips();
    }

    private void LoadCurrentDifficultyTips()
    {
        for (int i = 0; i < _tips.Count; i++)
        {
            if (_tips[i].difficulty == SessionManager.CurrentDifficulty.name)
            {
                _currentDifficultyTips.Add(_tips[i]);
            }
        }
    }
}