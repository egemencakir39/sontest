using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // hareket hýzý
    public float jumpForce = 10f; // zýplama gücü
    public int extraJumps = 1; // Çift zýplama hakký
    public float dashingPower = 24f; // dash gücü
    public float dashingTime = 0.2f; // dash süresi
    public float dashingCoolDown = 1f;
    //saldýrý
    public GameObject swordCollider;//kýlýç colldier
    public float attackCooldown = 1.0f;
    public int attackDamage = 10;
    private int remainingJumps; // tekrarlanabilir zýplama
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
        // zemin temas kontrolü
        isGrounded = Physics2D.IsTouchingLayers(col, LayerMask.GetMask("Ground"));

        // Zýplama
        if (isGrounded)
        {
            remainingJumps = extraJumps;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || remainingJumps > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            remainingJumps--;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) //dash tuþ kontrolü
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
     void flipface() // karakterin yüzünü döndürme
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
        // Kýlýç animasyonunu oynatabilirsiniz.
        StartCoroutine(ResetAttack());
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordCollider.SetActive(false);
       // chainCollider.SetActive(false);
        // Saldýrý animasyonlarýný sonlandýrabilirsiniz.

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (swordCollider.activeSelf && other.CompareTag("enemy"))
        {
            Debug.Log("Deðdi");
            // Kýlýç aktifken ve düþmanla temas durumunda burada hasar verme veya diðer iþlemleri gerçekleþtirin.
            // Örnek olarak, other.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }
}