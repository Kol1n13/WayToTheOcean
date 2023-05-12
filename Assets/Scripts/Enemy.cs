using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 40;
    
    private Animator anime;
    [SerializeField] private AudioSource dieSound;
    
    private void Start()
    {
        anime = GetComponent<Animator>();
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
            health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        dieSound.Play();
        anime.SetTrigger("Death");
        Destroy(gameObject, 0.1f);
    }
}
