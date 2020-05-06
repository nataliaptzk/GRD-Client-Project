using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for setting up the help screen for the current difficulty.
/// - Natalia Pietrzak
/// </summary>
public class HelpScreenSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _helps = new List<GameObject>();

    private void Start()
    {
        HelpSetup();
    }

    private void HelpSetup()
    {
        if (SessionManager.CurrentDifficulty.name == "easy")
        {
            _helps[0].SetActive(true);
            _helps[1].SetActive(false);
            _helps[2].SetActive(false);
        }
        else if (SessionManager.CurrentDifficulty.name == "normal")
        {
            _helps[0].SetActive(false);
            _helps[1].SetActive(true);
            _helps[2].SetActive(false);
        }
        else if (SessionManager.CurrentDifficulty.name == "hard")
        {
            _helps[0].SetActive(false);
            _helps[1].SetActive(false);
            _helps[2].SetActive(true);
        }
    }
}