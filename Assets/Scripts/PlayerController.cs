using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    private void Awake()
    {
        //initializing
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 입력 처리 (좌우 이동)
        moveInput.x = Input.GetAxis("Horizontal");

        // 점프 입력
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // 점프 중 상태
        }
    }

    private void FixedUpdate()
    {
        // 물리 이동 처리
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았는지 확인
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

