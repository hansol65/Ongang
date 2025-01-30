using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public float moveSpeed = 2f;    // �̵� �ӵ�
    private int defaultDirection;  // ����Ʈ ����(-1: ��, 0: ������, 1: ��)
    private float directionChangeCooldown;  // ���� ���� ��Ÿ��
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody; // Rigidbody2D �߰�

    private LayerMask groundLayer; // �ٴ� üũ�� ���� ���̾�
    private bool isActive = true;  // Ȱ��ȭ ���� ���� ����


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");

        ChangeDirection();
    }

    private void Update()
    {
        if (isActive) // Ȱ��ȭ ���¿����� ������ ó��
        {
            DefaultMovement();
        }
    }

    private void DefaultMovement()
    {
        // Raycast�� �ٴ� üũ
        if (IsGrounded(defaultDirection))
        {
            rigidBody.velocity = new Vector2(defaultDirection * moveSpeed, rigidBody.velocity.y); // �ӵ� ����

            // ��������Ʈ ���� ��ȯ
            if (defaultDirection < 0)
                spriteRenderer.flipX = true; // �������� �̵�
            else if (defaultDirection > 0)
                spriteRenderer.flipX = false; // ���������� �̵�
        }
        else
        {
            ChangeDirection(); // �ٴ��� ������ ���� ��ȯ
        }

        // ��Ÿ�� ������ ���ο� ���� ���� ����
        directionChangeCooldown -= Time.deltaTime;
        if (directionChangeCooldown <= 0)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        defaultDirection = Random.Range(-1, 2);    // �������� -1, 0, 1 ���� ����
        directionChangeCooldown = Random.Range(2f, 5f); // ��� �ð� 2���� 5�� ����
    }

    private bool IsGrounded(float direction)
    {
        // Raycast�� �ٴ� üũ
        Vector2 origin = new Vector2(transform.position.x + direction * 0.5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(origin, Vector2.down * 1f, Color.red); // Raycast ����׿�
        return hit.collider != null;
    }

    // MovementAI ��Ȱ��ȭ
    public void StopMovement()
    {
        isActive = false;
        rigidBody.velocity = Vector2.zero; // �̵� ����
    }

    // MovementAI Ȱ��ȭ
    public void ResumeMovement()
    {
        isActive = true;
        ChangeDirection(); // ���� ����
    }
}
