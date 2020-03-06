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


    public void CheckIfFinished()
    {
        if (_rubbishSlotsParent.transform.childCount == 1)
        {
            Invoke("FinishMiniGame", 0.1f);
        }
    }
}