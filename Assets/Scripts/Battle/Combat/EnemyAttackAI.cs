using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    public float moveSpeed = 2f; // 이동 속도
    private Rigidbody2D rigidBody;
    private Transform target; // 타겟 (Player)
    private bool isColliding = false; // 충돌 상태 확인

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // Player 레이어를 찾아 타겟 설정
        FindEnemy();
    }

    private void FixedUpdate()
    {
        if (!isColliding && target != null)
        {
            Move(target.position); // 타겟으로 이동
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log($"{gameObject.name}가 {collision.gameObject.name}와 충돌했습니다.");
            isColliding = true; // 충돌 상태 설정
            rigidBody.velocity = Vector2.zero; // 이동 멈춤
        }
    }

    private void FindEnemy()
    {
        int enemyLayer = LayerMask.NameToLayer("Player");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, 1 << enemyLayer);

        if (enemies.Length > 0)
        {
            target = enemies[0].transform; // 가장 가까운 적을 타겟으로 설정
            Debug.Log($"타겟 설정: {target.name}");
        }
        else
        {
            Debug.LogWarning("적을 찾을 수 없습니다!");
        }
    }
}
