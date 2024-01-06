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
        // sahne y�klendi�inde maxHealth ile currentHealthi e�itle
        if (SceneManager.GetActiveScene().name == "level1") 
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        else
        {
            // kaydedilmi� de�eri �ek
            if (PlayerPrefs.HasKey("PlayerHealth"))
            {
                currentHealth = PlayerPrefs.GetInt("PlayerHealth");
            }
            else
            {
                // baslang�c de�eri
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

    // health g�ncelle
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);

        // max sa�l��a s�n�rla
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // sa�l�k de�eri update
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();

        // Can 0'a d��erse karakteri �ld�r
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