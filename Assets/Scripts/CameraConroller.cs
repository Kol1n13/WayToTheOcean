using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerWithGun;


    void Update()
    {   
        if (ItemCollecter.isGunCollected == false)
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        else
            transform.position = new Vector3(playerWithGun.position.x, playerWithGun.position.y, transform.position.z);
    }
}
