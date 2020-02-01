using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float startHealth;
    [SerializeField] private float currentHealth;
    private Bullet bullet;

    private void Start()
    {
        startHealth = 0f;
        currentHealth = startHealth;
    }

    private void Update()
    {
        if (currentHealth >= 100)
        {
            //Next Phase
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            bullet = collision.GetComponent<Bullet>();
            GetRepaired(bullet.damage);
            Destroy(collision.gameObject);
        }
    }

    public void GetRepaired(float damage)
    {
        currentHealth += damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
