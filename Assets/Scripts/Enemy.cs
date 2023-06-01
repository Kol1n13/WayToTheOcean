using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 40;

    private Animator anime;
    [SerializeField] private AudioSource dieSound;
    private bool isPlaying = false;
    [SerializeField] private bool isObjectBoss;

    public void Start()
    {
        anime = GetComponent<Animator>();
        if (isObjectBoss)
        {
            BossLogic.maxHealth = health;
            BossLogic.health = health;
        }
    }

    public void Update()
    {
        if (isObjectBoss)
            BossLogic.health = health;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= damage;
            if (!isPlaying)
                PlayDieSound();
        }

        if (health <= 0)
            Die();
    }

    void Die()
    {
        anime.SetTrigger("Death");
        Destroy(gameObject, 0.2f);
        if (isObjectBoss)
            BossLogic.isBossFightEnd = true;
    }

    void PlayDieSound()
    {
        if (dieSound != null)
        {
            isPlaying = true;
            dieSound.Play();
            StartCoroutine(WaitForDieSound());
        }
    }

    IEnumerator WaitForDieSound()
    {
        yield return new WaitForSeconds(dieSound.clip.length);
        isPlaying = false;
    }
}
