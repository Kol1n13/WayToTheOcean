using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tramp : MonoBehaviour
{
    private Animator anime;
    [SerializeField] private AudioSource jumpSound;
    
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            jumpSound.Play();
            anime.SetTrigger("trigger");
    }
}
