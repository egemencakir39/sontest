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

    void UpdateFacingDirection(Vector2 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
        {
            // Player sa�da, y�z� sa�a baks�n
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else
        {
            // Player solda, y�z� sola baks�n
            transform.localScale = new Vector3(-3f, 3f, 3f);
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

                // Y�z� do�ru y�ne �evir
                UpdateFacingDirection(player.position);
            }
            else
            {
                // a ve b noktalar� aras�nda bir collider olu�tur ve o collidera player girerse player� takip et
                Collider2D areaCollider = GetComponent<Collider2D>();
                Vector2 areaCenter = (pointA.position + pointB.position) / 2f;
                Vector2 areaSize = new Vector2(Vector2.Distance(pointA.position, pointB.position), attackDistance * 2f);
                Bounds areaBounds = new Bounds(areaCenter, areaSize);

                if (areaBounds.Contains(player.position))
                {
                    // e�er player belirlenen alandaysa player� takip et
                    transform.position = Vector2.MoveTowards(transform.position, player.position, trackingSpeed * Time.deltaTime);

                    // Y�z� do�ru y�ne �evir
                    UpdateFacingDirection(player.position);
                }
                else
                {
                    // belirlenen alanda de�ilse normal hareket et
                    transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, enemySpeed * Time.deltaTime);

                    if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
                    {
                        ChangeTarget();
                    }

                    // Y�z� do�ru y�ne �evir
                    UpdateFacingDirection(targetPoint.position);
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
                Vector2 direction = (player.position - firePoint.position).normalized;

                GameObject fire = Instantiate(enemyBullet, firePoint.position, Quaternion.identity);

                // Nesnenin y�z�n� atan y�ne g�re d�nd�r
                UpdateObjectFacingDirection(fire, direction);

                // Nesneyi hareket ettir
                fire.GetComponent<Rigidbody2D>().velocity = direction * 10f;
            }
        }
        finally
        {
            isAttacking = false;
        }
    }





    void UpdateObjectFacingDirection(GameObject obj, Vector2 direction)
    {
        SpriteRenderer objRenderer = obj.GetComponent<SpriteRenderer>();
        if (objRenderer != null)
        {
            // Nesnenin y�z�n� atan y�ne g�re d�nd�r
            if (direction.x > 0)
            {
                obj.transform.localScale = new Vector3(1, 1, 1); // Nesne sa�a gidiyorsa, y�z� sa�a baks�n
            }
            else
            {
                obj.transform.localScale = new Vector3(-1, 1, 1); // Nesne sola gidiyorsa, y�z� sola baks�n
            }
        }
    }

    void ChangeTarget()
    {
        targetPoint = (targetPoint == pointA) ? pointB : pointA;
    }
}