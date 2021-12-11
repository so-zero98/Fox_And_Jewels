using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SoundManager soundManager;
    public float enemySpeed;
    public float rayDistance;
    private bool moveRight;
    public Transform groundDetect;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    CircleCollider2D col;

    public AudioClip enemyDamagedSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDistance); // 레이로 지형 판정

        if (groundCheck.collider == false)  // 지형 크기에 따라 좌우 이동 및 flip
        {
            if (moveRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
    }

    public void OnDie()
    {
        soundManager.EffectSoundPlay("EnemyDamaged", enemyDamagedSound);
        animator.SetTrigger("isDamaged");   // 죽는 이펙트
        enemySpeed = 0;
        Destroy(gameObject, 0.5f);  // 0.5초 후 삭제
    }
}
