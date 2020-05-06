using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for counting the time in the mini-games.
/// - Natalia Pietrzak
/// </summary>
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

            int min = Mathf.FloorToInt(timeLeft / 60);
            int sec = Mathf.FloorToInt(timeLeft % 60);


            timeText.text = min.ToString("00") + ":" + sec.ToString("00");
            yield return null;
        }

        _currentMiniGame.FinishMiniGame();
    }
}