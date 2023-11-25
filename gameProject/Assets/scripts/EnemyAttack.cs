using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer temas eden nesne bir "Player" ise
        if (other.CompareTag("Player"))
        {
            // PlayerHealth scriptine eriþim saðla
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Eðer PlayerHealth scripti varsa ve oyuncunun saðlýðýna zarar verecek kadar saðlýk varsa
            if (playerHealth != null && playerHealth.currentHealth > damageAmount)
            {
                // Oyuncunun saðlýðýný azalt
                playerHealth.UpdateHealth(-damageAmount);
            }
        }
    }
}
