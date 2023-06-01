using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public float jumpCooldown = 3f;

    private Rigidbody2D rb;
    private float timeSinceLastJump = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FollowPlayer();
        Jump();
    }

    private void FollowPlayer()
    {
        if (player.position.x < transform.position.x)
        {
            // ����� ��������� ����� �� �����, ��������� �����
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (player.position.x > transform.position.x)
        {
            // ����� ��������� ������ �� �����, ��������� ������
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (Time.time > jumpCooldown + timeSinceLastJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            timeSinceLastJump = Time.time;
        }
    }
}
