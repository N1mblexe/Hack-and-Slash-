using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static GameObject player;
    public static GameObject playerBody;
    public static PlayerController playerController;

    public static bool ISGROUNDED;
    public static bool ISWALKING;

    private void Start()
    {
        try
        {
            playerBody = GameObject.Find("playerBody");
            player = GameObject.Find("player");
        }
        catch
        {
            Debug.LogError("Player body or player not found!!!");
        }

        playerController = playerBody.GetComponent<PlayerController>();
    }

    private void Update()
    {
        ISGROUNDED = playerController.isGrounded;
        ISWALKING = playerController.isWalking;
    }

}
