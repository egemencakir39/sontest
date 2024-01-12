using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;
    soundManager SoundManagerScript;
   



    private void Start()
    {
        SoundManagerScript = GameObject.Find("SoundManager").GetComponent<soundManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            SoundManagerScript.takingDamage_();
            playerHealth.UpdateHealth(-damageAmount);
           
            
        }
    }
}
