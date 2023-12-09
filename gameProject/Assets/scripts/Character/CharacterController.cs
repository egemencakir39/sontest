using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // hareket h�z�
    public float jumpForce = 10f; // z�plama g�c�
    public int extraJumps = 1; // �ift z�plama hakk�
    public float dashingPower = 24f; // dash g�c�
    public float dashingTime = 0.2f; // dash s�resi
    public float dashingCoolDown = 1f;
    //sald�r� 1
    public GameObject swordCollider;//k�l�� colldier
    public float attackCooldown = 1.0f;
    public float firerate;
    float nextfire;
    public int attackDamage = 10;
    //z�plama
    private int remainingJumps; // tekrarlanabilir z�plama
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool canDash = true;
    private bool isDashing;
    private bool canAttack = true;
    [SerializeField] private TrailRenderer tr;
    // attack 2
    public GameObject bulletPrefab; // Mermi prefab�n� atayabilece�iniz bir alan
    public Transform firePoint;
    public float BulletSpeed = 100f;
    public float fireRateAttack2;
    float nextFireAttack2;
    [SerializeField] public static int playerScore = 0;
    



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        remainingJumps = extraJumps;
    }
    private void Update()
    {

        if (canAttack && Input.GetMouseButtonDown(0))//k�l�� vurma
        {
            Attack1(); //attack 1 fonksiyonu �a��r�r
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
        if (Input.GetKeyDown(KeyCode.E) && playerScore >= 5)
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

    void flipface() // karakterin y�z�n� d�nd�rme
    {
        Vector2 GeciciScale = transform.localScale;
        if (rb.velocity.x > 0)
        {
            GeciciScale.x = Mathf.Abs(GeciciScale.x); // Pozitif �l�ek (sa�a bak�yor)
        }
        else if (rb.velocity.x < 0)
        {
            GeciciScale.x = -Mathf.Abs(GeciciScale.x); // Negatif �l�ek (sola bak�yor)
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
    void Attack1() //sword mekani�i
    {
        if(Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            if (canAttack)
            {
                canAttack = false;
                swordCollider.SetActive(true);
                StartCoroutine(ResetAttack());
                
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
            // K�l�� aktifken ve d��manla temas durumunda burada hasar verme veya di�er i�lemleri ger�ekle�tirin.
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
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed, 0f); // Sa�a do�ru at��
            }
            else
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-BulletSpeed, 0f); // Sola do�ru at��
            }
        }
        
        
    }

}
