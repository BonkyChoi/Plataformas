using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    [SerializeField] private int health = 3;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float knockbackForce = 7f;

    [SerializeField] private int damageToPlayer = 1;
    [SerializeField] private int damageTakenPerHit = 1;

    private Rigidbody2D rb;
    private bool isHurt;
    private bool isDead;
    private bool shouldMove;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead || player == null)
        {
            return;
        }

        Player playerScript = player.GetComponent<Player>();

        if (playerScript != null && playerScript.Death)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("InMovement", false);
            return;
        }

        if (isHurt)
        {
            return;
        }
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            MoveTowardPlayer();
        }
        if (distanceToPlayer < detectionRadius)
            shouldMove = true;
        else
            shouldMove = false;
        
    }
    private void FixedUpdate()
    {
        if (isHurt || isDead)
        {
            return;
        }

        if (!shouldMove)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
    }

    private void MoveTowardPlayer()
    {
        float directionX = player.position.x - transform.position.x;
        float moveDirection = 0f;

        if (directionX > 0)
            moveDirection = 1f;
        else if (directionX < 0)
            moveDirection = -1f;

        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        transform.localScale = new Vector3(moveDirection, 1, 1);

        animator.SetBool("InMovement", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
            return;

        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript != null)
            {
                Vector2 direction = (collision.transform.position - transform.position);
                direction.y = 0;
                direction = direction.normalized;

                playerScript.ReceivingDamage(direction, damageToPlayer);
            }
        }

        if (collision.CompareTag("Attack"))
        {
            float directionX = transform.position.x - collision.transform.position.x;

            if (directionX > 0)
            {
                directionX = 1f;
            }
            else
                directionX = -1f;
            directionX = MathF.Sign(directionX);

            Vector2 direction = new Vector2(directionX, 0f);

            TakeDamage(direction, damageTakenPerHit);
        }
    }

    public void TakeDamage(Vector2 direction, int amount)
    {
        if (isHurt || isDead)
            return;

        isHurt = true;
        health -= amount;

        if (health <= 0)
        {
            Die();
            return;
        }

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(HurtCooldown());
    }

    IEnumerator HurtCooldown()
    {
        yield return new WaitForSeconds(0.4f);
        isHurt = false;
        rb.linearVelocity = Vector2.zero;
    }

    private void Die()
    {
        isDead = true;
        isHurt = true;

        rb.linearVelocity = Vector2.zero;

        foreach (Collider2D col in GetComponents<Collider2D>())
            col.enabled = false;

        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float duration = state.length;

        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    
}
