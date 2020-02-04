using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGame : MonoBehaviour
{
    [SerializeField] private MiniGameInfo _gameInfo;

    [SerializeField] private GameObject _rubbishSlotsParent;

//    [SerializeField] private SessionManager _sessionManager;
    private GameManager _gameManager;
    private RubbishGenerator _rubbishGenerator;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
    }

    private void Start()
    {
        // _sessionManager = FindObjectOfType<SessionManager>();
        _rubbishGenerator.GeneratePlasticObjects(SessionManager.CurrentDifficulty, _rubbishSlotsParent);
    }
}