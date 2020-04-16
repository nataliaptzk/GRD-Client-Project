using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : MonoBehaviour
{
    private bool _isPaused;
    [SerializeField] private GameObject _pauseMenu;

    private void Awake()
    {
        _isPaused = false;
        _pauseMenu.SetActive(false);
    }

    public void ChangePauseState()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            _isPaused = true;
        }
    }
}