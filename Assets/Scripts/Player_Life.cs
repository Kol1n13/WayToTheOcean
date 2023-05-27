using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player_Life : MonoBehaviour
{
    private Animator anime;
    private Rigidbody2D rjbody;
    private bool isPlayerDead = false;

    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWithGun;

    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource waterSound;

    [SerializeField] private Slider deathSlider;


    public static bool TraplineActive;

    [SerializeField] private float fallThreshold = -20f; // Пороговое значение скорости падения

    [SerializeField] private float deathTimerInterval = 10f; // Интервал смерти игрока

    private float deathTimer = 0f; // Таймер смерти игрока
    private bool isFalling = false;
    public static bool isOnFan = false;

    void Start()
    {
        anime = GetComponent<Animator>();
        rjbody = GetComponent<Rigidbody2D>();
        deathTimer = deathTimerInterval; // Инициализация таймера смерти
        deathSlider.maxValue = deathTimerInterval; // Установка максимального значения слайдера

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
        if (rjbody.velocity.y < fallThreshold && !isFalling && !isPlayerDead)
        {
            isFalling = true;
            StartCoroutine(FallCheckCoroutine());
        }

        // Обновление таймера смерти игрока
        if (!isPlayerDead && deathTimer > 0f)
        {
            deathTimer -= Time.deltaTime;
            deathSlider.value = deathTimer; // Обновление значения слайдера
            if (deathTimer <= 0f)
            {
                Die();
            }
        }
    }



    IEnumerator FallCheckCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // Подождать 1 секунду, чтобы убедиться, что игрок продолжает падать

        if (isFalling && rjbody.velocity.y < fallThreshold && !isPlayerDead)
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
        if (collision.gameObject.CompareTag("Fan"))
        {
            isOnFan = true;
        }
        else isOnFan = false;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            waterSound.Play();
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Firetrap") && Firetrap.active || collision.gameObject.CompareTag("Fan"))
        {
            Die();
        }

        // Сбросить таймер смерти при нахождении в воде
        if (collision.gameObject.CompareTag("Water"))
        {
            deathTimer = deathTimerInterval; // Сбросить таймер смерти
        }
    }

    protected void Die()
    {
        isPlayerDead = true;
        if (ItemCollecter.isGunCollected)
            player.transform.position = playerWithGun.transform.position;
        ItemCollecter.isGunCollected = false;
        deathSound.Play();
        rjbody.bodyType = RigidbodyType2D.Static;
        anime.SetTrigger("Death");
        deathTimer = deathTimerInterval; // Сбросить таймер смерти
        StartCoroutine(DelayedRestartLevel());
    }

    IEnumerator DelayedRestartLevel()
    {
        yield return new WaitForSeconds(1.0f); // Подождать 1 секунду

        RestartLevel(); // Вызвать метод RestartLevel()
    }

    protected void RestartLevel()
    {
        isPlayerDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}