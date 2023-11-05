using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{   
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 'E' tuþuna basýldý mý?
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Mermi ateþle
            Shoot();
        }
    }
    

    // Mermi oluþturma ve ateþleme metodu
    void Shoot()
    {
       Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        float speed = 10f;
        if (rb.velocity.x > 0) 
        {
            bulletPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
        }
        else if (rb.velocity.x < 0)
        { 
         bulletPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2 (-speed, 0f);

        }
        else
        {
            bulletPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
        }

    }
}
