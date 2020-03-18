using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image _timerImage;
    [SerializeField] private Level _currentMiniGame;
    public int timeLeft, timePassed;


    private void Start()
    {
        _currentMiniGame = FindObjectOfType<Level>();
    }

    public IEnumerator StartStopwatch(float duration)
    {
        float totalTime = 0;
        while (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            timePassed = (int) totalTime;
            Debug.Log(timePassed);
            yield return null;
        }
    }

    public IEnumerator Countdown(float duration)
    {
        float timeRatio;
        float totalTime = duration;
        while (totalTime >= 0)
        {
            totalTime -= Time.deltaTime;
            timeLeft = (int) totalTime;
            timeRatio = timeLeft / duration;
            _timerImage.fillAmount = timeRatio;
            timeText.text = timeLeft.ToString();
            yield return null;
        }

        _currentMiniGame.FinishMiniGame();
    }
}