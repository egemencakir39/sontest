using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int extraJumps = 1;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCoolDown = 1f;
    //saldýrý 1
    public GameObject swordCollider;
    public float attackCooldown = 1.0f;
    public float firerate;
    float nextfire;
    public int attackDamage = 10;
    //zýplama
    private int remainingJumps;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool canDash = true;
    private bool isDashing;
    private bool canAttack = true;
    [SerializeField] private TrailRenderer tr;
    // attack 2
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float BulletSpeed = 100f;
    public float fireRateAttack2;
    float nextFireAttack2;
    [SerializeField] public static int playerScore = 5;
    Animator animator;
    AudioSource audioSource;
    private bool isMoving = false;
    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        remainingJumps = extraJumps;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {

        if (canAttack && Input.GetMouseButtonDown(0))//kýlýç vurma
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
            animator.SetTrigger("JumpAnim");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            remainingJumps--;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) //dash tuþ kontrolü
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.E) && playerScore >= 5)
        {
            animator.SetTrigger("attack2");
            Invoke("Attack2", .7f);

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
        if (horizontalInput != 0)
        {
            animator.SetBool("RunAnim", true);
            if (!isMoving)
            {
                audioSource.Play();
                isMoving = true; // karakter hareket ettiði durumu iþaretle
            }
        }
        else
        {
            animator.SetBool("RunAnim", false);
            if (isMoving)
            {
                audioSource.Stop();
                isMoving = false; // karakter durduðu durumu iþaretle
            }
        }
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
        if (Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            if (canAttack)
            {
                canAttack = false;
                swordCollider.SetActive(true);
                StartCoroutine(ResetAttack());
                animator.SetTrigger("attack1");
            }
        }

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

            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

    }

    void Attack2() // Mermi atma
    {

        if (Time.time > nextFireAttack2)
        {
            nextFireAttack2 = Time.time + fireRateAttack2;
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

}