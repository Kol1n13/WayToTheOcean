using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 40;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
            health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
