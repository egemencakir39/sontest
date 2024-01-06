using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
   
    public int maxHealth = 100;
    public int currentHealth;
   // public Transform respawnPoint;
    private ChekPointSystem cp;
    public healthBar healthBar;
   



    private void Start()
    {
        cp = gameObject.GetComponent<ChekPointSystem>();
        // sahne yüklendiðinde maxHealth ile currentHealthi eþitle
        if (SceneManager.GetActiveScene().name == "level1") 
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        else
        {
            // kaydedilmiþ deðeri çek
            if (PlayerPrefs.HasKey("PlayerHealth"))
            {
                currentHealth = PlayerPrefs.GetInt("PlayerHealth");
            }
            else
            {
                // baslangýc deðeri
                currentHealth = Mathf.Clamp(maxHealth, 0, maxHealth);
            }
        }
    }

    private void OnDisable()
    {
        //health save
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();
    }

    // health güncelle
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);

        // max saðlýða sýnýrla
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // saðlýk deðeri update
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();

        // Can 0'a düþerse karakteri öldür
        if (currentHealth == 0)
        {
            die();
        }
        healthBar.SetHealth(currentHealth);
    }
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