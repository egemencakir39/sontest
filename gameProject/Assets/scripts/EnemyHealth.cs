using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Düþmanýn maksimum saðlýðý
    public int currentHealth; // Þu anki saðlýk
    public GameObject itemPrefab; // Oluþturulacak item prefabý
   

    void Start()
    {
        currentHealth = maxHealth; // Baþlangýçta düþmanýn maksimum saðlýðýna sahip olur
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Düþmanýn saðlýðýný aldýðý hasar kadar azalt

        if (currentHealth <= 0)
        {
            Die(); // Düþmanýn saðlýðý sýfýr veya daha azsa, ölüm iþlemi
            CharacterController.playerScore += 10;
        }
    }

    void Die()
    {
        // Düþmanýn ölüm iþlemini burada gerçekleþtirin
        // Örneðin, ölüm animasyonunu baþlatabilir veya düþman objesini yok edebilirsiniz

        // Ölüm iþlemi gerçekleþtikten sonra, toplanabilir itemi oluþturun
        Instantiate(itemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject); // Düþman objesini yok edin
    }
}