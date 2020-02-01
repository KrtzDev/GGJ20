using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float maxHealth;
    [SerializeField]private float currentHealth;
    private Bullet bullet;

    [SerializeField] private Image HealthBar;

    private void Start()
    {
        maxHealth = 100f;
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
        HealthBar.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
