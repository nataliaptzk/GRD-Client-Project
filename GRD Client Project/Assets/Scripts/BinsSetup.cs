using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinsSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bins = new List<GameObject>();

    private void Start()
    {
        BinSetup();
    }

    private void BinSetup()
    {
        if (SessionManager.CurrentDifficulty.name == "easy" || SessionManager.CurrentDifficulty.name == "normal")
        {
            _bins[0].SetActive(true);
            _bins[1].SetActive(false);
        }
        else if (SessionManager.CurrentDifficulty.name == "hard")
        {
            _bins[0].SetActive(false);
            _bins[1].SetActive(true);
        }
    }
}