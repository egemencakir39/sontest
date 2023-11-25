using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er temas eden nesne bir "Player" ise
        if (other.CompareTag("Player"))
        {
            // PlayerHealth scriptine eri�im sa�la
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // E�er PlayerHealth scripti varsa ve oyuncunun sa�l���na zarar verecek kadar sa�l�k varsa
            if (playerHealth != null && playerHealth.currentHealth > damageAmount)
            {
                // Oyuncunun sa�l���n� azalt
                playerHealth.UpdateHealth(-damageAmount);
            }
        }
    }
}
