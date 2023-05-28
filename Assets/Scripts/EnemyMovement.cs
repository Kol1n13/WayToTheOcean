using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;

    private SpriteRenderer sprite;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex += 1;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        Vector2 direction = waypoints[currentWaypointIndex].transform.position - transform.position;

        if (direction.x > 0f)
        {
            sprite.flipX = true;
        }
        else if (direction.x < 0f)
        {
            sprite.flipX = false;
        }
    }
}