using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public int maxHealth = 100;
    int currentHealth;
    public Healthbar healthBar;
    [SerializeField] private AudioSource deadSoundEffect;
    [SerializeField] private AudioSource hitSoundEffect;


    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        hitSoundEffect.Play();
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            deadSoundEffect.Play();
            anim.SetBool("die1", true);
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.enabled = false;
            
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            deadSoundEffect.Play();
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {

        if (currentHealth <= 0)
        {
            this.enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            GetComponent<PlayerCombat>().enabled = false;
            if (rb.bodyType != RigidbodyType2D.Static)

                anim.SetTrigger("death");

            


        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
    }
}