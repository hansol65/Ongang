using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public float moveSpeed = 2f;          // �̵� �ӵ�
    public float attackCooldown = 1f;    // ���� ��Ÿ��
    private float cooldownTimer = 0f;    // ���� ��Ÿ��
    private Unit targetUnit;             // ���� Ÿ��
    private Rigidbody2D rigidBody;       // Rigidbody2D ������Ʈ
    private SpriteRenderer spriteRenderer; // SpriteRenderer

    private bool isActive = false;       // AI Ȱ��ȭ ����
    private LayerMask targetLayer;       // ���� ��� ���̾�

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Enemy ���̾�� ����
        targetLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        if (!isActive) return; // AI ��Ȱ��ȭ ���¿����� �������� ����

        if (targetUnit == null || targetUnit.currentHP <= 0)
        {
            FindTarget(); // Ÿ�� Ž��

            if (targetUnit == null) // Ÿ���� ������ �̵� ����
            {
                rigidBody.velocity = Vector2.zero;
                return;
            }
        }

        float distance = Vector2.Distance(transform.position, targetUnit.transform.position);
        float combinedRadius = (spriteRenderer.bounds.extents.x + targetUnit.GetComponent<SpriteRenderer>().bounds.extents.x) * 0.9f;

        if (distance > combinedRadius) // Ÿ�ٰ� �پ����� ������ �̵�
        {
            MoveToTarget();
        }
        else
        {
            rigidBody.velocity = Vector2.zero; // Ÿ�ٿ� ����������� �̵� ����

            if (cooldownTimer <= 0)
            {
                Attack(); // ���� ����
                cooldownTimer = attackCooldown; // ��Ÿ�� �ʱ�ȭ
            }

            cooldownTimer -= Time.deltaTime; // ��Ÿ�� ����
        }
    }

    private void FindTarget()
    {
        // Enemy ���̾��� ���� Ž��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 100f, targetLayer); // ���� Ž��
        foreach (var collider in colliders)
        {
            var unit = collider.GetComponent<Unit>();
            if (unit != null && unit.currentHP > 0)
            {
                targetUnit = unit;
                Debug.Log($"{gameObject.name}��(��) {targetUnit.gameObject.name}�� Ÿ������ ����");
                return;
            }
        }

        targetUnit = null;
    }

    private void MoveToTarget()
    {
        Vector2 direction = (targetUnit.transform.position - transform.position).normalized;
        rigidBody.velocity = new Vector2(direction.x * moveSpeed, rigidBody.velocity.y);

        // ��������Ʈ ���� ��ȯ
        spriteRenderer.flipX = direction.x < 0;
    }

    private void Attack()
    {
        if (targetUnit == null) return;

        targetUnit.takeDamage(1); // Ÿ�ٿ��� ������
        Debug.Log($"{gameObject.name}��(��) {targetUnit.gameObject.name}�� �����մϴ�!");

        if (targetUnit.currentHP <= 0)
        {
            Debug.Log($"{targetUnit.gameObject.name}��(��) �׾����ϴ�!");
            targetUnit.Die(); // Ÿ�� ���� ��� ó��
            targetUnit = null; // Ÿ�� �ʱ�ȭ
        }
    }

    public void ActivateAI()
    {
        isActive = true; // AttackAI Ȱ��ȭ
        Debug.Log($"{gameObject.name}�� AttackAI�� Ȱ��ȭ�Ǿ����ϴ�.");
    }

    public void DeactivateAI()
    {
        isActive = false; // AttackAI ��Ȱ��ȭ
        rigidBody.velocity = Vector2.zero; // �̵� ����
        Debug.Log($"{gameObject.name}�� AttackAI�� ��Ȱ��ȭ�Ǿ����ϴ�.");
    }
}
