using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 4;
    public float jumpForce = 7;
    private float movementX;
    private new Rigidbody2D rigidbody;
    private Controls input;

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    private void Move(float axis)
    {
         movementX = axis * speed;
    }

    private void Jump()
    {
        rigidbody.velocity = new Vector2(movementX, jumpForce);
    }

    private void Awake()
    {
        input = new Controls();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        input.Player.Move.performed += context => Move(context.ReadValue<float>());        
        input.Player.Move.canceled += context => Move(0);
        input.Player.Jump.performed += context => Jump();
    }

    private void Update()
    {
        rigidbody.velocity = new Vector2(movementX, rigidbody.velocity.y);
    }
}
