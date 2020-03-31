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

    [SerializeField] private byte[] _hashedPassword;
//  [SerializeField] private MD5 _hashedPassword;

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
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(enteredPassword);

        //  var newHash = Crypto.ComputeMD5Hash(bytes);

        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();


        //    MD5 md5 = MD5.Create(enteredPassword);


        //  MD5 md5 = MD5.Create();
        //byte[] bytes = Encoding.ASCII.GetBytes(usedString+secretKey);     // this wrong because can't receive korean character
        //   byte[] bytes = Encoding.UTF8.GetBytes(usedString + secretKey);
        byte[] newHash = md5.ComputeHash(bytes);

        //   _hashedPassword = newHash;
        if (_hashedPassword.SequenceEqual(newHash))
            //  if (_hashedPassword == md5)

        {
            Debug.Log("correct");

            return true;
        }

        Debug.Log("incorrect");

        return false;
    }
}