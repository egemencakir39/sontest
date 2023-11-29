using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float life = 2;
    public GameObject bulletPrefab;
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
            
        }
    }
}