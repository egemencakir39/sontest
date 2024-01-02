using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosSControl : MonoBehaviour
{
    public Transform startPoint; // Ba�lang�� noktas�
    public Transform endPoint;   // Biti� noktas�
    public float speed = 5f;     // D��man�n hareket h�z�
    public float detectionRange = 5f; // Takip etme mesafesi
    private Animator animator;
    private Transform target;    // Hedef nokta

    void Start()
    {
        // Ba�lang��ta d��man�n hedefi biti� noktas� olsun
        target = endPoint;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Oyuncu belirli bir mesafede ise takip et
            if (distanceToPlayer < detectionRange)
            {
                target = player.transform;
            }
            else
            {
                // Oyuncu takip mesafesinde de�ilse, hedefi de�i�tir
                target = (target == endPoint) ? startPoint : endPoint;
            }
        }

        // D��man�n hedefe do�ru hareket etmesi
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        animator.SetBool("walk", true);

        // Y�z� gitti�i y�ne g�re d�nd�r
        Flip();
    }

    void Flip()
    {
        // D��man�n y�z�n� gitti�i y�ne g�re d�nd�r
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(2f, 2f, 2f); // Sa�a do�ru gidiyorsa normal y�nde b�rak
        }
        else if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(-2f, 2f, 2f); // Sola do�ru gidiyorsa yatayda d�nd�r
        }
    }
}