using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 10;

    private Vector3 dir;

    private void Start()
    {
        dir = transform.right;
    }
    private void FixedUpdate()
    {
        transform.position += dir * projectileSpeed * Time.deltaTime;

        Destroy(gameObject,5f);
    }


}
