using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private Animator PlayerAnim;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }
        if (PlayerAnim.GetBool("Die"))
        {
            this.enabled = false;
        }
    }
}
