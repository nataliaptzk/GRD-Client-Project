using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class DataCollectionFileManager
{
    private static string _fileName = "/datacollection.txt";

    public static void WriteStringContinuation(string text, bool ifAddSemicolon)
    {
        if (SessionManager.Consent)
        {
            string path = Application.persistentDataPath + _fileName;

            using (StreamWriter outputFile = new StreamWriter(path, true))
            {
                if (ifAddSemicolon)
                {
                    outputFile.Write(text + ";"); //adds to the current line
                }
                else
                {
                    outputFile.Write(text); //adds to the current line
                }

                outputFile.Flush();
                outputFile.Close();
            }
        }
    }

    public static void WriteStringNewLine(string text, string text2)
    {
        string path = Application.persistentDataPath + _fileName;
        if (!System.IO.File.Exists(path))
        {
            System.IO.File.WriteAllText(path, "");
        }


        using (StreamWriter outputFile = new StreamWriter(path, true))
        {
            outputFile.WriteLine(""); //makes a new line
            outputFile.Write(text + ";"); //adds to the current line
            outputFile.Write(text2 + ";"); //adds to the current line

            outputFile.Flush();
            outputFile.Close();
        }
    }

    public static void AdjustDataFileForReplay(int columnsAmountToRemove)
    {
        if (SessionManager.Consent)
        {
            string path = Application.persistentDataPath + _fileName;
            var row = File.ReadLines(path).Last();


            string[] columns = row.Split(new char[] {';'});
            string[] goodColumns = new string[columns.Length - columnsAmountToRemove];


            Array.Copy(columns, goodColumns, goodColumns.Length);

            string newRow = String.Join(";", goodColumns);

            newRow = newRow + ";";


            var fileContent = File.ReadLines(path).ToList();

            fileContent[fileContent.Count - 1] = newRow;
            string newLines = string.Join("\n", fileContent.ToArray());
            File.WriteAllText(path, newLines);
        }
    }
}