using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGoogleDrive;

//using UnityEngine.Windows;

public class Admin : MonoBehaviour
{
    [SerializeField] private GameObject _incorrectMessage;
    [SerializeField] private TextMeshProUGUI _driveInfo;
    [SerializeField] private byte[] _hashedPassword;
    [SerializeField] private string _response;
    [SerializeField] private GameObject _confirmationWindow;
    [SerializeField] private GameObject _removeFinishedWindow;

    private void Update()
    {
        CheckForLoggedInUser();
    }

    public void SendDataFile()
    {
        string filePath = Application.persistentDataPath + "/datacollection.txt";

        string testContent = File.ReadAllText(filePath);

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(testContent);

        var file = new UnityGoogleDrive.Data.File() {Name = "BiodegradabilityCollectedData.txt", Content = bytes};
        GoogleDriveFiles.Create(file).Send().OnDone += file1 => _response = Encoding.UTF8.GetString(file.Content);
    }

    public void RemoveConfirmation()
    {
        _confirmationWindow.SetActive(true);
    }

    public void RemoveData()
    {
        string filePath = Application.persistentDataPath + "/datacollection.txt";
        System.IO.File.WriteAllText(filePath, "");
        _removeFinishedWindow.SetActive(true);
        _confirmationWindow.SetActive(false);
    }

    public void AdminLoginAttempt(TMP_InputField enteredPassword)
    {
        if (ComputePassword(enteredPassword.text))
        {
            SceneManager.LoadScene("02 AdminScreen");
        }
        else
        {
            _incorrectMessage.SetActive(true);
        }
    }

    private void CheckForLoggedInUser()
    {
        var storedInfo = GoogleDriveSettings.LoadFromResources();

        if (storedInfo.CachedAccessToken == "")
        {
            _driveInfo.text = "no Google Drive connected.";
        }
        else
        {
            _driveInfo.text = "Google Drive connected.";
        }
    }


    public void LogoutUser()
    {
        var storedInfo = GoogleDriveSettings.LoadFromResources();
        storedInfo.DeleteCachedAuthTokens();
    }

    private bool ComputePassword(string enteredPassword)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(enteredPassword);

        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        byte[] newHash = md5.ComputeHash(bytes);

        if (_hashedPassword.SequenceEqual(newHash))

        {
            return true;
        }

        return false;
    }
}