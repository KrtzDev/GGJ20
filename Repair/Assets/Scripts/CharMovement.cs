using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{

    private Rigidbody2D charRb;
    private BoxCollider2D charColl;

    private float horizontal;
    private float vertical;
    private Vector3 movement;
    private Vector3 movementdir;

    [SerializeField] private float dashForce = 5f;
    private bool dashpossible = true;
    [SerializeField]private LayerMask layerMask;

    [SerializeField] private float movementSpeed = 5f;

    private void Start()
    {
        charRb = GetComponent<Rigidbody2D>();
        charColl = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movementdir = new Vector3(horizontal,vertical,0);
        movement = Vector3.ClampMagnitude(movementdir, 1f);

        if (Input.GetButtonDown("Dash") && dashpossible == true && CanDash())
        {
            transform.position += movementdir.normalized * dashForce;
            charColl.enabled = false;
            StartCoroutine(DashInvincibility());
            dashpossible = false;
            StartCoroutine(DashCooldown());
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        charRb.MovePosition(transform.position + movement * movementSpeed * Time.fixedDeltaTime);
        transform.right = movementdir.normalized;
    }

    private bool CanDash()
    {
        return Physics2D.Raycast(transform.position, movementdir.normalized, dashForce, layerMask).collider == null; ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position,movementdir.normalized * dashForce);
    }

    IEnumerator DashInvincibility()
    {
        yield return new WaitForSeconds(.1f);
        charColl.enabled = true;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(.3f);
        dashpossible = true;
    }
}
