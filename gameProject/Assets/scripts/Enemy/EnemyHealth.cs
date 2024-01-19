using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; 
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private int point = 10;

    void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(int attackDamage)
    {
        currentHealth -= attackDamage;

        if (currentHealth <= 0)
        {
            Die();
            CharacterController.playerScore += point;
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