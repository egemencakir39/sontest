using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float life = 3;
    public GameObject bulletPrefab;
    void Awake()
    {
     Destroy(gameObject, life);   
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (bulletPrefab.activeSelf && other.CompareTag("enemy"))
        {
            Debug.Log("Deðdi mermi");
            Destroy(gameObject);
        }
    }
}
