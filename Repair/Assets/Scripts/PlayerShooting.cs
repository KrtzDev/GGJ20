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
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }

}
