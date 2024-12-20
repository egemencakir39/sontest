using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordenemy : MonoBehaviour
{
    soundManager SoundManagerScript;
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;
    public int Damage = 10;
    private Animator animator;
    private bool movingToA = true;

    // Player takip �zellikleri
    public float chaseSpeed = 5f;
    public float chaseDistance = 10f;
    private Transform player;

    // Atak �zellikleri
    public float attackDistance = 5f; // Atak mesafesi
    public float timeBetweenAttacks = 2f; // Ataklar aras�ndaki s�re
    private float attackTimer;
    private bool isWalking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        SoundManagerScript = GameObject.Find("SoundManager").GetComponent<soundManager>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Oyuncuya yakla��ld���nda
            if (distanceToPlayer < chaseDistance)
            {
                // player takip sadece x ekseni
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
                isWalking = true;

                FlipTowardsPlayer(player.position.x);

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

                        // timer g�ncelle
                        attackTimer = Time.time + timeBetweenAttacks;
                    }
                }
            }
            else
            {
                if (movingToA)
                {
                    if (Vector2.Distance(transform.position, pointA.position) > 0.1f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
                        FlipTowardsDirection(true); // Sa�a bak�yor
                    }
                    else
                    {
                        movingToA = false;
                    }
                }
                else
                {
                    if (Vector2.Distance(transform.position, pointB.position) > 0.1f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
                        FlipTowardsDirection(false); // Sola bak�yor
                    }
                    else
                    {
                        movingToA = true;
                    }
                }

                isWalking = Vector2.Distance(transform.position, pointA.position) > 0.1f || Vector2.Distance(transform.position, pointB.position) > 0.1f;
            }
        }
        else
        {
            isWalking = false; // E�er player null ise, isWalking false yap
        }

        animator.SetBool("walk", isWalking);
    }

    void Attack1()
    {

        animator.SetTrigger("attack1");
        Debug.Log("Attack 1!");
        FlipTowardsPlayer(player.position.x);
       
    }

    void Attack2()
    {
        
        
        animator.SetTrigger("attack1");
        Debug.Log("Attack 2!");
        FlipTowardsPlayer(player.position.x);
       
    }


   
    void FlipTowardsPlayer(float playerX)
    {
        // Y�r�y�� y�n�ne g�re d�n��
        if (transform.position.x < playerX)
        {
            transform.localScale = new Vector3(2, 2, 2); // Normal y�nde
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 2); // Ters y�nde
        }
    }

    void FlipTowardsDirection(bool facingRight)
    {
        // Y�r�y�� y�n�ne g�re d�n��
        if (facingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Normal y�nde
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Ters y�nde
        }
    }
    public void PlayerDead()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}