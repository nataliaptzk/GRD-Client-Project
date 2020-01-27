using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Score _score;
  //  public ContactFilter2D contactFilter = new ContactFilter2D();


    // Start is called before the first frame update
    private void OnMouseDown()
    {
    }

    private void OnMouseUp()
    {
        BoxCollider2D myCollider = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D[] colliders = new BoxCollider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D(); 
        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);

        // GameObject myColl = colliders[0].gameObject;

        if (colliders[0].gameObject.GetComponent<Bin>())
        {
            if (colliders[0].gameObject.GetComponent<Bin>().type == gameObject.GetComponent<Rubbish>().type)
            {
                _score.AddScore(1);
            }

            else
            {
                _score.AddScore(-1);
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