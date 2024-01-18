using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public float teleportDistance = 2f;
    public Transform firePoint;
    private Transform targetPos;
    private bool isTeleporting = false;
    private bool isAttacking = false;
    public GameObject enemyBullet;
    public float attackCooldown = 2f;
    private Transform player;
    public float teleportCoolDown = 2;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerPosition());

        if (distanceToPlayer <= teleportDistance && !isTeleporting)
        {
            StartCoroutine(Teleport());
        }
    }
    void UpdateFacingDirection(Vector2 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
        {
            // Player saðda, yüzü saða baksýn
            transform.localScale = new Vector3(2.5f, 2.5f, 2f);
        }
        else
        {
            // Player solda, yüzü sola baksýn
            transform.localScale = new Vector3(-2.5f, 2.5f, 2f);
        }
    }

    IEnumerator Teleport()
    {
        isTeleporting = true;

        yield return new WaitForSeconds(teleportCoolDown);

        targetPos = (targetPos == pos1) ? pos2 : pos1;

        transform.position = targetPos.position;
        isTeleporting = false;
    }

    // Oyuncu pozisyonunu döndür
    private Vector2 PlayerPosition()
    {
        return player.position;
    }

    IEnumerator AttackCooldown()
    {
        while (true)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, PlayerPosition());

            if (distanceToPlayer <= 10f && !isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger("attackboss");
                UpdateFacingDirection(player.position);

                yield return new WaitForSeconds(.5f);
                try
                {
                    
                    if (enemyBullet != null && firePoint != null)
                    {
                        GameObject fire = Instantiate(enemyBullet, firePoint.position, Quaternion.identity);

                        // Oyuncuya göre ateþin yüzünü döndür
                        Vector2 direction = (player.position - firePoint.position).normalized;
                        UpdateFacingDirection(player.position);

                        // Y ekseninde hareket etmesini engelle (sadece saða veya sola gitmesini saðla)
                        direction.y = 0;

                        fire.GetComponent<Rigidbody2D>().velocity = direction.normalized * 10f;

                        Destroy(fire, 3f);
                    }

                    yield return new WaitForSeconds(attackCooldown);
                }
                finally
                {
                    isAttacking = false;
                }
            }

            yield return null;
        }
    }
}