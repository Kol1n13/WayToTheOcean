using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool isPlayerDead = false;

    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWithGun;

    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource waterSound;

    [SerializeField] private Slider deathSlider;

    public static bool TraplineActive;

    [SerializeField] private float fallThreshold = -20f;
    [SerializeField] private float deathTimerInterval = 10f;

    private float deathTimer = 0f;
    private bool isFalling = false;
    public static bool isOnFan = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        deathTimer = deathTimerInterval;
        deathSlider.maxValue = deathTimerInterval;

        if (Checkpoint.isCheckpointCollect)
        {
            player.transform.position = Checkpoint.checkpointPosition;
            player.SetActive(false);
            playerWithGun.SetActive(true);
            playerWithGun.transform.position = player.transform.position;
            ItemCollecter.isGunCollected = true;
        }
    }

    void Update()
    {
        if (rigidbody.velocity.y < fallThreshold && !isFalling && !isPlayerDead)
        {
            isFalling = true;
            StartCoroutine(FallCheckCoroutine());
        }

        if (!isPlayerDead && deathTimer > 0f)
        {
            deathTimer -= Time.deltaTime;
            deathSlider.value = deathTimer;
            if (deathTimer <= 0f)
                Die();
        }
    }

    IEnumerator FallCheckCoroutine()
    {
        yield return new WaitForSeconds(1.0f);

        if (isFalling && rigidbody.velocity.y < fallThreshold && !isPlayerDead)
            Die();

        isFalling = false;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
            Die();
        if (collision.gameObject.CompareTag("Tramp"))
        {
            TraplineActive = true;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce);
        }
        if (collision.gameObject.CompareTag("Fan"))
            isOnFan = true;
        else
            isOnFan = false;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
            waterSound.Play();
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Firetrap") && Firetrap.active || collision.gameObject.CompareTag("Fan"))
            Die();

        if (collision.gameObject.CompareTag("Water"))
            deathTimer = deathTimerInterval;
    }

    protected void Die()
    {
        isPlayerDead = true;
        if (ItemCollecter.isGunCollected)
            player.transform.position = playerWithGun.transform.position;
        ItemCollecter.isGunCollected = false;
        deathSound.Play();
        rigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
        deathTimer = deathTimerInterval;
        StartCoroutine(DelayedRestartLevel());
    }

    IEnumerator DelayedRestartLevel()
    {
        yield return new WaitForSeconds(1.0f);
        RestartLevel();
    }

    protected void RestartLevel()
    {
        isPlayerDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
