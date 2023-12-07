using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
   // public Transform respawnPoint;
    private ChekPointSystem cp;



    private void Start()
    {
        cp = gameObject.GetComponent<ChekPointSystem>();
        // Sahne 1 y�klendi�inde maxHealth ile currentHealth'i e�itle
        if (SceneManager.GetActiveScene().name == "bolum1")
        {
            currentHealth = maxHealth;
        }
        else
        {
            // PlayerPrefs'ten kaydedilmi� sa�l�k de�erini al
            if (PlayerPrefs.HasKey("PlayerHealth"))
            {
                currentHealth = PlayerPrefs.GetInt("PlayerHealth");
            }
            else
            {
                // E�er daha �nce bir de�er kaydedilmemi�se, ba�lang��ta maksimum sa�l�k de�eri ile ba�la
                currentHealth = Mathf.Clamp(maxHealth, 0, maxHealth);
            }
        }
    }

    private void OnDisable()
    {
        // Sahneden ��k�ld���nda PlayerPrefs'e g�ncel sa�l�k de�erini kaydet
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();
    }

    // Sa�l�k de�erini g�ncellemek i�in kullan�lan fonksiyon
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;

        // Sa�l�k de�erini maksimum sa�l�k de�eri aras�nda s�n�rla
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Sa�l�k de�eri g�ncellendikten sonra PlayerPrefs'e kaydet
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();

        // Can 0'a d��erse karakteri �ld�r
        if (currentHealth == 0)
        {
            die();
        }
    }

    // Karakterin �l�m durumunu kontrol etmek i�in kullan�lan fonksiyon
   
    private void die()
    {
       // yield return new WaitForSeconds(2);
        currentHealth = maxHealth;
        Debug.Log("Player Died");
        gameObject.transform.position = cp.GetCurrentCheckpoint().position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            cp.CheckPointChanger(collision.gameObject.transform);
        }
    }

}