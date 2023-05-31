using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] waypointToMovement;
    [SerializeField] private GameObject[] waypointToStartLvl;
    [SerializeField] private GameObject finish;

    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float speed = 2f;
    private int currentWaypoint = 0;
    public static bool isBossFightStart = false;
    public static bool isBossFightEnd = false;

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

    private void Start()
    {
        ResetBossSettings();
    }

    private void Update()
    {
        if (isBossFightStart == false)
        {
            Movement(waypointToStartLvl);
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
                    InstantiateMonsters();
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
        currentPhase = (BossPhase)Random.Range(0, 3);
        StartCoroutine(EndPhase());
    }

    private IEnumerator EndPhase()
    {   
        if (currentPhase == BossLogic.BossPhase.SummonHelpers)
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
    }

    private void InstantiateMonsters()
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