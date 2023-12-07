using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int healthToAdd = 10;  // Toplandýðýnda eklenen saðlýk puaný

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer temas eden nesne bir "Player" ise
        if (other.CompareTag("Player"))
        {
            // Saðlýk deðerini güncelle
            other.GetComponent<PlayerHealth>().UpdateHealth(healthToAdd);

            // Obje silinsin
            Destroy(gameObject);
        }
    }
}