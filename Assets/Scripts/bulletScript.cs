using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Move();
        DestroyAfterDelay(4f);
    }

    private void Move()
    {
        rb.velocity = transform.right * speed;
    }

    private void DestroyAfterDelay(float delay)
    {
        Destroy(gameObject, delay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cannon"))
        {
            return;
        }

        Destroy(gameObject);
    }
}