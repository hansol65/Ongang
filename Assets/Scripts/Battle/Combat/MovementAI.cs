using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    private int defaultDirection;
    private float directionChangeCooldown;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private LayerMask groundLayer;
    private bool isActive = true;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");

        ChangeDirection();
    }

    private void Update()
    {
        if (isActive)
        {
            DefaultMovement();
        }
    }

    private void DefaultMovement()
    {
        // Raycast
        if (IsGrounded(defaultDirection))
        {
            rigidBody.velocity = new Vector2(defaultDirection * moveSpeed, rigidBody.velocity.y);

            if (defaultDirection < 0)
                spriteRenderer.flipX = true;
            else if (defaultDirection > 0)
                spriteRenderer.flipX = false;
        }
        else
        {
            ChangeDirection();
        }

        directionChangeCooldown -= Time.deltaTime;
        if (directionChangeCooldown <= 0)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        defaultDirection = Random.Range(-1, 2);
        directionChangeCooldown = Random.Range(2f, 5f);
    }

    private bool IsGrounded(float direction)
    {
        // Raycast
        Vector2 origin = new Vector2(transform.position.x + direction * 0.5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(origin, Vector2.down * 1f, Color.red); // Raycast
        return hit.collider != null;
    }

    // MovementAI
    public void StopMovement()
    {
        isActive = false;
        rigidBody.velocity = Vector2.zero;
    }

    // MovementAI
    public void ResumeMovement()
    {
        isActive = true;
        ChangeDirection();
    }
}
