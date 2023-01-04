using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "player")
        {
            player.GetComponent<PlayerController>().isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<PlayerController>().isGrounded = false;
        player.GetComponent<PlayerController>().doubleJump = false;
    }
}
