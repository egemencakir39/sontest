using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    //saldýrý 2
    private int remainingJumps; // tekrarlanabilir zýplama
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool canDash = true;
    private bool isDashing;
    private bool canAttack = true;
    [SerializeField] private TrailRenderer tr;
    // attack 2
    public GameObject bulletPrefab; // Mermi prefabýný atayabileceðiniz bir alan
    public Transform firePoint;
    public float BulletSpeed = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        remainingJumps = extraJumps;
    }
    private void Update()
    {

        if (canAttack && Input.GetMouseButtonDown(0))//kýlýç vurma
        {
            Attack1(); //attack 1 fonksiyonu çaðýrýr
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack2();
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
        if (rb.velocity.x > 0)
        {
            GeciciScale.x = Mathf.Abs(GeciciScale.x); // Pozitif ölçek (saða bakýyor)
        }
        else if (rb.velocity.x < 0)
        {
            GeciciScale.x = -Mathf.Abs(GeciciScale.x); // Negatif ölçek (sola bakýyor)
        }
        transform.localScale = GeciciScale;
    }
    private IEnumerator Dash() //dash fonksiyonu
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
    void Attack1() //sword mekaniði
    {
        canAttack = false;
        swordCollider.SetActive(true);
        StartCoroutine(ResetAttack());
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordCollider.SetActive(false);
        // chainCollider.SetActive(false);


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
    void Attack2() // Mermi atma
    {
        // Mermi örneðini oluþturun
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        if (transform.localScale.x > 0)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed, 0f); // Saða doðru atýþ
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-BulletSpeed, 0f); // Sola doðru atýþ
        }

    }

}
