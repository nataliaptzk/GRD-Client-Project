using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGoogleDrive;
//using UnityEngine.Windows;

public class Admin : MonoBehaviour
{
    [SerializeField] private GameObject _incorrectMessage;
  //  [SerializeField] private byte[] _hashedPassword;
  [SerializeField] private MD5 _hashedPassword;

    public void Test()
    {
        string testContent = "Hello my name is Biodegradability";

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(testContent);

        var file = new UnityGoogleDrive.Data.File() {Name = "test.txt", Content = bytes};
        GoogleDriveFiles.Create(file).Send();
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

    public void LogoutUser()
    {
     //   GoogleDriveSettings.DeleteCachedAuthTokens;
    }

    private bool ComputePassword(string enteredPassword)
    {
     //   byte[] bytes = System.Text.Encoding.UTF8.GetBytes(enteredPassword);

      //  var newHash = Crypto.ComputeMD5Hash(bytes);
        
        MD5 md5 = MD5.Create(enteredPassword);

     //   if (_hashedPassword.SequenceEqual(md5))
        if (_hashedPassword == md5)
        {
            return true;
        }

        return false;
    }
}