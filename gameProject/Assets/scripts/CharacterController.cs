using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // hareket h�z�
    public float jumpForce = 10f; // z�plama g�c�
    public int extraJumps = 1; // �ift z�plama hakk�
    public float dashingPower = 24f; // dash g�c�
    public float dashingTime = 0.2f; // dash s�resi
    public float dashingCoolDown = 1f;
    //sald�r�
    public GameObject swordCollider;//k�l�� colldier
    public float attackCooldown = 1.0f;
    public int attackDamage = 10;
    private int remainingJumps; // tekrarlanabilir z�plama
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool canDash = true;
    private bool isDashing;
    private bool canAttack = true;
    [SerializeField] private TrailRenderer tr;
    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        
        remainingJumps = extraJumps;
    }

    private void Update()
    {
        if (canAttack && Input.GetMouseButtonDown(0))
        {
            Attack1();
        }

        flipface();
        // zemin temas kontrol�
        isGrounded = Physics2D.IsTouchingLayers(col, LayerMask.GetMask("Ground"));

        // Z�plama
        if (isGrounded)
        {
            remainingJumps = extraJumps;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || remainingJumps > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            remainingJumps--;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) //dash tu� kontrol�
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        // Karakter hareketi
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        
        
    }
     void flipface() // karakterin y�z�n� d�nd�rme
    {
       Vector2 GeciciScale = transform.localScale;
        if (rb.velocity.x>0)
        {
            GeciciScale.x = 10f;
        }
        else if (rb.velocity.x<0)
        {
            GeciciScale.x = -10f;
        }
        transform.localScale = GeciciScale;
    }
    private IEnumerator Dash() //dash donksiyonu
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
    void Attack1()
    {
        canAttack = false;
        swordCollider.SetActive(true);
        // K�l�� animasyonunu oynatabilirsiniz.
        StartCoroutine(ResetAttack());
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordCollider.SetActive(false);
       // chainCollider.SetActive(false);
        // Sald�r� animasyonlar�n� sonland�rabilirsiniz.

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (swordCollider.activeSelf && other.CompareTag("enemy"))
        {
            Debug.Log("De�di");
            // K�l�� aktifken ve d��manla temas durumunda burada hasar verme veya di�er i�lemleri ger�ekle�tirin.
            // �rnek olarak, other.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }
}