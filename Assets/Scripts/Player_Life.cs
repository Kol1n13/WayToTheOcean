using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    private Animator anime;
    private Rigidbody2D rjbody;

    [SerializeField] private AudioSource deathSound;

    void Start()
    {
        anime = GetComponent<Animator>(); 
        rjbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
            Die();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Firetrap") && Firetrap.active)
        {
            Die();
        }
    }
    private void Die()
    {
        deathSound.Play();
        rjbody.bodyType = RigidbodyType2D.Static;
        anime.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
