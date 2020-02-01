using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform playerPosition;
    [SerializeField] private Rigidbody2D EnemyRb;
    [SerializeField] private float speed;

    [SerializeField]private int movedirection;
    private float timer;
    [SerializeField] private float timeBetweenChange = .5f;
    // Update is called once per frame
    void Update()
    {
        if (playerPosition != null)
        {
            if (timer <= 0)
            {
                movedirection = Random.Range(1, -2);
                if (movedirection != 0)
                {
                    timer = timeBetweenChange;
                }
            }
            timer -= Time.deltaTime;

            transform.right = playerPosition.position - transform.position;

            float dist = Vector3.Distance(playerPosition.position, transform.position);
            if (dist > 6)
            {
                EnemyRb.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
            }
            else if (dist < 4)
            {
                EnemyRb.MovePosition(transform.position + -transform.right * speed * Time.deltaTime);
            }
            else
            {
                EnemyRb.MovePosition(transform.position + transform.up * movedirection * speed * Time.deltaTime);
            }
        }
        
    }
}
