using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterController : MonoBehaviour
{
    soundManager SoundManagerScript;

   [SerializeField] public float moveSpeed = 5f;
   [SerializeField] public float jumpForce = 10f;
    public Transform groundCheck;
    public string groundTag = "Ground";
    private bool canDoubleJump;
    // [SerializeField] private int extraJumps = 1;
    [SerializeField] private float dashingPower = 24f;
   [SerializeField] private float dashingTime = 0.2f;
   [SerializeField] private float dashingCoolDown = 1f;
    //saldýrý 1
   [SerializeField] private GameObject swordCollider;
   [SerializeField] private float attackCooldown = 1.0f;
   [SerializeField] private float firerate;
    float nextfire;
    public int attackDamage = 10;
    //zýplama
    private int remainingJumps;
    private bool isGrounded;
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
    [SerializeField] public static int playerScore = 0;
    Animator animator;
    //ses
    private AudioSource audioSource; 
    public AudioClip[] FootSteps;
    private bool isMoving = false;
    //special attack
    [SerializeField] private GameObject SpecialAttackSword;
    private bool hasItem = false;
    private bool isInSpecialAttackMode = false;
    private float specialAttackDuration = 20f;
    private float timeSinceSpecialAttack;
    private bool canUseSpecialAttack = true;
    [SerializeField] private int specialAttackDamage = 20;
    public static Transform playerPosition;
    [SerializeField] private Light2D enemyLight2D;
    private Color originalColor;
    private float lastETime;
    private float cooldownDuration = 3f;
    private float coolDownTime = .7f;
    private float mouseButton;
    public AnimationClip[] animations;
    


    private void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        isGrounded = false;
        canDoubleJump = false;      
        animator = GetComponent<Animator>();
        SoundManagerScript = GameObject.Find("SoundManager").GetComponent<soundManager>();
        audioSource = GetComponent<AudioSource>();
        playerPosition = transform;
        swordCollider.SetActive(false);
        SpecialAttackSword.SetActive(false);
        originalColor = enemyLight2D.color;
    }
    private void Update()
    {
        
        if (canAttack && Input.GetMouseButtonDown(0) && Button0())//kýlýç vurma
        {
          // animator.SetTrigger("attack1");
            Attack1();
            PlayRandomAnimation();
            mouseButton = Time.time;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, 1 << LayerMask.NameToLayer(groundTag));
        if (isGrounded)
        {
            canDoubleJump = true;
            
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) //ZIPLAMA
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
        void Jump()  //ZIPLAMA
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("JumpAnim");
            SoundManagerScript.Jump_();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) //DASH
        {
            animator.SetTrigger("Dash");
            StartCoroutine(Dash());
            SoundManagerScript.Dash_();
        }
        if (Input.GetKeyDown(KeyCode.E) && playerScore >= 20 && CanPressE()) //OK
        {
            animator.SetTrigger("attack2");
            SoundManagerScript.Bow_();
            Invoke("Attack2", 1f);
            lastETime = Time.time;

        }
        if (Input.GetKeyDown(KeyCode.F)) // Special atak
        {
            // f basýlýysa ve item varsa baþlt
            if (hasItem && !isInSpecialAttackMode)
            {
                SoundManagerScript.Attack1_();
                StartCoroutine(StartSpecialAttack());
            }
            else if (isInSpecialAttackMode)
            {
                SoundManagerScript.Attack1_();
                animator.SetTrigger("SpecialAttack");
                // f tuþuna basýldýðýnda çaðýr
                SpecialAttack();
                StartCoroutine(StartSpecialAttack());
            }
        }
    }
    private void FixedUpdate()
    {
        flipface();
        if (isDashing)
        {
            return;
        }

        // Karakter hareketi
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);


        if (horizontalInput != 0)
        {
            if (isGrounded)
            {
                if (!audioSource.isPlaying)
                {
                    AudioClip randomyurumesesi = FootSteps[Random.Range(0, FootSteps.Length)];
                    audioSource.PlayOneShot(randomyurumesesi);
                    audioSource.pitch = Random.Range(0.8f, 1.2f);
                    
                }
            }
            animator.SetBool("RunAnim", true);
            isMoving = true;
           
        }
        else
        {
            animator.SetBool("RunAnim", false);
            if (isMoving)
            {
                audioSource.Stop();
                isMoving = false;
            }

        }
        playerPosition = transform;

       

    }

    void flipface()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 newScale = transform.localScale;

        if (horizontalInput > 0)
        {
            newScale.x = Mathf.Abs(newScale.x); // Pozitif ölçek (saða bakýyor)
        }
        else if (horizontalInput < 0)
        {
            newScale.x = -Mathf.Abs(newScale.x); // Negatif ölçek (sola bakýyor)
        }

        transform.localScale = newScale;
    }
    bool CanPressE()
    {
        return Time.time - lastETime >= cooldownDuration;
    }
    bool Button0()
    {
        return Time.time - mouseButton >= coolDownTime;
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
               

            }
        }

    }
    void SpecialAttack()
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            if (canAttack)
            {
                canAttack = false;
                SpecialAttackSword.SetActive(true);
                StartCoroutine(ResetAttack());
                
            }
        }
    }
    IEnumerator StartSpecialAttack()
    {
        isInSpecialAttackMode = true;
        hasItem = false; 
        timeSinceSpecialAttack = 0f;

       
        while (timeSinceSpecialAttack < specialAttackDuration)
        {
            timeSinceSpecialAttack += Time.deltaTime;

          
            if (Input.GetKeyDown(KeyCode.F) && !isInSpecialAttackMode)
            {
                SpecialAttack();
                
            }

            yield return null;
        }

        
        isInSpecialAttackMode = false;

        
        yield return new WaitForSeconds(10f);
        canUseSpecialAttack = true;
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordCollider.SetActive(false);
        SpecialAttackSword.SetActive(false); 
    

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (swordCollider.activeSelf && other.CompareTag("enemy"))
        {

            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            StartCoroutine(FlashRed());
        }
        if (other.CompareTag("chem"))
        {
            hasItem = true;
        }
        if (SpecialAttackSword.activeSelf && other.CompareTag("enemy"))
        {
            StartCoroutine(FlashRed());
            other.GetComponent<EnemyHealth>().TakeDamage(specialAttackDamage);
        }
    }



    void Attack2() // Mermi atma
    {
        
        if (Time.time > nextFireAttack2)
        {
            nextFireAttack2 = Time.time + fireRateAttack2;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Mermiyi doðru yöne yerleþtir
            Vector2 bulletVelocity = new Vector2((transform.localScale.x > 0) ? BulletSpeed : -BulletSpeed, 0f);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;

            // Mermiyi saða doðru atan karakterse, mermiyi de saða çevir
            if (transform.localScale.x > 0)
            {
                bullet.transform.localScale = new Vector3(7, 6, 1);
            }
            else // Mermiyi sola doðru atan karakterse, mermiyi sola çevir
            {
                bullet.transform.localScale = new Vector3(-7, 6, 1);
            }
        }
    }
    IEnumerator FlashRed()
    {
        enemyLight2D.color = Color.red; 

      
        yield return new WaitForSeconds(0.5f); 

        enemyLight2D.color = originalColor;
    }
    
    void PlayRandomAnimation()
    {

        if (animations == null || animations.Length == 0 || animator == null)
        {
            return;
        }
        int randomIndex = Random.Range(0, animations.Length);
        AnimationClip selectedAnimation = animations[randomIndex];
        animator.Play(selectedAnimation.name);
        Debug.Log("Played Animation: " + selectedAnimation.name);

        if (selectedAnimation.name == "attack1")
        {
            SoundManagerScript.Attack1_();
        }
        else if (selectedAnimation.name == "attack3")
        {
            SoundManagerScript.Attack1_();
        }
    }

}



