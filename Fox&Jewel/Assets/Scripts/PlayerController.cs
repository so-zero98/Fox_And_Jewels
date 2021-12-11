using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StartGame startGame;
    public GameManager manager;
    public SoundManager soundManager;
    ItemController itemController;
    public float moveSpeed;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;

    public Animator animator;

    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;

    public AudioClip playerDamagedSound;
    public AudioClip jumpSound;
    public AudioClip getGemSound;
    public AudioClip getCherrySound;

    public bool isLadder;

    float mx;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (!manager.isGameOver && startGame.isGameStart)
        {
            mx = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded()) // 땅에 있고 스페이스 눌렀을 때 점프
            {
                soundManager.EffectSoundPlay("Jump", jumpSound);
                Jump();
            }

            if (Mathf.Abs(mx) > 0.05f)  // 이동키 눌렀을 때 이동
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            if (mx > 0f)    // 이동 방향에 맞게 flip
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (mx < 0f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (IsJumping() && rb.velocity.y >= 0) { }
            else
            {
                animator.SetBool("isGrounded", IsGrounded());
                animator.SetBool("isJumping", false);
            }

            if (isLadder)
            {
                float ver = Input.GetAxisRaw("Vertical");
                if ((ver > 0 && IsGrounded()) || (ver > 0 && !IsGrounded()) || (ver < 0 && !IsGrounded()) || (ver == 0 && !IsGrounded()))
                {
                    animator.SetBool("isGrounded", false);
                    animator.SetBool("isClimbing", true);
                    animator.speed = Mathf.Abs(ver);
                    rb.gravityScale = 0;
                    Vector2 upDown = new Vector2(rb.velocity.x, ver * moveSpeed);
                    rb.velocity = upDown;
                }
                else if ((ver == 0 && IsGrounded()) || (ver < 0 && IsGrounded()))
                {
                    animator.SetBool("isGrounded", true);
                    animator.SetBool("isClimbing", false);
                }
            }
            else
            {
                animator.SetBool("isClimbing", false);
                rb.gravityScale = 4f;
                animator.speed = 1f;
                animator.SetBool("isGrounded", IsGrounded());
            }
        }
    }

    private void FixedUpdate()
    {
        if (spriteRenderer.color.a == 1)    // spriteRenderer 알파 값이 1일 때 -> 피격 시 인식 안되게 하기 위해
        {
            Vector2 movement = new Vector2(mx * moveSpeed, rb.velocity.y);

            rb.velocity = movement;
        }
    }

    void Jump()
    {
        IsJumping();
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    bool IsJumping()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isGrounded", false);
        return true;
    }

    public bool IsGrounded()    // 지면에 닿았는지 체크
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.35f, groundLayers);

        if (groundCheck != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))   // 적에 충돌했을 때
        {
            if ((rb.velocity.y <= 0) && (feet.position.y > collision.transform.position.y)) // 아래 속도에 적 위치보다 위에 있을 때 적 밟기
            {
                collision.gameObject.layer = 14;
                OnAttack(collision.transform);
            }
            else
            {
                OnDamaged(collision.transform.position);    // 그게 아니면 피격 판정
            }
        }

        else if (collision.gameObject.CompareTag("Spike"))  // 스파이크 일 때
            OnDamaged(collision.transform.position);
    }

    void OnAttack(Transform enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        int dirY = transform.position.y - enemy.position.y > 0 ? 1 : -1;    // 위로 반동

        rb.AddForce(new Vector2(dirY, 1) * 9, ForceMode2D.Impulse);

        enemyController.OnDie();    // 적 죽음

    }

    void OnDamaged(Vector2 targetPos)   // 피격당함, 무적상태
    {
        soundManager.EffectSoundPlay("PlayerDamaged", playerDamagedSound);

        manager.PlayerDamaged();

        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.7f);

        int dirX = transform.position.x - targetPos.x > 0 ? 1 : -1;

        rb.AddForce(new Vector2(dirX, 1) * 9, ForceMode2D.Impulse);

        animator.SetTrigger("isDamaged");

        if (!manager.isGameOver)    // 죽고나서 물리적용 무시하기 위한 조건
            Invoke("OffDamaged", 0.5f);
    }

    void OffDamaged()   // 피격 후 무적판정 해제
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 8;
        Invoke("VelocityReset", 0.4f);
    }

    void VelocityReset()    // 피격 후 뒤로 밀렸을 때 미끄러지는 현상 수정하기 위해
    {
        if (IsGrounded())
            rb.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item")) // 아이템 획득
        {
            itemController = collision.GetComponent<ItemController>();
            itemController.GetItem();

            bool isGem = collision.gameObject.name.Contains("Gem");
            bool isCherry = collision.gameObject.name.Contains("Cherry");

            if (isGem)
            {
                soundManager.EffectSoundPlay("GetGem", getGemSound);
                manager.gem += 1;
            }
            if (isCherry)
            {
                soundManager.EffectSoundPlay("GetCherry", getCherrySound);
                if (manager.life < 3)
                    manager.GetLife();
            }
        }

        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}
