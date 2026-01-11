using System;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private Animator anim;
    private float timer;
    [SerializeField] float cooldown = 1;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask whatIsDamageable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer >=cooldown)
        {
            anim.SetTrigger("Attack");
            timer = 0f;
        }
    }
    
    private void AttackHit()
    {
        Collider2D result = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsDamageable);

        if (result != null)
        {
         if (result.TryGetComponent<IDamagable>(out IDamagable damagable))
         { 
            damagable.TakeDamage(gameObject, 20f); 
         }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
