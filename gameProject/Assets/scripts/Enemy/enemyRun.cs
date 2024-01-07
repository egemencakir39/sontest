using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRun : MonoBehaviour
{
    public float speed = 5f; // Koþma hýzý
    public float detectionRange = 5f; // Oyuncuyu algýlama mesafesi
    private Transform player; // Oyuncu konumu

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncu konumunu bul
    }

    void Update()
    {
        // Oyuncuya belirli bir mesafeden yaklaþýldýðýnda
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            // Oyuncuya doðru koþ
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Oyuncuya deðildiðinde silin
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
