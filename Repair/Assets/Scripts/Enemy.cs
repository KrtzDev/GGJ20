using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float startHealth;
    private float maxHealth;
    [SerializeField] private float currentHealth;
    private Bullet bullet;

    [SerializeField] private Image HealthBar;

    private void Start()
    {
        startHealth = 0f;
        maxHealth = 300f;
        currentHealth = startHealth;
    }

    private void Update()
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = 0;

            NextPhase();
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
        HealthBar.fillAmount = currentHealth / maxHealth;
    }

    void NextPhase()
    {
        Gamemanager.instance.EndGame();
    }
}
