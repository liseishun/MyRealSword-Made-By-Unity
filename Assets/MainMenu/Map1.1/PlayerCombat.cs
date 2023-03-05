using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float knockbackForce = 10f;
    public float knockbackTime = 0.5f;
    float nextAttackTime = 0f;

    public float attackRate = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                StartCoroutine(KnockbackCo(enemyRb));
            }
        }
    }
    private IEnumerator KnockbackCo(Rigidbody2D enemyRb)
    {
        if (enemyRb != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            enemyRb.velocity = Vector2.zero;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, attackRange, 0));
    }

}

