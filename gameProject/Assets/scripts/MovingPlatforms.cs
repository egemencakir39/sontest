using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float speed = 2.0f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingToEnd = true;

    void Start()
    {
        startPosition = startPos.position;
        endPosition = endPos.position;
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        float step = speed * Time.deltaTime;

        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
        }

        if (transform.position == endPosition)
        {
            movingToEnd = false;
        }
        else if (transform.position == startPosition)
        {
            movingToEnd = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Platform �zerindeki nesneleri hareket etmesi i�in g�ncelle
        if (collision.gameObject.CompareTag("Player")) // Bu k�sm� ihtiyac�n�za g�re ayarlayabilirsiniz
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Platform �zerindeki nesnelerin hareketini durdur
        if (collision.gameObject.CompareTag("Player")) // Bu k�sm� ihtiyac�n�za g�re ayarlayabilirsiniz
        {
            collision.transform.parent = null;
        }
    }
}