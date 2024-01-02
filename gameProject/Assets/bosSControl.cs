using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosSControl : MonoBehaviour
{
    public Transform startPoint; // Baþlangýç noktasý
    public Transform endPoint;   // Bitiþ noktasý
    public float speed = 5f;     // Düþmanýn hareket hýzý
    public float detectionRange = 5f; // Takip etme mesafesi
    private Animator animator;
    private Transform target;    // Hedef nokta

    void Start()
    {
        // Baþlangýçta düþmanýn hedefi bitiþ noktasý olsun
        target = endPoint;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Oyuncu belirli bir mesafede ise takip et
            if (distanceToPlayer < detectionRange)
            {
                target = player.transform;
            }
            else
            {
                // Oyuncu takip mesafesinde deðilse, hedefi deðiþtir
                target = (target == endPoint) ? startPoint : endPoint;
            }
        }

        // Düþmanýn hedefe doðru hareket etmesi
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        animator.SetBool("walk", true);

        // Yüzü gittiði yöne göre döndür
        Flip();
    }

    void Flip()
    {
        // Düþmanýn yüzünü gittiði yöne göre döndür
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(2f, 2f, 2f); // Saða doðru gidiyorsa normal yönde býrak
        }
        else if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(-2f, 2f, 2f); // Sola doðru gidiyorsa yatayda döndür
        }
    }
}