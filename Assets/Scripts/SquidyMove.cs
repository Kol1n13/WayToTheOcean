using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidyMove : MonoBehaviour
{
    private Animator anime;

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minSpeed = 2f; // ����������� �������� ��������
    [SerializeField] private float maxSpeed = 10f; // ������������ �������� ��������
    private int currentWaypointIndex = 0;

    private enum MovementState { blink, left, right, topDown }

    private float horizontalMoveTime = 0f; // ����� �������� �� �����������
    private const float maxHorizontalMoveTime = 3f; // ������������ ����� �������� �� �����������


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

        // ����������� ��������� ��������
        MovementState state;
        if (direction.x > 0f)
        {
            state = MovementState.right;
            // ���������� ������� �������� �� �����������
            horizontalMoveTime += Time.deltaTime;
        }
        else if (direction.x < 0f)
        {
            state = MovementState.left;
            // ���������� ������� �������� �� �����������
            horizontalMoveTime += Time.deltaTime;
        }
        else
        {
            state = MovementState.blink;
            // ��������� ������� �������� �� �����������
            horizontalMoveTime = 0f;
        }

        // ����������� �������� ��������
        if (state == MovementState.left || state == MovementState.right)
        {
            // �������� �������� ������� �� ������� �������� �� �����������
            float speedFactor = Mathf.Clamp(horizontalMoveTime / maxHorizontalMoveTime, 0f, 1f);
            speed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);
        }
        else
        {
            speed = minSpeed;
        }

        // �������� �� �������� �� ���������
        if (Mathf.Abs(direction.y) > 0.1f)
        {
            state = MovementState.topDown;
        }

        // ��������� ��������
        anime.SetInteger("MovementState", (int)state);
    }
}