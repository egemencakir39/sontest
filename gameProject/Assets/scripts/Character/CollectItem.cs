using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int healthToAdd = 10;  // Topland���nda eklenen sa�l�k puan�

    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er temas eden nesne bir "Player" ise
        if (other.CompareTag("Player"))
        {
            // Sa�l�k de�erini g�ncelle
            other.GetComponent<PlayerHealth>().UpdateHealth(healthToAdd);

            // Obje silinsin
            Destroy(gameObject);
        }
    }
}