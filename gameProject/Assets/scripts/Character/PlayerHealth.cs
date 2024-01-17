using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool isDead = false;
    private bool canMove = true;
    Rigidbody2D rb;
    private CharacterController characterController;
    Animator animator;
   




    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
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
            gameObject.tag = "deadplayer";
            animator.SetTrigger("Dead");
        }
        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (currentHealth != 0)
        {
            gameObject.tag = "Player";
        }
    }



    private void die()
    {
          
        rb.velocity = Vector2.zero;


        canMove = false;
        isDead = true; // Ölüm durumunu aktif et
        StartCoroutine(RespawnAfterDelay());
        StartCoroutine(Cp());
        if (characterController != null)
        {
            characterController.enabled = false;

            // Belirli bir süre sonra scripti tekrar etkinleþtir
            Invoke("Etkinlestir", 2f);
        }
        SoundManagerScript.Dead_();
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
        
        if (rb != null)
        {
            Vector2 knockbackDirection = new Vector2(-1f, 1f); // Örneðin, sola ve yukarýya bir knockback
            rb.velocity = Vector2.zero; // Önceki hýzý sýfýrla
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }


    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        currentHealth = maxHealth;
        Debug.Log("Player Died");
        healthBar.SetMaxHealth(maxHealth);

        //gameObject.transform.position = cp.GetCurrentCheckpoint().position;
        
    }

    private IEnumerator Cp()
    {
        
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("Dead");
        gameObject.transform.position = cp.GetCurrentCheckpoint().position;
    }
    void Etkinlestir()
    {
        // Scripti tekrar etkinleþtir
        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }


}

