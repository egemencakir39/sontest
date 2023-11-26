using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Transform respawnPoint;



    private void Start()
    {
        // Sahne 1 yüklendiðinde maxHealth ile currentHealth'i eþitle
        if (SceneManager.GetActiveScene().name == "bolum1")
        {
            currentHealth = maxHealth;
        }
        else
        {
            // PlayerPrefs'ten kaydedilmiþ saðlýk deðerini al
            if (PlayerPrefs.HasKey("PlayerHealth"))
            {
                currentHealth = PlayerPrefs.GetInt("PlayerHealth");
            }
            else
            {
                // Eðer daha önce bir deðer kaydedilmemiþse, baþlangýçta maksimum saðlýk deðeri ile baþla
                currentHealth = Mathf.Clamp(maxHealth, 0, maxHealth);
            }
        }
    }

    private void OnDisable()
    {
        // Sahneden çýkýldýðýnda PlayerPrefs'e güncel saðlýk deðerini kaydet
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();
    }

    // Saðlýk deðerini güncellemek için kullanýlan fonksiyon
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;

        // Saðlýk deðerini maksimum saðlýk deðeri arasýnda sýnýrla
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Saðlýk deðeri güncellendikten sonra PlayerPrefs'e kaydet
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();

        // Can 0'a düþerse karakteri öldür
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Karakterin ölüm durumunu kontrol etmek için kullanýlan fonksiyon
    private void Die()
    {
        // Burada karakterin ölme iþlemleri gerçekleþtirilebilir
        // Örneðin, karakterin animasyonunu deðiþtir, oyunu durdur veya yeniden baþlat gibi iþlemler
        Debug.Log("Player Died");

        // Karakteri sil
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;

        // Karakteri checkpoint'ten yeniden baþlat

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Karakter checkpoint'e temas ettiðinde respawnPoint'i güncelle
        if (other.CompareTag("Checkpoint"))
        {
            ChekPointSystem checkpoint = other.GetComponent<ChekPointSystem>();
            if (checkpoint != null && checkpoint.IsChecked())
            {
                respawnPoint = other.transform;
            }
        }
    }
}