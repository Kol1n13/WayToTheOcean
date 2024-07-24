using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float timeToLive = 7f;

    public GameObject impactEffect;
    private float spawnTime;


    void Start()
    {
        rb.velocity = transform.right * speed;
        spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - spawnTime > timeToLive)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject); 
            Destroy(Instantiate(impactEffect, transform.position, transform.rotation));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            return;
        var impact = Instantiate(impactEffect, transform.position, transform.rotation);
        Animation animation;
        if (impact.TryGetComponent<Animation>(out animation))
            Destroy(impact, animation.clip.length);
        else
            Destroy(impact, 0.25f);
        Destroy(gameObject);
    }
}
