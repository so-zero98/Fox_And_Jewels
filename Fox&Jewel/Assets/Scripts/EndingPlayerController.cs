using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlayerController : MonoBehaviour
{
    public Animator animator;
    public Transform EndingUI;
    public Rigidbody2D rb;
    public float moveSpeed;
    bool trigger;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerIdle();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
            Vector2 movement = new Vector2(moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetBool("Run", false);
        }
    }

    void PlayerIdle()
    {
        animator.Play("Player_Idle");
        Invoke("OnRun", 1.5f);
    }

    void OnRun()
    {
        trigger = true;
    }

    void OnEnding()
    {
        EndingUI.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("House"))
        {
            trigger = false;
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
            Invoke("OnEnding", 1f);
        }
    }
}
