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

    private float horizontalMoveTime = 0f; // ����� �������� �� �����������
    private const float maxHorizontalMoveTime = 3f; // ������������ ����� �������� �� �����������

    private enum MovementState { blink, left, right, topDown }

    private void Start() => anime = GetComponent<Animator>();

    private void Update()
    {
        UpdateWaypoint();
        MoveTowardsWaypoint();
        UpdateMovementState();
        UpdateSpeed();
        UpdateAnimation();
    }

    private void UpdateWaypoint()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private void MoveTowardsWaypoint() => transform.position = Vector2.MoveTowards(transform.position,
        waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

    private void UpdateMovementState()
    {
        Vector2 direction = waypoints[currentWaypointIndex].transform.position - transform.position;

        if (direction.x > 0f)
        {
            SetMovementState(MovementState.right);
            horizontalMoveTime += Time.deltaTime;
        }
        else if (direction.x < 0f)
        {
            SetMovementState(MovementState.left);
            horizontalMoveTime += Time.deltaTime;
        }
        else
        {
            SetMovementState(MovementState.blink);
            horizontalMoveTime = 0f;
        }
        if (Mathf.Abs(direction.y) > 0.1f)
            SetMovementState(MovementState.topDown);
    }

    private void UpdateSpeed()
    {
        if (IsMovingHorizontally())
        {
            float speedFactor = Mathf.Clamp(horizontalMoveTime / maxHorizontalMoveTime, 0f, 1f);
            speed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);
        }
        else
            speed = minSpeed;
    }

    private void UpdateAnimation() => anime.SetInteger("MovementState", (int)GetCurrentMovementState());

    private void SetMovementState(MovementState state) => anime.SetInteger("MovementState", (int)state);

    private MovementState GetCurrentMovementState() => (MovementState)anime.GetInteger("MovementState");

    private bool IsMovingHorizontally()
    {
        MovementState state = GetCurrentMovementState();
        return state == MovementState.left || state == MovementState.right;
    }
}
