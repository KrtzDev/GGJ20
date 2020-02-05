using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private float maxHealth;
    private float normalHealth;
    private float cheatHealth;
    [SerializeField]private float currentHealth;
    private Bullet bullet;

    [SerializeField] private GameObject DeathEffect;

    [SerializeField] private Image HealthBar;

    [SerializeField] private TMP_Text maxHealthText;
    [SerializeField] private Animator textAnim;
    [SerializeField] private Animator playerAnim;

    private void Start()
    {
        maxHealth = 100f;
        normalHealth = 100f;
        cheatHealth = 1000f;
        currentHealth = maxHealth;    
    }

    private void Update()
    {

        if (Input.GetButtonDown("Cheat"))
        {
            if (maxHealth == 100)
            {
                maxHealth = cheatHealth;
                currentHealth = cheatHealth;
                maxHealthText.text = ("1000");
                textAnim.SetTrigger("ShowHealth");
            }
            else if (maxHealth == 1000)
            {
                maxHealth = normalHealth;
                currentHealth = normalHealth;
                maxHealthText.text = ("100");
                textAnim.SetTrigger("ShowHealth");
            }
        }

        if (currentHealth <= 0)
        {
            currentHealth = 100;
            Gamemanager.instance.gameState = GameState.DEATH;
            playerAnim.SetBool("Die",true);
            StartCoroutine(Die());
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

    IEnumerator Die()
    {      
        yield return new WaitForSeconds(1.5f);
        Instantiate(DeathEffect,transform.position,transform.rotation);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject,3f);
        yield return new WaitForSeconds(1f);
        SceneManagerx.instance.ChangeSceneCredits();
    }
}
