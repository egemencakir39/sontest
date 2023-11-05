using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab�n� atayabilece�iniz bir alan
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
        // Mermi �rne�ini olu�turun
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        if (transform.localScale.x > 0)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed, 0f); // Sa�a do�ru at��
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-BulletSpeed, 0f); // Sola do�ru at��
        }
        
    }
    
}
