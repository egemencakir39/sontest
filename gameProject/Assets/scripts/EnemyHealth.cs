using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // D��man�n maksimum sa�l���
    public int currentHealth; // �u anki sa�l�k
    public GameObject itemPrefab; // Olu�turulacak item prefab�
   

    void Start()
    {
        currentHealth = maxHealth; // Ba�lang��ta d��man�n maksimum sa�l���na sahip olur
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // D��man�n sa�l���n� ald��� hasar kadar azalt

        if (currentHealth <= 0)
        {
            Die(); // D��man�n sa�l��� s�f�r veya daha azsa, �l�m i�lemi
            CharacterController.playerScore += 10;
        }
    }

    void Die()
    {
        // D��man�n �l�m i�lemini burada ger�ekle�tirin
        // �rne�in, �l�m animasyonunu ba�latabilir veya d��man objesini yok edebilirsiniz

        // �l�m i�lemi ger�ekle�tikten sonra, toplanabilir itemi olu�turun
        Instantiate(itemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject); // D��man objesini yok edin
    }
}