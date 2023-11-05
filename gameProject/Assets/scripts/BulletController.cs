using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefabýný atayabileceðiniz bir alan
    public Transform firePoint;
    public float BulletSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack2();
        }
    }
    void Attack2() // Mermi atma
    {
        // Mermi örneðini oluþturun
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        if (transform.localScale.x > 0)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed, 0f); // Saða doðru atýþ
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-BulletSpeed, 0f); // Sola doðru atýþ
        }
        
    }
    
}
