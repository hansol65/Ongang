using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public Transform Target;    // �̵� ���
    public float moveSpeed = 2f;    // �̵� �ӵ�
    public float stopDistance = 1.5f;   // ���� �Ÿ�
    private int defaultDirection;  // ����Ʈ ����(-1: ��, 0: ������, 1: ��)
    private float directionChangeCooldown;  // ���� ���� ��Ÿ��
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody; // Rigidbody2D �߰�

    public LayerMask groundLayer; // �ٴ� üũ�� ���� ���̾�

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>(); // Rigidbody2D ��������
        ChangeDirection();
    }

    private void Update()
    {
        // Ÿ���� �������� �ʰų� ��Ȱ��ȭ�� ��� ����Ʈ �ൿ ����
        if (Target == null || !Target.gameObject.activeInHierarchy)
        {
            Target = null; // ��Ȱ��ȭ�� Ÿ���� �ʱ�ȭ
            DefaultMovement();
        }
        else
        {
            MoveTarget();
        }
    }

    private void MoveTarget()
    {
        // Target���� �Ÿ� ���
        float distance = Vector2.Distance(transform.position, Target.position);

        // ���� �Ÿ� �̻��� ��� Target�� ����
        if (distance > stopDistance)
        {
            Vector2 direction = (Target.position - transform.position).normalized;

            // ��������Ʈ ���� ��ȯ
            if (direction.x < 0)
                spriteRenderer.flipX = true; // �������� �̵�
            else if (direction.x > 0)
                spriteRenderer.flipX = false; // ���������� �̵�

            // Raycast�� �ٴ� üũ
            if (IsGrounded(direction.x))
            {
                rigidBody.velocity = new Vector2(direction.x * moveSpeed, rigidBody.velocity.y); // �ӵ� ����
            }
            else
            {
                ChangeDirection(); // �ٴ��� ������ ���� ��ȯ
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y); // ����
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
        directionChangeCooldown = Random.Range(2f, 5f); // ��� �ð� 2���� 5�ʻ���
    }

    private bool IsGrounded(float direction)
    {
        // Raycast�� �ٴ� üũ
        Vector2 origin = new Vector2(transform.position.x + direction * 0.5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(origin, Vector2.down * 1f, Color.red); // Raycast ����׿�
        return hit.collider != null;
    }
}