using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public Unit unit; // 현재 유닛의 스탯 정보
    public Unit targetUnit; // 공격 대상 유닛
    public float attackRange = 1.5f; // 공격 범위
    private float attackCooldown; // 공격 대기 시간
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (targetUnit == null || unit == null) return;

        float distance = Vector2.Distance(transform.position, targetUnit.transform.position);

        // 공격 가능 거리 안에 있다면 공격
        if (distance <= attackRange && attackCooldown <= 0)
        {
            Attack();
            attackCooldown = unit.attackSpeed; // 공격 대기 시간 초기화
        }

        // 공격 대기 시간 감소
        attackCooldown -= Time.deltaTime;
    }

    private void Attack()
    {
        if (targetUnit == null || targetUnit.currentHP <= 0) return;

        animator.SetTrigger("Attack");

        Debug.Log($"{gameObject.name}이(가) {targetUnit.gameObject.name}을(를) 공격합니다!");
        targetUnit.takeDamage(unit.attackPower);
    }
}