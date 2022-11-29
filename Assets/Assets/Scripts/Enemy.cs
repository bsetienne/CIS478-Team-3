using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Transform target;
    public Transform borderCheck;
    public int enemyHP = 150;
    public Animator animator;
    int currentHealth;
    public Slider enemyHPBar;
    // Start is called before the first frame update
    void Start()
    {
        enemyHPBar.value = enemyHP;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = enemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }
    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        enemyHPBar.value = currentHealth;

        animator.SetTrigger("Damage");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        this.enabled = false;
    }
}
