using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    private Animator anime;
    private Rigidbody2D rjbody;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private AudioSource deathSound;

    public static bool TraplineActive;

    void Start()
    {
        anime = GetComponent<Animator>(); 
        rjbody = GetComponent<Rigidbody2D>();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
            Die();
        if (collision.gameObject.CompareTag("Tramp"))
        {
            TraplineActive = true;
            rjbody.velocity = new Vector3(rjbody.velocity.x, jumpForce);
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Firetrap") && Firetrap.active || collision.gameObject.CompareTag("Fan"))
        {
            Die();
        }
    }
    protected void Die()
    {
        deathSound.Play();
        rjbody.bodyType = RigidbodyType2D.Static;
        anime.SetTrigger("Death");
    }

    protected void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
