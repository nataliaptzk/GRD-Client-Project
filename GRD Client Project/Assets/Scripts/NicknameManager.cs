using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
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
        using (StreamReader r = new StreamReader("Assets/Resources/names.json"))
        {
            string json = r.ReadToEnd();
            _firstPart = JsonUtility.FromJson<RootObject>(json).First;
            _secondPart = JsonUtility.FromJson<RootObject>(json).Second;
        }
    }
}