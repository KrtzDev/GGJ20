using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private Transform playerPosition;

    private Vector3 shootingdir;
    private float shootingCountdown;
    [SerializeField]private float shootingRate;

    private void Update()
    {
        shootingdir = playerPosition.position - transform.position;
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
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }
}
