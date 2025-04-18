using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;

public enum UnitState
{
    Idle,
    Searching,
    Fighting,
    Dead
}

public class AttackAI : MonoBehaviour
{
    public float moveSpeed = 2f; // 이동 속도
    public float attackCooldown = 1f; // 공격 쿨타임
    private Rigidbody2D rigidBody;
    private Transform target; // 타겟 (Enemy)
    public bool isColliding = false; // 충돌 상태 확인
    private float lastAttackTime; // 마지막 공격 시간
    private UnitState currentState = UnitState.Searching; // 유닛의 현재 상태 (초기값: 탐색)

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // Enemy 레이어를 찾아 타겟 설정
        FindEnemy();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case UnitState.Idle:
                // 아무것도 하지 않고 대기
                break;
            case UnitState.Searching:
                if (target == null)
                {
                    FindEnemy();
                }

                if (target != null && !isColliding)
                {
                    Move(target.position);
                }
                break;

            case UnitState.Fighting:
                if (isColliding && target != null)
                {
                    Attack();
                }
                break;

            case UnitState.Dead:
                // 아무것도 하지 않음 (추후 사망 애니메이션이나 삭제 처리 가능)
                break;
        }
    }

    private void Move(Vector2 targetPosition)
    {
        // 단순히 x 방향만 계산하여 이동
        float direction = targetPosition.x - transform.position.x;

        // 방향에 따라 이동 속도 설정 (Normalize 사용하지 않음)
        direction = direction > 0 ? 1 : -1; // 오른쪽: 1, 왼쪽: -1

        rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);

        // 스프라이트 방향 설정
        GetComponent<SpriteRenderer>().flipX = direction < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log($"{gameObject.name}가 {collision.gameObject.name}와 충돌했습니다.");
            isColliding = true; // 충돌 상태 설정
            rigidBody.velocity = Vector2.zero; // 이동 멈춤
            target = collision.transform; // 충돌한 Enemy를 타겟으로 설정

            currentState = UnitState.Fighting;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.transform == target)
        {
            Debug.Log($"{gameObject.name}가 {collision.gameObject.name}와 충돌이 끝났습니다.");
            isColliding = false; // 충돌 상태 해제
            target = null; // 타겟 초기화
        }
    }

    private void FindEnemy()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, 1 << enemyLayer);

        if (enemies.Length > 0)
        {
            // 가장 가까운 적을 타겟으로 설정
            target = enemies[0].transform;
            Debug.Log($"타겟 설정: {target.name}");
        }
        else
        {
            target = null; // 타겟 없음
            Debug.LogWarning("Enemy를 찾을 수 없습니다!");
            currentState = UnitState.Idle; // 대기 상태
        }
    }

    private void Attack()
    {
        if (target == null)
        {
            Debug.LogWarning("타겟이 없습니다. 공격을 멈춥니다.");
            return; // 타겟이 없으면 공격 중단
        }

        // 쿨타임 체크
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            Unit targetUnit = target.GetComponent<Unit>();
            if (targetUnit != null)
            {
                targetUnit.takeDamage(GetComponent<Unit>().attackPower);

                // test
                Unit unit = GetComponent<Unit>();

                unit.stat.Exp += 1;
                Debug.Log($"Exp : {unit.stat.Exp}");

                string jsonData = JsonUtility.ToJson(unit.stat);
                string path = Path.Combine(Application.dataPath, "PlayerData.json");
                File.WriteAllText(path, jsonData);
            }
        }
    }
}
