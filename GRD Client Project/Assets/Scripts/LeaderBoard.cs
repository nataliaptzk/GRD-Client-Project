using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
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

    private List<Entry> _entries = new List<Entry>();


    private void ReadLeaderboardFile()
    {
    }

    public void SaveFinalResultToLeaderboardFile()
    {
        Entry newEntry = new Entry(SessionManager.Nickname, SessionManager.Score.ToString(), SessionManager.CurrentDifficulty);

        string data = JsonUtility.ToJson(newEntry);
        System.IO.File.AppendAllText(Application.persistentDataPath + "/leaderboard.json", data);
    }
}