using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxHealth;
    [SerializeField]private float currentHealth;
    private Bullet bullet;

    private void Start()
    {
        maxHealth = 1000f;
        currentHealth = maxHealth;    
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            //game over
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            bullet = collision.GetComponent<Bullet>();
            GetDamage(bullet.damage);
            Destroy(collision.gameObject);
        }
    }

    public void GetDamage(float damage)
    {
        currentHealth -= damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
