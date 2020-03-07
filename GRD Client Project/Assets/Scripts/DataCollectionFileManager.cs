using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class DataCollectionFileManager
{
    private static string _fileName = "/testfile.txt";

    public static void WriteStringContinuation(string text)
    {
        if (CheckIfFileExists(_fileName))
        {
            string path = Application.persistentDataPath + _fileName;

            using (StreamWriter outputFile = new StreamWriter(path, true))
            {
                outputFile.Write(text + ";"); //adds to the current line

                outputFile.Flush();
                outputFile.Close();
            }
        }
    }

    public static void WriteStringNewLine(string text)
    {
        if (CheckIfFileExists(_fileName))
        {
            string path = Application.persistentDataPath + _fileName;

            using (StreamWriter outputFile = new StreamWriter(path, true))
            {
                outputFile.WriteLine(""); //makes a new line
                outputFile.Write(text + ";"); //adds to the current line

                outputFile.Flush();
                outputFile.Close();
            }
        }
    }


    private static bool CheckIfFileExists(string fileName)
    {
        bool temp;
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            temp = true;
        }
        else
        {
            temp = false;
        }

        return temp;
    }
}