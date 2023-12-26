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
        while (true) // Sonsuz bir döngü
        {
            float distanceToPlayer = Vector2.Distance(transform.position, PlayerPosition());

            if (distanceToPlayer <= 10f && !isAttacking)
            {
                isAttacking = true;

                try
                {
                    if (enemyBullet != null && firePoint != null)
                    {
                        GameObject fire = Instantiate(enemyBullet, firePoint.position, Quaternion.identity);
                        Vector2 direction = (player.position - firePoint.position).normalized;
                        fire.GetComponent<Rigidbody2D>().velocity = direction * 10f;
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