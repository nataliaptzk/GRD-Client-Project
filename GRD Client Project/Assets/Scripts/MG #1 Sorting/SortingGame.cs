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
        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
    }

    private void Start()
    {
        _rubbishGenerator.GeneratePlasticObjects(SessionManager.CurrentDifficulty, _rubbishSlotsParent);
        StartCoroutine(_timer.Countdown(SessionManager.CurrentDifficulty.duration * _miniGameBaseTime));
    }
}