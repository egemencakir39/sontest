using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRun : MonoBehaviour
{
    public float speed = 5f; // Ko�ma h�z�
    public float detectionRange = 5f; // Oyuncuyu alg�lama mesafesi
    private Transform player; // Oyuncu konumu

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncu konumunu bul
    }

    void Update()
    {
        // Oyuncuya belirli bir mesafeden yakla��ld���nda
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            // Oyuncuya do�ru ko�
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Oyuncuya de�ildi�inde silin
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
