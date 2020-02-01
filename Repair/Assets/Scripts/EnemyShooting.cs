using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ShootingPoint[] shootingPoints;
    [SerializeField] private ShootingPointVariation[] shootingPointVariations;
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private Transform playerPosition;

    private Vector3 shootingdir;
    private float shootingCountdown;
    private float shootingCountdownOffset;
    [SerializeField]private float shootingRate;

    private void Start()
    {
        shootingPoints = FindObjectsOfType<ShootingPoint>();
        shootingPointVariations = FindObjectsOfType<ShootingPointVariation>();
        shootingCountdownOffset = (1f/shootingRate) * .5f;
    }

    private void FixedUpdate()
    {
        if (playerPosition != null)
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
                if (shootingCountdownOffset <= 0)
                {
                    ShootVariation();
                    shootingCountdownOffset = 1f / shootingRate;
                }
            }
            shootingCountdown -= Time.fixedDeltaTime;
            shootingCountdownOffset -= Time.fixedDeltaTime;
        }
    }

    private void Shoot()
    {
        foreach (var shootingPoint in shootingPoints)
        {
            Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        }     
    }

    private void ShootVariation()
    {
        foreach (var shootingPointVariation in shootingPointVariations)
        {
            Instantiate(bulletPrefab, shootingPointVariation.transform.position, shootingPointVariation.transform.rotation);
        }
    }
}
