using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGame : MonoBehaviour
{
    [SerializeField] private MiniGameInfo _gameInfo;

    [SerializeField] private GameObject _rubbishSlotsParent;

    private SessionManager _sessionManager;
    private GameManager _gameManager;
    private RubbishGenerator _rubbishGenerator;

    private void Awake()
    {
        _sessionManager = FindObjectOfType<SessionManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _rubbishGenerator = FindObjectOfType<RubbishGenerator>();
        
        _rubbishGenerator.GeneratePlasticObjects(_sessionManager.currentDifficulty, _rubbishSlotsParent);

    }
    
}