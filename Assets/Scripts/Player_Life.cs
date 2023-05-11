using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    private Animator anime;
    private Rigidbody2D rjbody;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWithGun;

    [SerializeField] private AudioSource deathSound;

    public static bool TraplineActive;

    [SerializeField] private float fallThreshold = -20f; // Пороговое значение скорости падения

    private bool isFalling = false;

    void Start()
    {
        anime = GetComponent<Animator>();
        rjbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rjbody.velocity.y < fallThreshold && !isFalling)
        {
            isFalling = true;
            StartCoroutine(FallCheckCoroutine());
        }
    }

    IEnumerator FallCheckCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // Подождать 1 секунду, чтобы убедиться, что игрок продолжает падать

        if (isFalling && rjbody.velocity.y < fallThreshold)
        {
            Die();
        }

        isFalling = false;
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
        if (ItemCollecter.isGunCollected)
            player.transform.position = playerWithGun.transform.position;
        ItemCollecter.isGunCollected = false;
        deathSound.Play();
        rjbody.bodyType = RigidbodyType2D.Static;
        anime.SetTrigger("Death");
    }

    protected void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}