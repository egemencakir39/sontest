using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRun : MonoBehaviour
{
    public float speed = 5f; // Koþma hýzý
    public float detectionRangeX = 8f; // Sadece x ekseni üzerindeki algýlama mesafesi
    public float detectionRangeY = 2f; // Sadece y ekseni üzerindeki algýlama mesafesi
    private Transform player; // Oyuncu konumu

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncu konumunu bul
    }

    void Update()
    {
        // Oyuncuya belirli bir mesafeden x ve y ekseni üzerinde yaklaþýldýðýnda
        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

        if (distanceToPlayerX < detectionRangeX && distanceToPlayerY < detectionRangeY)
        {
            // Oyuncuya doðru koþ
            float moveDirection = Mathf.Sign(player.position.x - transform.position.x);
            transform.Translate(Vector2.right * moveDirection * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}