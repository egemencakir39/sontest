using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingFloor : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float bouncingForce = 20f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
       rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 bouncingVelocity = rb.velocity;
            bouncingVelocity.y = bouncingForce;
            rb.velocity = bouncingVelocity;
        }
    }
}
