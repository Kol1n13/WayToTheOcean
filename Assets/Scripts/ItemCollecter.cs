using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollecter : MonoBehaviour
{
    private int apples = 0;
    [SerializeField] private Text ApplesText;
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWithGun;

    public static bool isGunCollected;
    public static bool isGunCollectedForCheck;
    public static bool flag = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            collectSound.Play();
            Destroy(collision.gameObject);
            apples++;
            ApplesText.text = "Score: " + apples;
        }

        if (collision.gameObject.CompareTag("Gun"))
            GiveGunToPlayer(collision);
    }

    private void GiveGunToPlayer(Collider2D collision)
    {
        collectSound.Play();
        Destroy(collision.gameObject);
        isGunCollected = true;
        isGunCollectedForCheck = true;
        player.SetActive(false);
        playerWithGun.SetActive(true);
        playerWithGun.transform.position = player.transform.position;
    }

}
