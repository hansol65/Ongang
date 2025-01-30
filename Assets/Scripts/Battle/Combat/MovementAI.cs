using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public float moveSpeed = 2f;    // 이동 속도
    private int defaultDirection;  // 디폴트 방향(-1: 좌, 0: 가만히, 1: 우)
    private float directionChangeCooldown;  // 방향 변경 쿨타임
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody; // Rigidbody2D 추가

    private LayerMask groundLayer; // 바닥 체크를 위한 레이어
    private bool isActive = true;  // 활성화 상태 제어 변수


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");

        ChangeDirection();
    }

    private void Update()
    {
        if (isActive) // 활성화 상태에서만 움직임 처리
        {
            DefaultMovement();
        }
    }

    private void DefaultMovement()
    {
        // Raycast로 바닥 체크
        if (IsGrounded(defaultDirection))
        {
            rigidBody.velocity = new Vector2(defaultDirection * moveSpeed, rigidBody.velocity.y); // 속도 설정

            // 스프라이트 방향 전환
            if (defaultDirection < 0)
                spriteRenderer.flipX = true; // 왼쪽으로 이동
            else if (defaultDirection > 0)
                spriteRenderer.flipX = false; // 오른쪽으로 이동
        }
        else
        {
            ChangeDirection(); // 바닥이 없으면 방향 전환
        }

        // 쿨타임 지나면 새로운 랜덤 방향 설정
        directionChangeCooldown -= Time.deltaTime;
        if (directionChangeCooldown <= 0)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        defaultDirection = Random.Range(-1, 2);    // 랜덤으로 -1, 0, 1 방향 선택
        directionChangeCooldown = Random.Range(2f, 5f); // 대기 시간 2에서 5초 사이
    }

    private bool IsGrounded(float direction)
    {
        // Raycast로 바닥 체크
        Vector2 origin = new Vector2(transform.position.x + direction * 0.5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(origin, Vector2.down * 1f, Color.red); // Raycast 디버그용
        return hit.collider != null;
    }

    // MovementAI 비활성화
    public void StopMovement()
    {
        isActive = false;
        rigidBody.velocity = Vector2.zero; // 이동 멈춤
    }

    // MovementAI 활성화
    public void ResumeMovement()
    {
        isActive = true;
        ChangeDirection(); // 방향 변경
    }
}
