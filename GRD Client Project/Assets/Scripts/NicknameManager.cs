using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

/// <summary>
/// This class reads the names from the .json file and displays it on the screen. The user can randomise using UI buttons.
/// - Natalia Pietrzak
/// </summary>
public class NicknameManager : MonoBehaviour
{
    [SerializeField] private List<FirstPart> _firstPart = new List<FirstPart>();

    [SerializeField] private List<SecondPart> _secondPart = new List<SecondPart>();

    private void Awake()
    {
        StartCoroutine(LoadJsonNames());
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

    private IEnumerator LoadJsonNames()
    {
        string filePath = Application.streamingAssetsPath + "/names.json";

        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();
            if (string.IsNullOrEmpty(www.error))
            {
                string json = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);

                _firstPart = JsonUtility.FromJson<RootObject>(json).First;
                _secondPart = JsonUtility.FromJson<RootObject>(json).Second;
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
}