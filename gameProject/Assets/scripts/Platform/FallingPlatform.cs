using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 2f;
    [SerializeField] private float respawnDelay = 5f; 
    [SerializeField] private Rigidbody2D rb;
    private Vector2 initialPosition; 

    void Start()
    {
        // pozisyonlarý eþitleme
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndRespawn());
        }
    }

    private IEnumerator FallAndRespawn()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyDelay);
        rb.bodyType = RigidbodyType2D.Static; 
        yield return new WaitForSeconds(respawnDelay);
        RespawnPlatform();
    }

    private void RespawnPlatform()
    {
        transform.position = initialPosition;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
