using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerShooting : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform rotationPoint;

    private float horizontalShoot;
    private float verticalShoot;
    private Vector3 shootingdir;

    [SerializeField] private float shootingRate = 2f;
    private float shootingCountdown;

    [SerializeField][Header("Bomb")]                     
    private float bomb;
    private int bombNumber;
    [SerializeField]private float bombRadius;
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private Collider2D[] results;

    [SerializeField] private float bombRate = .25f;
    private float bombCountdown;
    [SerializeField]private GameObject bombEffect;
    private Light2D bombLight;
    [SerializeField] private float updateLight = 20f;
    private EnemyShooting enemyShooting;
    private void Start()
    {
        bombNumber = 3;
        enemyShooting = FindObjectOfType<EnemyShooting>();
    }

    private void Update()
    {
        horizontalShoot = Input.GetAxis("HorizontalShoot");
        verticalShoot = Input.GetAxis("VerticalShoot");

        shootingdir = new Vector3(horizontalShoot,verticalShoot,0);
        if (shootingdir.sqrMagnitude != 0)
        {
            rotationPoint.transform.right = shootingdir.normalized;
            if (shootingCountdown <= 0)
            {
                Shoot();
                shootingCountdown = 1f / shootingRate;
            }
        }
        shootingCountdown -= Time.deltaTime;

        bomb = Input.GetAxis("Bomb");

        if (bomb > .1f)
        {
            if (bombNumber > 0 && bombCountdown <= 0)
            {
                bombNumber--;
                Bomb();
                bombCountdown = 1f / bombRate;                     
            }
        }
        bombCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (Gamemanager.instance.gameState == GameState.BOSSFIGHTPHASE1)
        {
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }

    private void Bomb()
    {
        if (Gamemanager.instance.gameState == GameState.BOSSFIGHTPHASE1)
        {
            if (enemyShooting != null)
            {
                enemyShooting.enabled = false;
            }
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, bombRadius, layerMask);
            GameObject BombEffectIns = Instantiate(bombEffect, transform.position, transform.rotation);
            bombLight = BombEffectIns.GetComponentInChildren<Light2D>();
            StartCoroutine(DimLight());
            Destroy(BombEffectIns, 2f);
            foreach (var collider in hitColliders)
            {
                Destroy(collider.gameObject);
            }
        }
       
    }

    IEnumerator DimLight()
    {
        float elapsed = 0f;

        while(elapsed < updateLight) 
        {
            elapsed += Time.deltaTime;
            bombLight.intensity -= Time.deltaTime;
            yield return null;
        }
        if (enemyShooting != null)
        {
            enemyShooting.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
