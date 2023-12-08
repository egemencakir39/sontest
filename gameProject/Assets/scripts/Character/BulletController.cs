using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float life = 2;
    public GameObject bulletPrefab;
    public float cooldownTime = 1f; // Cooldown süresi
    private bool canHit = true; // Ateþ edebilir mi kontrolü
    public int attack2Damage = 15;
    void Awake()
    {
        Destroy(gameObject, life);
        
    }
   
        
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (bulletPrefab.activeSelf && other.CompareTag("enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(attack2Damage);
            Destroy(gameObject);
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(cooldownTime);
        canHit = true; // Cooldown süresi bittiðinde tekrar ateþ etmeye izin ver
    }
}