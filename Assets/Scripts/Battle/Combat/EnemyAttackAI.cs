using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    public float moveSpeed = 2f; // �̵� �ӵ�
    private Rigidbody2D rigidBody;
    private Transform target; // Ÿ�� (Player)
    private bool isColliding = false; // �浹 ���� Ȯ��

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // Player ���̾ ã�� Ÿ�� ����
        FindEnemy();
    }

    private void FixedUpdate()
    {
        if (!isColliding && target != null)
        {
            Move(target.position); // Ÿ������ �̵�
        }
    }

    private void Move(Vector2 targetPosition)
    {
        // �ܼ��� x ���⸸ ����Ͽ� �̵�
        float direction = targetPosition.x - transform.position.x;

        // ���⿡ ���� �̵� �ӵ� ���� (Normalize ������� ����)
        direction = direction > 0 ? 1 : -1; // ������: 1, ����: -1

        rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);

        // ��������Ʈ ���� ����
        GetComponent<SpriteRenderer>().flipX = direction < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log($"{gameObject.name}�� {collision.gameObject.name}�� �浹�߽��ϴ�.");
            isColliding = true; // �浹 ���� ����
            rigidBody.velocity = Vector2.zero; // �̵� ����
        }
    }

    private void FindEnemy()
    {
        int enemyLayer = LayerMask.NameToLayer("Player");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, 1 << enemyLayer);

        if (enemies.Length > 0)
        {
            target = enemies[0].transform; // ���� ����� ���� Ÿ������ ����
            Debug.Log($"Ÿ�� ����: {target.name}");
        }
        else
        {
            Debug.LogWarning("���� ã�� �� �����ϴ�!");
        }
    }
}
