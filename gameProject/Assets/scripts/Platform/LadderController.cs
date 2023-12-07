using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderController : MonoBehaviour
{
    private float _vertical;
     [SerializeField] private float speed = 8f;
    private bool _isLadder;
    private bool _isClimbing;
    [SerializeField] private Rigidbody2D rb;
    void Update()
    {
        _vertical = Input.GetAxis("Vertical");
        if (_isLadder && Mathf.Abs(_vertical) > 0f)
        {
            _isClimbing = true;
        }
    }
    private void FixedUpdate()
    {
        if (_isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, _vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
          _isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
        }
    }
}
