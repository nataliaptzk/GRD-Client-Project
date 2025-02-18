﻿using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class hold mini-games profiles and game difficulties.
/// - Natalia Pietrzak
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<MiniGameInfo> _miniGames = new List<MiniGameInfo>();
    [SerializeField] private List<Difficulty> _difficulties = new List<Difficulty>();

    public List<MiniGameInfo> MiniGames
    {
        get => _miniGames;
        set => _miniGames = value;
    }

    public List<Difficulty> Difficulties
    {
        get => _difficulties;
        set => _difficulties = value;
    }
}