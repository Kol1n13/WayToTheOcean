using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpIfIdle : MonoBehaviour
{
    public float idleTimeThreshold = 3f; // Время, после которого объект считается неподвижным
    public float jumpForce = 10f; // Сила подпрыгивания
    public float jumpCooldown = 2f; // Время задержки между подпрыгиваниями
    private Rigidbody2D rb;
    private float timeSinceLastJump;

    private Vector2 initialPosition;
    private bool isIdle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        timeSinceLastJump = 0f;
        isIdle = false;
    }

    private void Update()
    {
        if (IsIdle())
        {
            Jump();
        }
    }

    private bool IsIdle()
    {
        if (Mathf.Abs(transform.position.x - initialPosition.x) <= 0.01f)
        {
            if (!isIdle)
            {
                timeSinceLastJump = 0f;
                isIdle = true;
            }
            else
            {
                timeSinceLastJump += Time.deltaTime;
                if (timeSinceLastJump >= idleTimeThreshold)
                {
                    return true;
                }
            }
        }
        else
        {
            isIdle = false;
        }
        return false;
    }

    private void Jump()
    {
        if (Time.time > jumpCooldown + timeSinceLastJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }
    }
}
