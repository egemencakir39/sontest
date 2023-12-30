using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth;
    public GameObject itemPrefab;
    public float knockbackForce = 5f;
    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            CharacterController.playerScore += 10;
        }
        else
        {
            Knockback();
        }
    }
    void Knockback()
    {
        Vector2 knockbackDirection = (transform.position - CharacterController.playerPosition.position).normalized;
        GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackDirection.x * knockbackForce, knockbackDirection.y * knockbackForce);
    }

    void Die()
    {
        
        Instantiate(itemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject); // Düþman objesini yok edin

    }
    void OnTriggerEnter2d(Collider other)
    {
        if (other.gameObject == itemPrefab)
        {
            
            Destroy(gameObject); 
        }
    }
}