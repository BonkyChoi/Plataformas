using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    
    [SerializeField] private float velocity = 6f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float knockBackStrength = 7f;
    [SerializeField] private float rayLength = 1.66f;
    [SerializeField] private LayerMask floor;
    [SerializeField] public int maxHP = 3;


    public int currentHP;
    private bool damaged;
    private bool striking;
    private bool onFloor;
    public bool Death { get; private set; } 

   
    private Rigidbody2D rb;
    private Animator animator;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHP = maxHP;
    }

    void Update()
    {
        if (Death)
        {
            Animacion();
            return;
        }

        if (!striking)
        {
            Movimiento();
            RevisarSuelo();

            if (onFloor && Input.GetKeyDown(KeyCode.Space) && !damaged)
                Saltar();
        }

        if (Input.GetKeyDown(KeyCode.J) && !striking && onFloor)
            Attacking();

        Animacion();
    }

   
    private void Movimiento()
    {
        if (damaged)
            return;

        float inputX = Input.GetAxis("Horizontal");
        float velocidadX = inputX * velocity;

        animator.SetFloat("Movement", Mathf.Abs(velocidadX));

        if (velocidadX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (velocidadX > 0)
            transform.localScale = new Vector3(1, 1, 1);

        rb.linearVelocity = new Vector2(velocidadX, rb.linearVelocity.y);
    }

    private void RevisarSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, floor);
        onFloor = hit.collider != null;
    }

    private void Saltar()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

   
    public void ReceivingDamage(Vector2 direccion, int AmountDamage)
    {
        if (damaged || Death)
            return;

        damaged = true;
        currentHP -= AmountDamage;

        if (currentHP <= 0)
        {
            Death = true;
            GameManager.instance.GameOver();
        }

        Vector2 knockback = new Vector2(direccion.x, 0);
        rb.AddForce(knockback * knockBackStrength, ForceMode2D.Impulse);

        StartCoroutine(StopDamage());
    }

    private IEnumerator StopDamage()
    {
        yield return new WaitForSeconds(0.4f);
        damaged = false;
    }

   
    public void Attacking()
    {
        striking = true;
    }

    public void NotAttacking()
    {
        striking = false;
    }


    private void Animacion()
    {
        animator.SetBool("OnFloor", onFloor);
        animator.SetBool("Damage", damaged);
        animator.SetBool("Attack", striking);
        animator.SetBool("Death", Death);
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * rayLength
        );
    }
}
