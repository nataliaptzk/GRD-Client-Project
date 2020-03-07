using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGame : Level
{
    [SerializeField] private GameObject _rubbishSlotsParent;

    private RubbishGenerator _rubbishGenerator;


    private void Awake()
    {
        DisplayTutorialScreen();
        _gameManager = FindObjectOfType<GameManager>();

        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
    }

    private void Start()
    {
        _rubbishGenerator.GeneratePlasticObjects(SessionManager.CurrentDifficulty, _rubbishSlotsParent);
    }

    public void FillInDataCollectionForRemainingObjects()
    {
        //in case the timer runs out of time
        int tempCount = 0;
        tempCount = _rubbishSlotsParent.transform.childCount;

        for (int i = 0; i < tempCount; i++)
        {
            DataCollectionFileManager.WriteStringContinuation("run out of time");
            DataCollectionFileManager.WriteStringContinuation("N/A");

        }
    }

    public void CheckIfFinished()
    {
        if (_rubbishSlotsParent.transform.childCount == 1)
        {
            Invoke("FinishMiniGame", 0.1f);
        }
    }
}