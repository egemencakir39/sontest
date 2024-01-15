using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    soundManager SoundManagerScript;
    public int maxHealth = 100;
    public int currentHealth;
   // public Transform respawnPoint;
    private ChekPointSystem cp;
    public healthBar healthBar;
    private AudioSource audioSource;
    private float knockbackForce = 10f;




    private void Start()
    {
        SoundManagerScript = GameObject.Find("SoundManager").GetComponent<soundManager>();
        audioSource = GetComponent<AudioSource>();
        cp = gameObject.GetComponent<ChekPointSystem>();
        // sahne yüklendiðinde maxHealth ile currentHealthi eþitle
        if (SceneManager.GetActiveScene().name == "level1")
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        if (SceneManager.GetActiveScene().name == "level2")
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
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
        
        if (amount < 0)
        {
          KnockBack();
        }


            currentHealth += amount;
        healthBar.SetHealth(currentHealth);

        // max saðlýða sýnýrla
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

       

        // Can 0'a düþerse karakteri öldür
        if (currentHealth == 0)
        {
            die();
        }
        healthBar.SetHealth(currentHealth);
    }
    private void die()
    {
        //yield return new WaitForSeconds(delay);

        currentHealth = maxHealth;
        Debug.Log("Player Died");
        gameObject.transform.position = cp.GetCurrentCheckpoint().position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            cp.CheckPointChanger(collision.gameObject.transform);
            SoundManagerScript.CheckPoint_();
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void KnockBack()
    {
        Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            Vector2 knockbackDirection = new Vector2(-1f, 1f); // Örneðin, sola ve yukarýya bir knockback
            playerRigidbody.velocity = Vector2.zero; // Önceki hýzý sýfýrla
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

}