using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
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
            _pauseMenu.GetComponentInChildren<UIView>().InstantHide();
            _pauseMenu.SetActive(false);
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            _pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
            _isPaused = true;
        }
    }
}