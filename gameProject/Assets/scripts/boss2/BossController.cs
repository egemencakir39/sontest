using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;
    public int Damage = 10;
    private Animator animator;
    private bool movingToA = true;

    // Player takip özellikleri
    public float chaseSpeed = 5f;
    public float chaseDistance = 10f;
    private Transform player;

    // Atak özellikleri
    public float attackDistance = 5f; // Atak mesafesi
    public float timeBetweenAttacks = 2f; // Ataklar arasýndaki süre
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Oyuncuya yaklaþýldýðýnda
            if (distanceToPlayer < chaseDistance)
            {
                // player takip
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
               

                // atak mesafesi
                if (distanceToPlayer < attackDistance)
                {
                    // atak timer
                    if (Time.time > attackTimer)
                    {
                        // random atak

                        int randomAttack = Random.Range(1, 3); // 1 veya 2
                        if (randomAttack == 1)
                        {

                            Attack1();
                        }
                        else
                        {

                            Attack2();
                        }

                        // timer güncelle
                        attackTimer = Time.time + timeBetweenAttacks;
                    }
                }
            }
            else
            {
                // player yoksa iki nokta arasýnda gidip gel
                if (movingToA)
                {
                    transform.position = Vector2.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
                    {
                        movingToA = false;
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
                    {
                        movingToA = true;
                    }
                }
            }
        }
    }
    void Attack1()
    {
        Debug.Log("Attack 1!");
       animator.SetTrigger("Attack1Trigger");

    }


    void Attack2()
    {
        Debug.Log("Attack 2!");
       animator.SetTrigger("Attack2Trigger");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncuya hasar ver
            other.GetComponent<PlayerHealth>().UpdateHealth(-Damage); // damageAmount, kýlýcýn verdiði hasar miktarý
        }
    }
}