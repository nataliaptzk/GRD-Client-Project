using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class NicknameManager : MonoBehaviour
{
    [SerializeField] private List<FirstPart> _firstPart = new List<FirstPart>();

    [SerializeField] private List<SecondPart> _secondPart = new List<SecondPart>();

    private void Awake()
    {
        LoadJsonNames();
    }

    public void RandomizeFirst(TMP_InputField nameAInputField)
    {
        var randomIndex = Random.Range(0, _firstPart.Count);
        nameAInputField.text = _firstPart[randomIndex].First;
    }

    public void RandomizeSecond(TMP_InputField nameBInputField)
    {
        var randomIndex = Random.Range(0, _secondPart.Count);
        nameBInputField.text = _secondPart[randomIndex].Second;
    }


    [Serializable]
    public class FirstPart
    {
        public string First;
    }

    [Serializable]
    public class SecondPart
    {
        public string Second;
    }

    [Serializable]
    public class RootObject
    {
        public List<FirstPart> First;
        public List<SecondPart> Second;
    }

    private void LoadJsonNames()
    {
        /*
        var _path = Application.streamingAssetsPath + "/names.json";
        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(_path);
        www.SendWebRequest();
        while (!www.isDone)
        {
        }

        string jsonString = www.downloadHandler.text;

        _firstPart = JsonUtility.FromJson<RootObject>(jsonString).First;
        _secondPart = JsonUtility.FromJson<RootObject>(jsonString).Second;
        */
/*
        using (StreamReader r = new StreamReader("Assets/StreamingAssets/names.json"))
        {
            string json = r.ReadToEnd();
            _firstPart = JsonUtility.FromJson<RootObject>(json).First;
            _secondPart = JsonUtility.FromJson<RootObject>(json).Second;
        }*/
/*
        var _path = Application.streamingAssetsPath + "/names.json";
        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(_path);
        www.SendWebRequest();
        while (!www.isDone)
        {
        }
        string jsonString = www.downloadHandler.text;
        */

        string filePath = Application.streamingAssetsPath + "/names.json";
        string jsonString;

        //   if (Application.platform == RuntimePlatform.Android) //Need to extract file from apk first
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            www.SendWebRequest();
            while (!www.isDone)
            {
            }

            jsonString = www.downloadHandler.text;

            _firstPart = JsonUtility.FromJson<RootObject>(jsonString).First;
            _secondPart = JsonUtility.FromJson<RootObject>(jsonString).Second;
        }
        //    else
        {
            //        jsonString = File.ReadAllText(filePath);
        }
    }
}