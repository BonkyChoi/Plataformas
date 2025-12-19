using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    private Rigidbody2D rb;
    private int score;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            score += 100;
            Destroy(gameObject);
        }
    }

    // private IEnumerator KnockBack(Vector3 knockDirection)
    // {
    //     rb.linearVelocity = Vector3.zero;
    //     rb.bodyType = RigidbodyType2D.Dynamic;
    //     rb.AddForce(knockDirection.normalized * 10f, ForceMode2D.Impulse);
    //     yield return new WaitForSeconds(0.2f);
    //     rb.bodyType = RigidbodyType2D.Kinematic;
    // }

    public void TakeDamage(GameObject dealer, float damage)
    {
        Vector3 knockDirection = transform.position - dealer.transform.position;
        
        // StartCoroutine(KnockBack(knockDirection));
        // health -= damage;
        // if (health <= 0)
        // {
        //     Destroy(gameObject);
        // }
    }

    
}
