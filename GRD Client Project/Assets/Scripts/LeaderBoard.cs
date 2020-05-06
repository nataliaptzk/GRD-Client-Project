using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This class manages the leader board. It reads and writes to the .json file the score entries.
/// - Natalia Pietrzak
/// </summary>
public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _scores = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _indexes = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _youIndicators = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI _title;

    [SerializeField] private List<Entry> _entries = new List<Entry>();

    [Serializable]
    public class Entry
    {
        public string name;
        public int finalScore;
        public string currentDifficulty;
        public string sessionID;

        public Entry(string name, int finalScore, string currentDifficulty, string sessionID)
        {
            this.name = name;
            this.finalScore = finalScore;
            this.currentDifficulty = currentDifficulty;
            this.sessionID = sessionID;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "07 EndScreen")
        {
            // ReadLeaderboardFile(false);
            SaveFinalResultToLeaderboardFile();
        }
    }

    private void ReadLeaderboardFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");

        if (!System.IO.File.Exists(filePath))
        {
            System.IO.File.WriteAllText(filePath, "");
        }


        string jsonToLoad = File.ReadAllText(filePath);

        if (jsonToLoad != "")
        {
            Entry[] tempLoadListData = JsonHelper.FromJson<Entry>(jsonToLoad);
            _entries = tempLoadListData.OfType<Entry>().ToList();

            string checkDate = _entries[0].sessionID;

            if (checkDate.Contains(DateTime.Today.ToShortDateString()) == false) // if the leaderboard file contains entries from a different day than today
            {
                System.IO.File.WriteAllText(filePath, "");
                _entries = new List<Entry>();
            }
        }
    }

    public void SaveFinalResultToLeaderboardFile()
    {
        ReadLeaderboardFile();

        Entry saveData = new Entry(SessionManager.Nickname, SessionManager.Score, SessionManager.CurrentDifficulty.name, SessionManager.SessionId);
        _entries.Add(saveData);


        string filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        string jsonToSave = JsonHelper.ToJson(_entries.ToArray());

        File.WriteAllText(filePath, jsonToSave);
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        string currentDifficulty = SessionManager.CurrentDifficulty.name;

        _title.text = "top 5 of the day on " + currentDifficulty + " difficulty";


        List<Entry> currentDifficultyEntryListCopy = new List<Entry>(_entries.Where(entry => entry.currentDifficulty == SessionManager.CurrentDifficulty.name));
        List<Entry> sortedPlayersCurrentDifficulty = currentDifficultyEntryListCopy.OrderByDescending(entry => entry.finalScore).ToList();

        int checkLength = 5;
        if (sortedPlayersCurrentDifficulty.Count < 5)
        {
            checkLength = sortedPlayersCurrentDifficulty.Count;
        }

        int currentPlayerIndex = sortedPlayersCurrentDifficulty.FindIndex(entry => entry.sessionID == SessionManager.SessionId);

        for (int i = 0; i < checkLength; i++)
        {
            _indexes[i].gameObject.SetActive(true);
            _names[i].gameObject.SetActive(true);
            _scores[i].gameObject.SetActive(true);
            _youIndicators[i].gameObject.SetActive(true);
            _names[i].text = sortedPlayersCurrentDifficulty[i].name;
            _scores[i].text = sortedPlayersCurrentDifficulty[i].finalScore.ToString();
            if (currentPlayerIndex == i)
            {
                _youIndicators[i].text = "you";
            }
        }


        if (currentPlayerIndex >= 5) // if the player is further than 5th place, show his details below in smaller font
        {
            _indexes[5].gameObject.SetActive(true);
            _names[5].gameObject.SetActive(true);
            _scores[5].gameObject.SetActive(true);
            _youIndicators[5].gameObject.SetActive(true);
            _indexes[5].text = currentPlayerIndex + 1 + ".";
            _names[5].text = sortedPlayersCurrentDifficulty[currentPlayerIndex].name;
            _scores[5].text = sortedPlayersCurrentDifficulty[currentPlayerIndex].finalScore.ToString();
            _youIndicators[5].text = "you";
        }
    }
}