using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Update()
    {
        transform.position = player.transform.position;
    }
}
