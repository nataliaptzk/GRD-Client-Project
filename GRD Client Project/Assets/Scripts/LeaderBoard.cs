using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _scores = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _indexes = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI _title;
 [SerializeField]   private List<Entry> _entries = new List<Entry>();

    [Serializable]
    public class Entry
    {
        public string name;
        public string finalScore;
        public string currentDifficulty;

        public Entry(string name, string finalScore, Difficulty currentDifficulty)
        {
            this.name = name;
            this.finalScore = finalScore;
            this.currentDifficulty = currentDifficulty.name;
        }
    }

    [Serializable]
    public class RootObject
    {
        public List<Entry> entries;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "07 EndScreen")
        {
            ReadLeaderboardFile();
        }
    }

    private void ReadLeaderboardFile()
    {
        //   string json = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);
        //    _questions = JsonUtility.FromJson<RootObject>(json).questions;

        string json = File.ReadAllText(Application.persistentDataPath + "/leaderboard.json");
       // _entries = JsonUtility.FromJson<RootObject>(json).entries;
        
        Entry newEntries = JsonUtility.FromJson<Entry>(json);
        Debug.Log(newEntries);
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
    }

    public void SaveFinalResultToLeaderboardFile()
    {
        Entry newEntry = new Entry(SessionManager.Nickname, SessionManager.Score.ToString(), SessionManager.CurrentDifficulty);

        string data = JsonUtility.ToJson(newEntry);
        System.IO.File.AppendAllText(Application.persistentDataPath + "/leaderboard.json", data);
    }
}