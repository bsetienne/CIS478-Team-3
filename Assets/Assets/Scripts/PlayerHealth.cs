using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 150;
    public int health;
    public Animator animator;
    private Rigidbody2D rb;

    public HealthBar healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.SetHealth(health);
        
        if(health <=0)
        {
            Death();
           
        }
    }

    public void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
