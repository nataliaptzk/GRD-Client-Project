using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBin : MonoBehaviour
{
    private Score _score;

    private void Awake()
    {
        _score = FindObjectOfType<Score>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.HasComponent<Rubbish>())
        {
            if (other.GetComponent<Rubbish>().type == gameObject.GetComponent<Bin>().type)
            {
                _score.AddScore(1 * SessionManager.CurrentDifficulty.pointsGainWhenCorrect);
            }
            else
            {
                _score.AddScore(-1 * SessionManager.CurrentDifficulty.pointsLossWhenIncorrect);
            }

            Destroy(other.gameObject);
        }
    }
}