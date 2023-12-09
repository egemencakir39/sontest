using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private float trackingSpeed = 4f;
    [SerializeField] private float attackDistance = 5f;
    [SerializeField] private float attackCooldown = 2f;
    public GameObject enemyBullet;
    public Transform firePoint;

    private bool isAttacking = false;
    private Transform targetPoint;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            targetPoint = pointA;
            StartCoroutine(EnemyBehaviour());
        }
    }

    IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackDistance && !isAttacking)
            {
                StartCoroutine(AttackCooldown());
                StartCoroutine(TrackPlayer());
            }
            else
            {
                // a ve b noktalarý arasýnda bir collider oluþtur ve o collidera player girerse playerý takip et
                Collider2D areaCollider = GetComponent<Collider2D>();
                Vector2 areaCenter = (pointA.position + pointB.position) / 2f;
                Vector2 areaSize = new Vector2(Vector2.Distance(pointA.position, pointB.position), attackDistance * 2f);
                Bounds areaBounds = new Bounds(areaCenter, areaSize);

                if (areaBounds.Contains(player.position))
                {
                    // eðer player belirlenen alandaysa playerý takip et
                    transform.position = Vector2.MoveTowards(transform.position, player.position, trackingSpeed * Time.deltaTime);
                }
                else
                {
                    // belirlenen alanda deðilse normal hareket et
                    transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, enemySpeed * Time.deltaTime);

                    if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
                    {
                        ChangeTarget();
                    }
                }
            }

            yield return null;
        }
    }

    IEnumerator TrackPlayer()
    {
        while (!isAttacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, trackingSpeed * Time.deltaTime);
            }
            else
            {
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator AttackCooldown()
    {
        isAttacking = true;

        try
        {
            yield return new WaitForSeconds(attackCooldown);

            if (enemyBullet != null && firePoint != null)
            {
                GameObject fire = Instantiate(enemyBullet, firePoint.position, Quaternion.identity);
                Vector2 direction = (player.position - firePoint.position).normalized;
                fire.GetComponent<Rigidbody2D>().velocity = direction * 10f;
            }
        }
        finally
        {
            isAttacking = false;
        }
    }

    void ChangeTarget()
    {
        targetPoint = (targetPoint == pointA) ? pointB : pointA;
    }
}