using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public float moveSpeed = 2f;          // 이동 속도
    public float attackCooldown = 1f;    // 공격 쿨타임
    private float cooldownTimer = 0f;    // 현재 쿨타임
    private Unit targetUnit;             // 현재 타겟
    private Rigidbody2D rigidBody;       // Rigidbody2D 컴포넌트
    private SpriteRenderer spriteRenderer; // SpriteRenderer

    private bool isActive = false;       // AI 활성화 여부
    private LayerMask targetLayer;       // 공격 대상 레이어

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Enemy 레이어로 설정
        targetLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        if (!isActive) return; // AI 비활성화 상태에서는 동작하지 않음

        if (targetUnit == null || targetUnit.currentHP <= 0)
        {
            FindTarget(); // 타겟 탐색

            if (targetUnit == null) // 타겟이 없으면 이동 멈춤
            {
                rigidBody.velocity = Vector2.zero;
                return;
            }
        }

        float distance = Vector2.Distance(transform.position, targetUnit.transform.position);
        float combinedRadius = (spriteRenderer.bounds.extents.x + targetUnit.GetComponent<SpriteRenderer>().bounds.extents.x) * 0.9f;

        if (distance > combinedRadius) // 타겟과 붙어있지 않으면 이동
        {
            MoveToTarget();
        }
        else
        {
            rigidBody.velocity = Vector2.zero; // 타겟에 가까워졌으면 이동 멈춤

            if (cooldownTimer <= 0)
            {
                Attack(); // 공격 실행
                cooldownTimer = attackCooldown; // 쿨타임 초기화
            }

            cooldownTimer -= Time.deltaTime; // 쿨타임 감소
        }
    }

    private void FindTarget()
    {
        // Enemy 레이어의 유닛 탐지
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 100f, targetLayer); // 넓은 탐지
        foreach (var collider in colliders)
        {
            var unit = collider.GetComponent<Unit>();
            if (unit != null && unit.currentHP > 0)
            {
                targetUnit = unit;
                Debug.Log($"{gameObject.name}이(가) {targetUnit.gameObject.name}을 타겟으로 설정");
                return;
            }
        }

        targetUnit = null;
    }

    private void MoveToTarget()
    {
        Vector2 direction = (targetUnit.transform.position - transform.position).normalized;
        rigidBody.velocity = new Vector2(direction.x * moveSpeed, rigidBody.velocity.y);

        // 스프라이트 방향 전환
        spriteRenderer.flipX = direction.x < 0;
    }

    private void Attack()
    {
        if (targetUnit == null) return;

        targetUnit.takeDamage(1); // 타겟에게 데미지
        Debug.Log($"{gameObject.name}이(가) {targetUnit.gameObject.name}을 공격합니다!");

        if (targetUnit.currentHP <= 0)
        {
            Debug.Log($"{targetUnit.gameObject.name}이(가) 죽었습니다!");
            targetUnit.Die(); // 타겟 유닛 사망 처리
            targetUnit = null; // 타겟 초기화
        }
    }

    public void ActivateAI()
    {
        isActive = true; // AttackAI 활성화
        Debug.Log($"{gameObject.name}의 AttackAI가 활성화되었습니다.");
    }

    public void DeactivateAI()
    {
        isActive = false; // AttackAI 비활성화
        rigidBody.velocity = Vector2.zero; // 이동 멈춤
        Debug.Log($"{gameObject.name}의 AttackAI가 비활성화되었습니다.");
    }
}
