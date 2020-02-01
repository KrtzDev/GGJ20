using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform rotationPoint;

    private float horizontalShoot;
    private float verticalShoot;
    private Vector3 shootingdir;

    [SerializeField] private float shootingRate = 2f;
    private float shootingCountdown;

    private float bomb;
    private int bombNumber;
    [SerializeField]private float bombRadius;
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private Collider2D[] results;

    [SerializeField] private float bombRate = .25f;
    private float bombCountdown;

    private void Start()
    {
        bombNumber = 3;
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
        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }

    private void Bomb()
    {
        Debug.Log("Bomb");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position,bombRadius,layerMask);
        foreach (var collider in hitColliders)
        {
            Destroy(collider.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
