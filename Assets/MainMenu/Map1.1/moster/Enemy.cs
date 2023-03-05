using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    public Transform player;
    public float detectionRadius = 10f;
    public float followSpeed = 2f;
    private SpriteRenderer spriteRenderer;
    private Vector3 previousPosition;
    public float attackRadius;

    public int attackDamage = 10;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    [SerializeField] private AudioSource EnemydeadSoundEffect;
    [SerializeField] private AudioSource EnemyhitSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        previousPosition = transform.position;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EnemyhitSoundEffect.Play();
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            EnemydeadSoundEffect.Play();
            Die();

        }

    }

    void Die()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died");
            animator.SetBool("IsDead", true);
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }




    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            

            player.GetComponent<PlayerLife>().TakeDamage(attackDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }



    void Update()
    {
        if (currentHealth > 0)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius)
            {
                if (distanceToPlayer > attackRadius)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
                }
                else
                {
                    Attack();
                    transform.position = transform.position;
                }
                if (transform.position.x < previousPosition.x)
                {
                    spriteRenderer.flipX = false;
                }
                else if (transform.position.x > previousPosition.x)
                {
                    spriteRenderer.flipX = true;
                }
                previousPosition = transform.position;
            }
        }
    }
}