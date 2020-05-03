using UnityEngine;

public class Answer : MonoBehaviour
{
    public int answerIndex;
    private Quiz _quiz;
    private Score _score;
    private Vector3 _startPosition;

    private void Awake()
    {
        _quiz = FindObjectOfType<Quiz>();
        _score = FindObjectOfType<Score>();
        _startPosition = transform.position;
    }

    private void OnMouseDown()
    {
    }

    private void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    private void OnMouseUp()
    {
        BoxCollider2D myCollider = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D[] colliders = new BoxCollider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);


        if (colliders[0] != null && colliders[0].gameObject.HasComponent<AnswerDropArea>())
        {
            if (_quiz.correctAnswer == answerIndex)
            {
                _score.AddScore(1 * SessionManager.CurrentDifficulty.pointsGainWhenCorrect);
                _score.CountCorrect();
       //         DataCollectionFileManager.WriteStringContinuation("correct", true);
            }

            else
            {
                _score.AddScore(-1 * SessionManager.CurrentDifficulty.pointsLossWhenIncorrect);
                _score.CountIncorrect();
        //        DataCollectionFileManager.WriteStringContinuation("incorrect", true);
            }

            gameObject.SetActive(false);
            //   ResetToStartPosition();
            _quiz.NextQuestion();
        }
    }

    private void ResetToStartPosition()
    {
        transform.position = _startPosition;
    }
}