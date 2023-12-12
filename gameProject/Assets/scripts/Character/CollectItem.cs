using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int healthToAdd = 10;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // sa�l�k+
            other.GetComponent<PlayerHealth>().UpdateHealth(healthToAdd);

            
            Destroy(gameObject);
        }
    }
}