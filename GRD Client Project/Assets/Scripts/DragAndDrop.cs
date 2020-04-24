using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Score _score;
    private SortingGame _sortingGame;

    private void Awake()
    {
        _sortingGame = FindObjectOfType<SortingGame>();
        _score = FindObjectOfType<Score>();
    }

    private void OnMouseDown()
    {
    }

    private void OnMouseUp()
    {
        BoxCollider2D myCollider = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D[] colliders = new BoxCollider2D[15];
        ContactFilter2D contactFilter = new ContactFilter2D();
        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);

        if (gameObject.HasComponent<Rigidbody2D>())
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        for (int i = 0; i < colliderCount; i++)
        {
            if (colliders[i].gameObject.HasComponent<Bin>())
            {
                if (colliders[i].gameObject.GetComponent<Bin>().type == gameObject.GetComponent<Rubbish>().type)
                {
                    _score.AddScore(1 * SessionManager.CurrentDifficulty.pointsGainWhenCorrect);
                    _score.CountCorrect();
                    DataCollectionFileManager.WriteStringContinuation(gameObject.GetComponent<Rubbish>().type.ToString(), true);
                    DataCollectionFileManager.WriteStringContinuation("correct", true);
                }

                else
                {
                    _score.AddScore(-1 * SessionManager.CurrentDifficulty.pointsLossWhenIncorrect);
                    _score.CountIncorrect();
                    DataCollectionFileManager.WriteStringContinuation(gameObject.GetComponent<Rubbish>().type.ToString(), true);
                    DataCollectionFileManager.WriteStringContinuation("incorrect", true);
                }

                _sortingGame.CheckIfFinished();
                Destroy(gameObject);
                break;
            }
        }
    }

    public void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
        if (gameObject.HasComponent<Rigidbody2D>())
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}