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

    public List<Difficulty> diffs = new List<Difficulty>();
    public RubbishGenerator rubbishGenerator;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rubbishGenerator.GeneratePlasticObjects(diffs[0], _rubbishSlotsParent);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            rubbishGenerator.GeneratePlasticObjects(diffs[1], _rubbishSlotsParent);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            rubbishGenerator.GeneratePlasticObjects(diffs[2], _rubbishSlotsParent);
        }
    }


    private void Awake()
    {
        _sessionManager = FindObjectOfType<SessionManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    
}