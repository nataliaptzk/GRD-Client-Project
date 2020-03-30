using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityGoogleDrive;

public class Admin : MonoBehaviour
{
    [ContextMenu("test function")]
    public void Test()
    {
        string testContent = "Hello my name is Biodegradability";

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(testContent);

        var file = new UnityGoogleDrive.Data.File() {Name = "test.txt", Content = bytes};
        GoogleDriveFiles.Create(file).Send();
    }
}