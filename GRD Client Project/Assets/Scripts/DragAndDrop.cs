using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Score _score;

    private SessionManager _sessionManager;


    private void Awake()
    {
        _sessionManager = FindObjectOfType<SessionManager>();
        _score = FindObjectOfType<Score>();
    }

    private void OnMouseDown()
    {
    }

    private void OnMouseUp()
    {
        BoxCollider2D myCollider = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D[] colliders = new BoxCollider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);


        if (colliders[0] != null && colliders[0].gameObject.HasComponent<Bin>())
        {
            if (colliders[0].gameObject.GetComponent<Bin>().type == gameObject.GetComponent<Rubbish>().type)
            {
                _score.AddScore(1 * _sessionManager.currentDifficulty.pointsGainWhenCorrect);
            }

            else
            {
                _score.AddScore(-1 * _sessionManager.currentDifficulty.pointsLossWhenIncorrect);
            }

            Destroy(gameObject);
        }
    }

    public void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }
}