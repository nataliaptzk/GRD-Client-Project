using UnityEngine;

/// <summary>
/// This class checks the collisions the bin has with rubbish game objects.
/// - Natalia Pietrzak
/// </summary>\
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
                DataCollectionFileManager.WriteStringContinuation(other.GetComponent<Rubbish>().type.ToString(), true);
                DataCollectionFileManager.WriteStringContinuation("correct", true);
            }
            else
            {
                _score.AddScore(-1 * SessionManager.CurrentDifficulty.pointsLossWhenIncorrect);
                _score.CountIncorrect();
                DataCollectionFileManager.WriteStringContinuation(other.GetComponent<Rubbish>().type.ToString(), true);
                DataCollectionFileManager.WriteStringContinuation("incorrect", true);
            }

            Destroy(other.gameObject);
        }
    }
}