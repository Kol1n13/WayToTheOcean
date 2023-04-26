using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidyMove : MonoBehaviour
{
    private Animator anime;

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minSpeed = 2f; // минимальная скорость движения
    [SerializeField] private float maxSpeed = 10f; // максимальная скорость движения
    private int currentWaypointIndex = 0;

    private enum MovementState { blink, left, right, topDown }

    private float horizontalMoveTime = 0f; // время движения по горизонтали
    private const float maxHorizontalMoveTime = 3f; // максимальное время движения по горизонтали


    private void Start()
    {
        anime = GetComponent<Animator>();
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

        // определение состояния движения
        MovementState state;
        if (direction.x > 0f)
        {
            state = MovementState.right;
            // увеличение времени движения по горизонтали
            horizontalMoveTime += Time.deltaTime;
        }
        else if (direction.x < 0f)
        {
            state = MovementState.left;
            // увеличение времени движения по горизонтали
            horizontalMoveTime += Time.deltaTime;
        }
        else
        {
            state = MovementState.blink;
            // обнуление времени движения по горизонтали
            horizontalMoveTime = 0f;
        }

        // определение скорости движения
        if (state == MovementState.left || state == MovementState.right)
        {
            // скорость движения зависит от времени движения по горизонтали
            float speedFactor = Mathf.Clamp(horizontalMoveTime / maxHorizontalMoveTime, 0f, 1f);
            speed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);
        }
        else
        {
            speed = minSpeed;
        }

        // проверка на движение по вертикали
        if (Mathf.Abs(direction.y) > 0.1f)
        {
            state = MovementState.topDown;
        }

        // установка анимации
        anime.SetInteger("MovementState", (int)state);
    }
}
