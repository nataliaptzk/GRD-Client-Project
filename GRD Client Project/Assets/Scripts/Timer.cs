using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Level _currentMiniGame;


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
            var integer = (int) totalTime;
            timeText.text = integer.ToString();
            yield return null;
        }
    }

    public IEnumerator Countdown(float duration)
    {
        float totalTime = duration;
        while (totalTime >= 0)
        {
            totalTime -= Time.deltaTime;
            var integer = (int) totalTime;
            timeText.text = integer.ToString();
            yield return null;
        }


        _currentMiniGame.FinishMiniGame();
    }
}