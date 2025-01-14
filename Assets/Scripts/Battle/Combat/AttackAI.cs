using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public Unit unit; // ���� ������ ���� ����
    public Unit targetUnit; // ���� ��� ����
    public float attackRange = 1.5f; // ���� ����
    private float attackCooldown; // ���� ��� �ð�
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (targetUnit == null || unit == null) return;

        float distance = Vector2.Distance(transform.position, targetUnit.transform.position);

        // ���� ���� �Ÿ� �ȿ� �ִٸ� ����
        if (distance <= attackRange && attackCooldown <= 0)
        {
            Attack();
            attackCooldown = unit.attackSpeed; // ���� ��� �ð� �ʱ�ȭ
        }

        // ���� ��� �ð� ����
        attackCooldown -= Time.deltaTime;
    }

    private void Attack()
    {
        if (targetUnit == null || targetUnit.currentHP <= 0) return;

        animator.SetTrigger("Attack");

        Debug.Log($"{gameObject.name}��(��) {targetUnit.gameObject.name}��(��) �����մϴ�!");
        targetUnit.takeDamage(unit.attackPower);
    }
}