using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;

public class AttackAI : MonoBehaviour
{
    public float moveSpeed = 2f; // 이동 속도
    public float attackCooldown = 1f; // 공격 쿨타임
    private Rigidbody2D rigidBody;
    private Transform target; // 타겟 (Enemy)
    public bool isColliding = false; // 충돌 상태 확인
    private float lastAttackTime; // 마지막 공격 시간

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // Enemy 레이어를 찾아 타겟 설정
        FindEnemy();
    }

    private void FixedUpdate()
    {
        if (!isColliding && target != null)
        {
            Move(target.position); // 타겟으로 이동
        }
        else if (isColliding && target != null)
        {
            Attack(); // 배틀 시작
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
                Debug.Log($"{gameObject.name}이(가) {target.name}에게 공격을 가했습니다!");


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
