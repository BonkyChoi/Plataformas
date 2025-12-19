using UnityEngine;

public class Slime : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void TakeDamage(float damage)
    {
        // Vector3 knockback = dealer.transform.position - transform.position;
        // KnockBack(knockDirection);
        // health -= damage;
        // if (health <= 0)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void KnockBack(Vector3 knockDirection)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(knockDirection * 10, ForceMode2D.Impulse);
        
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void TakeDamage(GameObject dealer, float damage)
    {
        throw new System.NotImplementedException();
    }
}
