using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] waypointToMovement;
    [SerializeField] private GameObject[] waypointToStartLvl;
    [SerializeField] private GameObject finish;

    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Slider healthSlider;

    [SerializeField] private float speed = 2f;
    private int currentWaypoint = 0;
    public static bool isBossFightStart = false;
    public static bool isBossFightEnd = false;
    public static int health;
    public static int maxHealth;

    public enum BossPhase
    {
        Move,
        Shoot,
        SummonHelpers
    }

    public static BossPhase currentPhase;
    public static bool isPhaseActive = false;

    private int monstersSpawned = 0; // Счетчик созданных монстров
    private const int maxMonsters = 4; // Максимальное количество монстров для создания

    private BossPhase previousPhase; // Предыдущая фаза

    private void Start()
    {
        ResetBossSettings();
        StartCoroutine(SpawnMonstersPeriodically(5f));
    }

    private void Update()
    {
        if (isBossFightStart)
        {
            healthSlider.value = (float)health / maxHealth;
        }


        if (isBossFightStart == false)
        {
            Movement(waypointToStartLvl);
            healthSlider.value = maxHealth;
        }
        else
        {
            if (!isPhaseActive)
            {
                StartNextPhase();
            }

            switch (currentPhase)
            {
                case BossPhase.Move:
                    Movement(waypointToMovement);
                    break;

                case BossPhase.Shoot:
                    break;

                case BossPhase.SummonHelpers:
                    // В этой фазе монстры спаунятся периодически, поэтому нет необходимости вызывать InstantiateMonsters()
                    break;
            }
        }

        if (isBossFightEnd)
        {
            finish.SetActive(true);
        }
    }

    private void StartNextPhase()
    {
        isPhaseActive = true;

        if (previousPhase == BossPhase.Move)
        {
            // Если предыдущая фаза была Move, выбираем следующую фазу исключая Move
            int randomPhaseIndex = Random.Range(1, 3); // 1: Shoot, 2: SummonHelpers
            currentPhase = (BossPhase)randomPhaseIndex;
        }
        else
        {
            currentPhase = (BossPhase)Random.Range(0, 3);
        }

        StartCoroutine(EndPhase());
    }

    private IEnumerator EndPhase()
    {
        if (currentPhase == BossPhase.SummonHelpers)
        {
            isPhaseActive = false;
        }
        else
        {
            yield return new WaitForSeconds(3f);
            isPhaseActive = false;
        }

    }

    private void Movement(GameObject[] waypoints)
    {
        if (Vector2.Distance(waypoints[currentWaypoint].transform.position, transform.position) < .1f)
        {
            currentWaypoint += 1;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * speed);

        if (isBossFightStart == false && currentWaypoint == waypointToStartLvl.Length - 1)
        {
            isBossFightStart = true;
        }
    }

    private void ResetBossSettings()
    {
        isBossFightStart = false;
        isPhaseActive = false;
        previousPhase = BossPhase.SummonHelpers; // Изначально предыдущая фаза установлена на SummonHelpers
    }

    private IEnumerator SpawnMonstersPeriodically(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (isBossFightStart && currentPhase != BossPhase.SummonHelpers)
            {
                SpawnMonsters();
            }
        }
    }

    private void SpawnMonsters()
    {
        if (monstersSpawned < maxMonsters)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                Instantiate(monsterPrefab, spawnPoints[i].position, Quaternion.identity);
                monstersSpawned++;
            }
        }
    }
}