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
                _score.CountCorrect();
                DataCollectionFileManager.WriteStringContinuation(other.GetComponent<Rubbish>().type.ToString());
                DataCollectionFileManager.WriteStringContinuation("correct");
            }
            else
            {
                _score.AddScore(-1 * SessionManager.CurrentDifficulty.pointsLossWhenIncorrect);
                _score.CountIncorrect();
                DataCollectionFileManager.WriteStringContinuation(other.GetComponent<Rubbish>().type.ToString());
                DataCollectionFileManager.WriteStringContinuation("incorrect");
            }

            Destroy(other.gameObject);
        }
    }
}